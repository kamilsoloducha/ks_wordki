using System.Linq;

namespace Cards.E2e.Tests.DeleteCard;

public class DeletePublicCard : DeleteCardContext
{
    public DeletePublicCard() : base()
    {
        GivenOwner.Groups.First().Cards.First().IsPrivate = false;
    }
    
    public override long GivenCardId => 1;
    public override int ExpectedSideCount => 2;
    public override int ExpectedCardsCount => 1;
    public override int ExpectedGroupsCount => 1;
    public override int ExpectedDetailsCount => 0;
}