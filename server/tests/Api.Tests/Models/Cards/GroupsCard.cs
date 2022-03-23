using System;
using System.Collections.Generic;

#nullable disable

namespace Api.Tests.Model.Cards
{
    public partial class GroupsCard
    {
        public long CardsId { get; set; }
        public long GroupsId { get; set; }

        public virtual Card Cards { get; set; }
        public virtual Group Groups { get; set; }
    }
}
