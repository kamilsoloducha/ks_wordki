using System.Collections.Generic;
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.TickCard.Contexts;

public abstract class TickCardContext
{
    public abstract Owner GivenOwner { get; }
    public abstract string GivenCardId { get; }
    public abstract IEnumerable<Detail> ExpectedDetails { get; }

}