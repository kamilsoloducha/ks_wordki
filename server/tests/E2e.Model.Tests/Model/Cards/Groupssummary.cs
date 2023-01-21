using System;
using System.Collections.Generic;

namespace E2e.Model.Tests.Model.Cards
{
    public partial class Groupssummary
    {
        public Guid? OwnerId { get; set; }
        public long? Id { get; set; }
        public string Name { get; set; }
        public int? Front { get; set; }
        public int? Back { get; set; }
        public long? CardsCount { get; set; }
    }
}
