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

public abstract class LoginUser
{
    internal class CommandHandler(
        IUserRepository userRepository,
        IPasswordManager passwordManager,
        IAuthenticationService authenticationService)
        : IRequestHandler<Command, Response>
    {
        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var hashedPassword = passwordManager.CreateHashedPassword(request.Password);
            var user = await userRepository.GetUser(request.UserName, hashedPassword, cancellationToken);
            
            if (user is null)
                return new Response(ResponseCode.UserNotFound);

            var result = authenticationService.Authenticate(user.Id, user.Roles.Select(x => x.Type.ToString()));

            user.Login();
            await userRepository.Update(user, cancellationToken);
            return new Response(ResponseCode.Successful, result.Token, user.Id, result.CreatingDateTime, result.ExpirationDateTime);
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