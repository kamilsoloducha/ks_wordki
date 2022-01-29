using System;

namespace Cards.Domain2
{
    public readonly struct GroupId
    {
        public long Value { get; }

        private GroupId(long id)
        {
            Value = id;
        }

        private static GroupId New() => new GroupId(0);
        internal static GroupId New(ISequenceGenerator sequenceGenerator)
        {
            var value = sequenceGenerator.Generate<GroupId>();
            return Restore(value);
        }
        public static GroupId Restore(long id)
        {
            if (id <= 0) throw new ArgumentException(nameof(id));

            return new GroupId(id);
        }

        public static bool operator ==(GroupId id1, GroupId id2) => id1.Value == id2.Value;
        public static bool operator !=(GroupId id1, GroupId id2) => id1.Value != id2.Value;

        public override bool Equals(object obj)
            => obj is GroupId id ? id == this : false;

        public override int GetHashCode()
            => Value.GetHashCode();
    }
}