using System;
using System.Threading;
using System.Threading.Tasks;
using Users.Domain;
using Blueprints.Application.Requests;
using FluentValidation;

namespace Users.Application
{
    public class RegisterUser
    {
        internal class CommandHandler : RequestHandlerBase<Command, Guid>
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

            public async override Task<ResponseBase<Guid>> Handle(Command request, CancellationToken cancellationToken)
            {
                var passwordHash = _passwordManager.CreateHashedPassword(request.Password);

                var user = await User.RegisterUser(
                    request.Name,
                    passwordHash,
                    request.Email,
                    request.FirstName,
                    request.Surname,
                    _dataChecker,
                    cancellationToken
                );

                await _userRepository.Add(user, cancellationToken);

                return ResponseBase<Guid>.Create(user.Id);
            }
        }
        public class Command : RequestBase<Guid>
        {
            public string Name { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string Surname { get; set; }
        }

        internal class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }
    }
}
