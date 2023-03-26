namespace Cards.E2e.Tests.DeleteCard;

public class DeleteNotExistedCard : DeleteCardContext
{
    public override long GivenCardId => -1;
    public override int ExpectedSideCount => 2;
    public override int ExpectedCardsCount => 1;
    public override int ExpectedGroupsCount => 1;
    public override int ExpectedDetailsCount => 2;
}