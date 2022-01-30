using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Requests;
using Lessons.Domain;
using MassTransit;

namespace Lessons.Application.Commands
{
    public class RegisterAnswer
    {
        internal class CommandHandler : RequestHandlerBase<Command, Resposne>
        {
            private readonly IPerformanceRepository _repository;
            private readonly IPublishEndpoint _publishEndpoint;

            public CommandHandler(IPerformanceRepository repository, IPublishEndpoint publishEndpoint)
            {
                _repository = repository;
                _publishEndpoint = publishEndpoint;
            }

            public async override Task<ResponseBase<Resposne>> Handle(Command request, CancellationToken cancellationToken)
            {
                var performance = await _repository.GetByUserId(request.UserId, cancellationToken);
                if (performance is null) return ResponseBase<Resposne>.Create("performance is null");

                performance.RegisterAnswer(request.SideId, request.Result);
                await _repository.Update(performance);
                await _publishEndpoint.Publish(performance.Events.First(), cancellationToken);

                return ResponseBase<Resposne>.Create(new Resposne());
            }
        }

        public class Command : RequestBase<Resposne>
        {
            public DateTime StartLessonDate { get; set; } // usually it will be the latest, always??
            public Guid UserId { get; set; }
            public long SideId { get; set; }
            public int Result { get; set; }
        }

        public class Resposne { }
    }
}