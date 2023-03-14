using System;
using Domain;
using Domain.Utils;

namespace Lessons.Domain.Lesson
{
    public class Lesson : Entity
    {
        public DateTime StartDate { get; private set; }
        public Guid UserId { get; private set; }
        public LessonType Type { get; private set; }
        public int TimeCounter { get; private set; }
        public Performance.Performance Performence { get; private set; }

        private Lesson() { }

        internal static Lesson NewLesson(Performance.Performance performence, LessonType type)
        {
            return new Lesson
            {
                StartDate = SystemClock.Now,
                UserId = performence.UserId,
                Type = type,
                TimeCounter = 0,
                Performence = performence
            };
        }

        internal void RegisterAnswer(Guid cardId, int side, int result)
        {
            IsDirty = true;
            TimeCounter = (int)(SystemClock.Now.Ticks - StartDate.Ticks / 1000);
        }
    }
}