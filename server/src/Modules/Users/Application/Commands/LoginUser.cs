using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Authentication;
using Users.Domain;
using Blueprints.Application.Requests;
using FluentValidation;
using System;

namespace Users.Application
{
    public class LoginUser
    {
        internal class CommandHandler : RequestHandlerBase<Command, Response>
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

            public async override Task<ResponseBase<Response>> Handle(Command request, CancellationToken cancellationToken)
            {
                var hashedPassword = _passwordManager.CreateHashedPassword(request.Password);
                var user = await _userRepository.GetUser(request.UserName, hashedPassword, cancellationToken);
                if (user is null)
                {
                    return ResponseBase<Response>.Create("user is null");
                }

                var token = _authenticationService.Authenticate(user.Id, user.Roles.Select(x => x.Type.ToString()));

                user.Login();
                await _userRepository.Update(user, cancellationToken);

                return ResponseBase<Response>.Create(new Response
                {
                    Token = token,
                    Id = user.Id
                });
            }
        }

        public class Command : RequestBase<Response>
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        internal class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Response
        {
            public string Token { get; set; }
            public Guid Id { get; set; }
            public DateTime CreatingDateTime { get; set; }
            public DateTime ExpirationDateTime { get; set; }
        }
    }
}