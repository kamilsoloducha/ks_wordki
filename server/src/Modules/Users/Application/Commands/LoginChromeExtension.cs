using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Authentication;
using MediatR;
using Users.Domain;

namespace Users.Application.Commands;

public class LoginChromeExtension
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
                return new Response(ResponseCode.UserNotFound, string.Empty);

            var token = _authenticationService.Authenticate(user.Id, new[] { RoleType.ChromeExtension.ToString() });

            user.Login();
            await _userRepository.Update(user, cancellationToken);

            return new Response(ResponseCode.Success, token);
        }
    }

    public record Command(string UserName, string Password) : IRequest<Response>;

    public record Response(ResponseCode ResponseCode, string Token);

    public enum ResponseCode
    {
        Success,
        UserNotFound
    }
}