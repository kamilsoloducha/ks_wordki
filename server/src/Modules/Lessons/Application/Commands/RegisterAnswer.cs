using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Requests;
using Lessons.Domain.Performance;
using MassTransit;
using MediatR;

namespace Lessons.Application.Commands;

public abstract class RegisterAnswer
{
    internal class CommandHandler : RequestHandlerBase<Command, Unit>
    {
        private readonly IPerformanceRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;


        public CommandHandler(IPerformanceRepository repository, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
        }

        public override async Task<ResponseBase<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var performance = await _repository.GetByUserId(request.UserId, cancellationToken);
            if (performance is null)
            {
                return ResponseBase<Unit>.CreateError("Performance not found");
            }
            
            performance.RegisterAnswer(request.CardId, request.SideType, request.Result);
            await _repository.Update(performance);
            await _publishEndpoint.Publish(performance.Events.First(), cancellationToken);

            return ResponseBase<Unit>.Create(Unit.Value);
        }
    }

    public record Command(Guid UserId, long CardId, int SideType, int Result) : IRequest<ResponseBase<Unit>>;
}