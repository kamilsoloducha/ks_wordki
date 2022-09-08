﻿#nullable disable

namespace Cards.E2e.Tests.Models.Cards;

public partial class GroupsCard
{
    public long CardsId { get; set; }
    public long GroupsId { get; set; }

    public virtual Card Cards { get; set; }
    public virtual Group Groups { get; set; }
}