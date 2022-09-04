using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Authentication;
using Domain.Utils;
using FluentValidation;
using MediatR;
using Users.Domain;
using Users.Domain.User;

namespace Users.Application.Commands;

public class RefreshToken
{
    internal class CommandHandler : IRequestHandler<Command, Response>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;

        public CommandHandler(IUserRepository userRepository,
            IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

        public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.Id, cancellationToken);
            if (user is null)
                return new Response(ResponseCode.UserNotFound);

            user.Login();
            await _userRepository.Update(user, cancellationToken);

            var creatingDate = SystemClock.Now;
            var newToken = _authenticationService.Refresh(request.Token);

            return new Response(ResponseCode.Success, newToken, request.Id, creatingDate, creatingDate.AddDays(7));
        }
    }

    public record Command(Guid Id, string Token) : IRequest<Response>;

    public record Response(
        ResponseCode ResponseCode,
        string Token = null,
        Guid? Id = null,
        DateTime? CreatingDateTime = null,
        DateTime? ExpirationDateTime = null);

    public enum ResponseCode
    {
        Success,
        UserNotFound
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