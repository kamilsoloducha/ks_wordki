using System;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Requests;
using FluentValidation;
using MediatR;
using Users.Domain;

namespace Users.Application
{
    public class DeleteUser
    {
        public class Command : RequestBase<Unit>
        {
            public Guid? Id { get; set; }
        }

        internal class CommandHandler : RequestHandlerBase<Command, Unit>
        {
            private readonly IUserRepository _userRepository;

            public CommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async override Task<ResponseBase<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetUser(request.Id.Value, cancellationToken);
                if (user is null)
                    return ResponseBase<Unit>.Create("user is null");

                user.Remove();

                await _userRepository.Update(user, cancellationToken);
                return ResponseBase<Unit>.Create(Unit.Value);
            }
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