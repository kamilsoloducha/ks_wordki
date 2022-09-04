#nullable disable

using System;
using System.Collections.Generic;

namespace Cards.E2e.Tests.Models.Cards;

public partial class Group
{
    public Group()
    {
        GroupsCards = new HashSet<GroupsCard>();
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public int Front { get; set; }
    public int Back { get; set; }
    public Guid OwnerId { get; set; }

    public virtual Owner Owner { get; set; }
    public virtual ICollection<GroupsCard> GroupsCards { get; set; }
}