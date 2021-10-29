
using System;

namespace Cards.Domain
{
    public readonly struct GroupId
    {
        public Guid Value { get; }
        private GroupId(Guid value)
        {
            Value = value;
        }
        internal static GroupId Create()
                    => new GroupId(Guid.NewGuid());

        public static GroupId Restore(Guid id)
            => new GroupId(id);

        public static bool operator ==(GroupId id1, GroupId id2) => id1.Value == id2.Value;
        public static bool operator !=(GroupId id1, GroupId id2) => id1.Value != id2.Value;

        public override bool Equals(object obj)
            => obj is GroupId id ? id == this : false;

        public override int GetHashCode()
            => Value.GetHashCode();

    }
}