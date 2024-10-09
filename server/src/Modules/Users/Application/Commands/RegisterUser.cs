using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Serilog;
using Users.Application.Services;
using Users.Domain.User;

namespace Users.Application.Commands;

public abstract class RegisterUser
{
    internal class CommandHandler(
        IUserRepository userRepository,
        IPasswordManager passwordManager,
        IPublishEndpoint publishEndpoint)
        : IRequestHandler<Command, Response>
    {
        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            if (await userRepository.Any(x => x.Name == request.UserName, cancellationToken))
                return new Response(ResponseCode.UserNameAlreadyOccupied);

            if (await userRepository.Any(x => x.Email == request.Email, cancellationToken))
                return new Response(ResponseCode.EmailAlreadyOccupied);

            var passwordHash = passwordManager.CreateHashedPassword(request.Password);

            var user = User.RegisterUser(
                request.UserName,
                passwordHash,
                request.Email,
                request.FirstName,
                request.Surname
            );

            await userRepository.Add(user, cancellationToken);
            await publishEndpoint.PublishBatch(user.Events, cancellationToken);
            
            Log.Information("User created: {UserId}", user.Id);
            
            return new Response(ResponseCode.Successful, user.Id);
        }
    }

    public record Command
    (
        string UserName,
        string Password,
        string Email,
        string FirstName = "",
        string Surname = ""
    ) : IRequest<Response>;

    public record Response(ResponseCode ResponseCode, Guid? UserId = null);
    
    public enum ResponseCode
    {
        Successful,
        UserNameAlreadyOccupied,
        EmailAlreadyOccupied
    }
}