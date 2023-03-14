namespace Cards.E2e.Tests.DeleteGroup
{
    public class DeleteGroupHappyPath : DeleteGroupContext
    {
        public override int ExpectedSideCount => 2;
        public override int ExpectedCardsCount => 1;
        public override int ExpectedGroupsCount => 1;
        public override int ExpectedDetailsCount => 0;
    }
}