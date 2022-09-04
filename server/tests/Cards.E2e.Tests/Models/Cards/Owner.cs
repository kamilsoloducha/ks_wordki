#nullable disable

using System;
using System.Collections.Generic;

namespace Cards.E2e.Tests.Models.Cards;

public partial class Owner
{
    public Owner()
    {
        Details = new HashSet<Detail>();
        Groups = new HashSet<Group>();
    }

    public Guid Id { get; set; }

    public virtual ICollection<Detail> Details { get; set; }
    public virtual ICollection<Group> Groups { get; set; }
}