using System;
using Blueprints.Domain;

namespace Lessons.Domain
{
    public class Repeat : Entity
    {
        public Guid Id { get; private set; }
        public Guid CardId { get; private set; }
        public int Side { get; private set; }
        public DateTime RepeatDate { get; private set; }
        public int Result { get; private set; }
        public Lesson Lesson { get; private set; }

        private Repeat() { }

        internal static Repeat Create(Guid cardId, int side, int result)
        {
            return new Repeat
            {
                Id = Guid.NewGuid(),
                CardId = cardId,
                Side = side,
                RepeatDate = DateTime.Now,
                Result = result
            };
        }

    }
}
