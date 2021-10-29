namespace Cards.Domain
{
    public class CreateCardFrontValueRule : StringValueHaveToBeDefined
    {
        public CreateCardFrontValueRule(string value) : base(value, "Front value")
        {
        }
    }
}