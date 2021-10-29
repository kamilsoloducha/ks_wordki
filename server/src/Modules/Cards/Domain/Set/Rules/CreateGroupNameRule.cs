namespace Cards.Domain
{
    public class CreateGroupNameRule : StringValueHaveToBeDefined
    {
        public CreateGroupNameRule(string value) : base(value, "Group name")
        {
        }
    }
}