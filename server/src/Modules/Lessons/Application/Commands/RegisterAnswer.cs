using System;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Requests;
using Lessons.Domain;

namespace Lessons.Application.Commands
{
    public class RegisterAnswer
    {
        internal class CommandHandler : RequestHandlerBase<Command, Resposne>
        {
            private readonly IPerformanceRepository _repository;

            public CommandHandler(IPerformanceRepository repository)
            {
                _repository = repository;
            }

            public async override Task<ResponseBase<Resposne>> Handle(Command request, CancellationToken cancellationToken)
            {
                var performance = await _repository.GetByUserId(request.UserId, cancellationToken);
                if (performance is null) return ResponseBase<Resposne>.Create("performance is null");

                performance.RegisterAnswer(request.CardId, request.Side, request.Result);

                return ResponseBase<Resposne>.Create(new Resposne());
            }
        }

        public class Command : RequestBase<Resposne>
        {
            public DateTime StartLessonDate { get; set; } // usually it will be the latest, always??
            public Guid UserId { get; set; }
            public Guid CardId { get; set; }
            public int Side { get; set; }
            public int Result { get; set; }
        }

        public class Resposne
        {

        }
    }
}