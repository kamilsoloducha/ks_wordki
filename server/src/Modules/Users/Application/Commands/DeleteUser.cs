using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MassTransit;
using MediatR;
using Users.Domain;
using Users.Domain.User;

namespace Users.Application.Commands;

public class DeleteUser
{
    internal class CommandHandler : IRequestHandler<Command, ResponseCode>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public CommandHandler(IUserRepository userRepository,
            IPublishEndpoint publishEndpoint)
        {
            _userRepository = userRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<ResponseCode> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.Id, cancellationToken);
            if (user is null) return ResponseCode.UserNotFound;
            
            user.Remove();
            await _userRepository.Update(user, cancellationToken);
            await _publishEndpoint.PublishBatch(user.Events, cancellationToken);
            
            return ResponseCode.Ok;
        }
    }
    
    public record Command(Guid Id) : IRequest<ResponseCode>;

    internal class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty);
        }
    }
    
    internal enum ResponseCode
    {
        Ok,
        UserNotFound
    }
}