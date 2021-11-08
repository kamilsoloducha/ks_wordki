
using System;

namespace Cards.Domain
{
    public readonly struct UserId : IEquatable<UserId>
    {
        public Guid Value { get; }
        private UserId(Guid value)
        {
            Value = value;
        }
        internal static UserId Create() => new UserId(Guid.NewGuid());
        public static UserId Restore(Guid id) => new UserId(id);

        public bool Equals(UserId other) => Value == other.Value;
        public override bool Equals(object obj) => (obj is UserId userId) && Equals(userId);
        public static bool operator ==(UserId id1, UserId id2) => Equals(id1, id2);
        public static bool operator !=(UserId id1, UserId id2) => !Equals(id1, id2);
        public override int GetHashCode() => Value.GetHashCode();
    }
}