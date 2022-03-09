using System;

namespace Cards.Application.Queries.Models
{
    public class CardsOverview
    {
        public Guid OwnerId { get; set; }
        public long All { get; set; }
        public long Drawer1 { get; set; }
        public long Drawer2 { get; set; }
        public long Drawer3 { get; set; }
        public long Drawer4 { get; set; }
        public long Drawer5 { get; set; }
        public long LessonIncluded { get; set; }
        public long Ticked { get; set; }
    }
}