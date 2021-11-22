
using System;
using Cards.Domain.Exceptions;

namespace Cards.Domain
{
    public readonly struct UserId : IEquatable<UserId>
    {
        public Guid Value { get; }
        private UserId(Guid value)
        {
            Value = value;
        }

        public static UserId Restore(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new EmptyGuidException();
            }
            return new UserId(id);
        }

        public static bool operator ==(UserId id1, UserId id2) => Equals(id1, id2);
        public static bool operator !=(UserId id1, UserId id2) => !Equals(id1, id2);

        public bool Equals(UserId other) => Value == other.Value;
        public override bool Equals(object obj) => (obj is UserId userId) && Equals(userId);

        public override int GetHashCode() => Value.GetHashCode();
    }
}