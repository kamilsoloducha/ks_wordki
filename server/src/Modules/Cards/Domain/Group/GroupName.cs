using Cards.Domain.Exceptions;

namespace Cards.Domain
{
    public readonly struct GroupName
    {
        public string Value { get; }
        private GroupName(string value)
        {
            Value = value;
        }

        public static GroupName Create(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                throw new NullGroupNameException();
            }
            return new GroupName(groupName);
        }

        public static implicit operator string(GroupName groupName) => groupName.Value;
    }
}