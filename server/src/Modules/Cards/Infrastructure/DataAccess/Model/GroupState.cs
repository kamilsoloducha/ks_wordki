using System;
using System.Collections.Generic;

namespace Cards.Infrastructure.DataAccess.Model
{
    internal class GroupState
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Front { get; set; }
        public int Back { get; set; }
        public IList<CardState> Cards { get; set; }
        public OwnerState Owner { get; set; }
    }
}