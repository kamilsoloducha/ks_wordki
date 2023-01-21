using System;
using System.Collections.Generic;

namespace E2e.Model.Tests.Model.Cards
{
    public partial class Card
    {
        public Card()
        {
            Groups = new HashSet<Group>();
        }

        public long Id { get; set; }
        public bool IsPrivate { get; set; }
        public long FrontId { get; set; }
        public long BackId { get; set; }

        public virtual Side Back { get; set; }
        public virtual Side Front { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}
