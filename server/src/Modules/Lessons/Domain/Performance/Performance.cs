using System;
using System.Collections.Generic;
using System.Linq;
using Blueprints.Domain;
using Domain.IntegrationEvents;

namespace Lessons.Domain
{
    public class Performance : Entity
    {
        public PerformanceId Id { get; private set; }
        public Guid UserId { get; private set; }
        public IList<Lesson> Lessons { get; private set; } = new List<Lesson>();

        private Performance() { }

        public static Performance Create(Guid userId)
        {
            return new Performance
            {
                Id = PerformanceId.Create(),
                UserId = userId,
                Lessons = new List<Lesson>()
            };
        }

        public DateTime StartLesson(LessonType type)
        {
            var newLesson = Lesson.NewLesson(this, type);
            Lessons.Add(newLesson);
            return newLesson.StartDate;
        }

        public void RegisterAnswer(long sideId, int result)
        {
            var latestLesson = Lessons.Aggregate((l1, l2) => l1.StartDate > l2.StartDate ? l1 : l2);

            // latestLesson.RegisterAnswer(cardId, side, result);
            _events.Add(new AnswerRegistered
            {
                UserId = UserId,
                SideId = sideId,
                Result = result
            });
        }
    }
}
