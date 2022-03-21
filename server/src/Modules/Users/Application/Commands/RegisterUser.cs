using System;
using System.Threading;
using System.Threading.Tasks;
using Users.Domain;
using Blueprints.Application.Requests;
using FluentValidation;
using MediatR;

namespace Users.Application
{
    public class RegisterUser
    {
        internal class CommandHandler : IRequestHandler<Command, Response>
        {
            private readonly IDataChecker _dataChecker;
            private readonly IUserRepository _userRepository;
            private readonly IPasswordManager _passwordManager;

            public CommandHandler(
                IDataChecker dataChecker,
                IUserRepository userRepository,
                IPasswordManager passwordManager)
            {
                _dataChecker = dataChecker;
                _userRepository = userRepository;
                _passwordManager = passwordManager;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _dataChecker.Any(x => x.Name == request.UserName, cancellationToken))
                    return new Response { ResponseCode = ResponseCode.UserNameAlreadyOccupied };

                if (!string.IsNullOrEmpty(request.Email) && await _dataChecker.Any(x => x.Email == request.Email, cancellationToken))
                    return new Response { ResponseCode = ResponseCode.EmailAlreadyOccupied };

                var passwordHash = _passwordManager.CreateHashedPassword(request.Password);

                var user = User.RegisterUser(
                    request.UserName,
                    passwordHash,
                    request.Email,
                    request.FirstName,
                    request.Surname
                );

                await _userRepository.Add(user, cancellationToken);

                return new Response
                {
                    UserId = user.Id,
                    ResponseCode = ResponseCode.Successful
                };
            }
        }
        public class Command : IRequest<Response>
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string Surname { get; set; }
        }

        public class Response
        {
            public ResponseCode ResponseCode { get; set; }
            public Guid? UserId { get; set; }
        }

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
}
