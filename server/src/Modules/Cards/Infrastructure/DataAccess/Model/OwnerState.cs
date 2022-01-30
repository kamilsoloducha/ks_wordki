using System;
using System.Collections.Generic;

namespace Cards.Infrastructure.Model
{
    internal class OwnerState
    {
        public Guid Id { get; set; }
        public IList<GroupState> Groups { get; set; }
        public IList<DetailState> Details { get; set; }
    }
}