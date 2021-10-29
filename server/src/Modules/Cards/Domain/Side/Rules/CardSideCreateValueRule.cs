namespace Cards.Domain
{
    internal class CardSideCreateValueRule : StringValueHaveToBeDefined
    {
        public CardSideCreateValueRule(string value) : base(value, nameof(CardSide.Value)) { }
    }
}