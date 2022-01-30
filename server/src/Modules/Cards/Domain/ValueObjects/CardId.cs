using System;

namespace Cards.Domain2
{
    public readonly struct CardId
    {
        public long Value { get; }

        private CardId(long id)
        {
            Value = id;
        }

        private static CardId New() => new CardId(0);
        internal static CardId New(ISequenceGenerator sequenceGenerator)
        {
            var value = sequenceGenerator.Generate<CardId>();
            return Restore(value);
        }
        public static CardId Restore(long id)
        {
            if (id <= 0) throw new ArgumentException(nameof(id));

            return new CardId(id);
        }

        public static bool operator ==(CardId id1, CardId id2) => id1.Value == id2.Value;
        public static bool operator !=(CardId id1, CardId id2) => id1.Value != id2.Value;

        public override bool Equals(object obj)
            => obj is CardId id ? id == this : false;

        public override int GetHashCode()
            => Value.GetHashCode();
    }
}