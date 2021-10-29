using System;
using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Requests;
using FluentValidation;
using Lessons.Domain;

namespace Lessons.Application.Commands
{
    public class StartLesson
    {
        internal class CommandHandler : RequestHandlerBase<Command, Response>
        {
            private readonly IPerformanceRepository _repository;

            public CommandHandler(IPerformanceRepository repository)
            {
                _repository = repository;
            }

            public async override Task<ResponseBase<Response>> Handle(Command request, CancellationToken cancellationToken)
            {
                var performance = await _repository.GetByUserId(request.UserId, cancellationToken);
                if (performance is null) return ResponseBase<Response>.Create("performance is null");

                var lessonStartDate = performance.StartLesson();
                return ResponseBase<Response>.Create(new Response
                {
                    StartDate = lessonStartDate,
                });
            }
        }

        public class Command : RequestBase<Response>
        {
            public Guid UserId { get; set; }
        }

        public class Response
        {
            public DateTime StartDate { get; set; }
        }

        internal class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.UserId).Must(x => x != Guid.Empty);
            }
        }
    }
}