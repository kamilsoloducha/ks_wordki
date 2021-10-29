using System;

namespace Cards.Domain
{
    public readonly struct CardId
    {
        public Guid Value { get; }

        private CardId(Guid value)
        {
            Value = value;
        }

        internal static CardId Create()
            => new CardId(Guid.NewGuid());

        public static CardId Restore(Guid id)
            => new CardId(id);

        public static bool operator ==(CardId id1, CardId id2) => id1.Value == id2.Value;
        public static bool operator !=(CardId id1, CardId id2) => id1.Value != id2.Value;

        public override bool Equals(object obj)
            => obj is CardId cardId ? cardId == this : false;

        public override int GetHashCode()
            => Value.GetHashCode();
    }
}