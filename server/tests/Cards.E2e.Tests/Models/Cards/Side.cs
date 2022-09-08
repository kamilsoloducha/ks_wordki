﻿#nullable disable

using System.Collections.Generic;

namespace Cards.E2e.Tests.Models.Cards;

public partial class Side
{
    public Side()
    {
        CardBacks = new HashSet<Card>();
        CardFronts = new HashSet<Card>();
    }

    public long Id { get; set; }
    public int Type { get; set; }
    public string Value { get; set; }
    public string Example { get; set; }

    public virtual ICollection<Card> CardBacks { get; set; }
    public virtual ICollection<Card> CardFronts { get; set; }
}