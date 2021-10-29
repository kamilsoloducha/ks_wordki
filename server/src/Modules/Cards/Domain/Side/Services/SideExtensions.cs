namespace Cards.Domain
{
    public static class SideExtensions
    {
        public static Side GetSecondSide(this Side side)
            => side == Side.Back ? Side.Front : Side.Back;
    }
}