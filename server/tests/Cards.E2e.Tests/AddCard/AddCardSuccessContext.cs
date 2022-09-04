using System;
using System.Collections.Generic;
using Card = Cards.E2e.Tests.Models.Cards.Card;
using Detail = Cards.E2e.Tests.Models.Cards.Detail;
using Group = Cards.E2e.Tests.Models.Cards.Group;

namespace Cards.E2e.Tests.AddCard;

public abstract class AddCardSuccessContext
{
    public abstract Application.Commands.AddCard.Command GivenRequest { get; }
    public abstract Group GivenGroup { get; }
    public virtual IReadOnlyCollection<Card> ExpectedCards { get; } = Array.Empty<Card>();
    public virtual IReadOnlyCollection<Detail> ExpectedDetails { get; } = Array.Empty<Detail>();
}