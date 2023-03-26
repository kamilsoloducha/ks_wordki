using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Authentication;
using Domain.Utils;
using MediatR;
using Users.Application.Services;
using Users.Domain.User;

namespace Users.Application.Commands;

public class LoginUser
{
    internal class CommandHandler : IRequestHandler<Command, Response>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordManager _passwordManager;
        private readonly IAuthenticationService _authenticationService;

        public CommandHandler(IUserRepository userRepository,
            IPasswordManager passwordManager,
            IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _passwordManager = passwordManager;
            _authenticationService = authenticationService;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var hashedPassword = _passwordManager.CreateHashedPassword(request.Password);
            var user = await _userRepository.GetUser(request.UserName, hashedPassword, cancellationToken);
            
            if (user is null)
                return new Response(ResponseCode.UserNotFound);

            var creatingDate = SystemClock.Now;
            var token = _authenticationService.Authenticate(user.Id, user.Roles.Select(x => x.Type.ToString()));

            user.Login();
            await _userRepository.Update(user, cancellationToken);
            return new Response(ResponseCode.Successful, token, user.Id, creatingDate, creatingDate.AddDays(7));
        }
    }

    public record Command(string UserName, string Password) : IRequest<Response>;

    public record Response(
        ResponseCode ResponseCode,
        string Token = null,
        Guid? Id = null,
        DateTime? CreatingDateTime = null,
        DateTime? ExpirationDateTime = null);

    public enum ResponseCode
    {
        Successful,
        UserNotFound
    }
}