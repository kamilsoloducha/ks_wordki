using System;
using System.Collections.Generic;

namespace E2e.Model.Tests.Model.Cards
{
    public partial class Group
    {
        public Group()
        {
            Cards = new HashSet<Card>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public int Front { get; set; }
        public int Back { get; set; }
        public Guid OwnerId { get; set; }

        public virtual Owner Owner { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}
