using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Requests;
using Lessons.Domain.Lesson;
using Lessons.Domain.Performance;
using MediatR;

namespace Lessons.Application.Commands
{
    public abstract class StartLesson
    {
        internal class CommandHandler : RequestHandlerBase<Command, Unit>
        {
            private readonly IPerformanceRepository _repository;

            public CommandHandler(IPerformanceRepository repository)
            {
                _repository = repository;
            }

            public override async Task<ResponseBase<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var performance = await _repository.GetByUserId(request.UserId, cancellationToken);

                if (performance is null)
                {
                    return ResponseBase<Unit>.CreateError("Performance not found");
                }
            
                var lessonType = LessonType.Create(request.LessonType);
                performance.StartLesson(lessonType);

                await _repository.Update(performance);
                return ResponseBase<Unit>.Create(Unit.Value);
            }
        }

        public record Command(Guid UserId, int LessonType) : IRequest<ResponseBase<Unit>>;
    }
}