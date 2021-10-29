namespace Cards.Domain
{
    public class CreateCardBackValueRule : StringValueHaveToBeDefined
    {
        public CreateCardBackValueRule(string value) : base(value, "Back value")
        {
        }
    }
}