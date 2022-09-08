using System;
using System.Threading;
using System.Threading.Tasks;
using Application.MassTransit;
using FluentValidation;
using MassTransit;
using MediatR;
using Serilog;
using Users.Application.Services;
using Users.Domain;
using Users.Domain.User;
using Users.Domain.User.Services;

namespace Users.Application.Commands;

public class RegisterUser
{
    internal class CommandHandler : IRequestHandler<Command, Response>
    {
        private readonly IDataChecker _dataChecker;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordManager _passwordManager;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger _logger; 

        public CommandHandler(
            IDataChecker dataChecker,
            IUserRepository userRepository,
            IPasswordManager passwordManager,
            IPublishEndpoint publishEndpoint,
            ILogger logger)
        {
            _dataChecker = dataChecker;
            _userRepository = userRepository;
            _passwordManager = passwordManager;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            if (await _dataChecker.Any(x => x.Name == request.UserName, cancellationToken))
                return new Response(ResponseCode.UserNameAlreadyOccupied);

            if (await _dataChecker.Any(x => x.Email == request.Email, cancellationToken))
                return new Response(ResponseCode.EmailAlreadyOccupied);

            var passwordHash = _passwordManager.CreateHashedPassword(request.Password);

            var user = User.RegisterUser(
                request.UserName,
                passwordHash,
                request.Email,
                request.FirstName,
                request.Surname
            );

            await _userRepository.Add(user, cancellationToken);
            await _publishEndpoint.PublishBatch(user.Events, cancellationToken);
            _logger.Information("User created: {UserId}", user.Id);
            return new Response(ResponseCode.Successful, user.Id);
        }
    }

    public record Command
    (
        string UserName,
        string Password,
        string Email,
        string FirstName,
        string Surname
    ) : IRequest<Response>;

    public record Response(ResponseCode ResponseCode, Guid? UserId = null);
    public enum ResponseCode
    {
        Successful,
        UserNameAlreadyOccupied,
        EmailAlreadyOccupied
    }

    internal class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
