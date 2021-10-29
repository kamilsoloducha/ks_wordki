using System;
using System.Collections.Generic;
using Blueprints.Domain;

namespace Lessons.Domain
{
    public class Lesson : Entity
    {
        public DateTime StartDate { get; private set; }
        public Guid UserId { get; private set; }
        public int Type { get; private set; }
        public int TimeCounter { get; private set; }
        public IList<Repeat> Repeats { get; private set; }
        public Performance Performence { get; private set; }

        private Lesson() { }

        internal static Lesson NewLesson(Performance performence, int type)
        {
            return new Lesson
            {
                StartDate = DateTime.Now,
                UserId = performence.UserId,
                Type = type,
                TimeCounter = 0,
                Repeats = new List<Repeat>(),
                Performence = performence
            };
        }

        internal void RegisterAnswer(Guid cardId, int side, int result)
        {
            Repeat.Create(cardId, side, result);
            TimeCounter = (int)(DateTime.Now.Ticks - StartDate.Ticks / 1000);
        }
    }
}
