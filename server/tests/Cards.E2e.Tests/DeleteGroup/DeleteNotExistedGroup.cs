namespace Cards.E2e.Tests.DeleteGroup;

public class DeleteNotExistedGroup : DeleteGroupContext
{
    public override long GivenGroupId => -1;
    public override int ExpectedSideCount => 2;
    public override int ExpectedCardsCount => 1;
    public override int ExpectedGroupsCount => 1;
    public override int ExpectedDetailsCount => 2;
}