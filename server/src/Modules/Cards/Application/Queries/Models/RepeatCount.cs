using System;

namespace Cards.Application.Queries
{
    public class RepeatCount
    {
        public int Count { get; set; }
        public DateTime Date { get; set; }
        public Guid OwnerId { get; set; }
    }
}