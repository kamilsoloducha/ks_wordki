
using System;

namespace Cards.Domain
{
    public readonly struct UserId
    {
        public Guid Value { get; }
        private UserId(Guid value)
        {
            Value = value;
        }
        internal static UserId Create()
                    => new UserId(Guid.NewGuid());

        public static UserId Restore(Guid id)
            => new UserId(id);

        public static bool operator ==(UserId id1, UserId id2) => id1.Value == id2.Value;
        public static bool operator !=(UserId id1, UserId id2) => id1.Value != id2.Value;

        public override bool Equals(object obj)
            => obj is UserId id ? id == this : false;

        public override int GetHashCode()
            => Value.GetHashCode();
    }
}