using System.Threading;
using System.Threading.Tasks;
using Users.Domain;
using Blueprints.Application.Requests;
using System;
using Blueprints.Application.Authentication;
using FluentValidation;
using Utils;

namespace Users.Application
{
    public class RefreshToken
    {
        internal class CommandHandler : RequestHandlerBase<Command, Response>
        {
            private readonly IUserRepository _userRepository;
            private readonly IAuthenticationService _authenticationService;

            public CommandHandler(IUserRepository userRepository,
                IAuthenticationService authenticationService)
            {
                _userRepository = userRepository;
                _authenticationService = authenticationService;
            }

            public async override Task<ResponseBase<Response>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetUser(request.Id, cancellationToken);
                if (user == null)
                {
                    throw new Exception("User not found");
                }

                user.Login();
                await _userRepository.Update(user, cancellationToken);

                var creatingDate = SystemClock.Now;
                var newToken = _authenticationService.Refresh(request.Token);

                return ResponseBase<Response>.Create(new Response
                {
                    Id = request.Id,
                    Token = newToken,
                    CreatingDateTime = creatingDate,
                    ExpirationDateTime = creatingDate.AddDays(7)
                });
            }
        }

        public class Command : RequestBase<Response>
        {
            public string Token { get; set; }
            public Guid Id { get; set; }
        }

        public class Response
        {
            public string Token { get; set; }
            public Guid Id { get; set; }
            public DateTime CreatingDateTime { get; set; }
            public DateTime ExpirationDateTime { get; set; }
        }

        internal class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).Must(x => x != Guid.Empty);
                RuleFor(x => x.Token).NotEmpty();
            }
        }
    }
}