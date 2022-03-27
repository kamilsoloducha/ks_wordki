using System.Threading;
using System.Threading.Tasks;
using Users.Domain;
using MediatR;
using Blueprints.Application.Requests;
using System.Linq;
using MassTransit;
using System;
using FluentValidation;

namespace Users.Application
{
    public class ConfirmEmail
    {
        internal class CommandHandler : RequestHandlerBase<Command, Unit>
        {
            private readonly IUserRepository _userRepository;
            private readonly IPublishEndpoint _publishEndpoint;

            public CommandHandler(IUserRepository userRepository,
                IPublishEndpoint publishEndpoint)
            {
                _userRepository = userRepository;
                _publishEndpoint = publishEndpoint;
            }

            public async override Task<ResponseBase<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetUser(request.Id, cancellationToken);

                user.Confirm();

                await _userRepository.Update(user, cancellationToken);
                await _publishEndpoint.Publish(user.Events.First(), cancellationToken);

                return ResponseBase<Unit>.Create(Unit.Value);
            }
        }

        public class Command : RequestBase<Unit>
        {
            public Guid Id { get; set; }
        }

        internal class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }
    }


}