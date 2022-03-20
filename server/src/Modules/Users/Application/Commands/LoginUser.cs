using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Authentication;
using Users.Domain;
using Blueprints.Application.Requests;
using FluentValidation;
using System;
using Utils;
using MediatR;

namespace Users.Application
{
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
                    return new Response { ResponseCode = ResponseCode.UserNotFound };

                var creatingDate = SystemClock.Now;
                var token = _authenticationService.Authenticate(user.Id, user.Roles.Select(x => x.Type.ToString()));

                user.Login();
                await _userRepository.Update(user, cancellationToken);

                return new Response
                {
                    ResponseCode = ResponseCode.Successful,
                    Token = token,
                    Id = user.Id,
                    CreatingDateTime = creatingDate,
                    ExpirationDateTime = creatingDate.AddDays(7)
                };
            }
        }

        public class Command : IRequest<Response>
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
            public ResponseCode ResponseCode { get; set; }
            public string Token { get; set; }
            public Guid Id { get; set; }
            public DateTime CreatingDateTime { get; set; }
            public DateTime ExpirationDateTime { get; set; }
        }

        public enum ResponseCode
        {
            Successful,
            UserNotFound
        }
    }
}