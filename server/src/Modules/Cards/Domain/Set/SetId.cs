using System;
using Cards.Domain.Exceptions;

namespace Cards.Domain
{
    public readonly struct SetId
    {
        public Guid Value { get; }
        private SetId(Guid value)
        {
            Value = value;
        }

        internal static SetId Create() => new SetId(Guid.NewGuid());

        public static SetId Restore(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new EmptyGuidException();
            }

            return new SetId(id);
        }

        public static bool operator ==(SetId id1, SetId id2) => id1.Value == id2.Value;
        public static bool operator !=(SetId id1, SetId id2) => id1.Value != id2.Value;

        public static implicit operator SetId(Guid id) => Restore(id);
        public static implicit operator Guid(SetId id) => id.Value;


        public override bool Equals(object obj) => obj is SetId id ? id == this : false;

        public override int GetHashCode() => Value.GetHashCode();
    }
}