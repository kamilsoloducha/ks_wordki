using Blueprints.Domain;

namespace Cards.Domain
{
    public readonly struct DetailId
    {
        public long Value { get; }

        private DetailId(long id)
        {
            Value = id;
        }

        private static DetailId New() => new DetailId(0);
        internal static DetailId New(ISequenceGenerator sequenceGenerator)
        {
            var value = sequenceGenerator.Generate<DetailId>();
            return Restore(value);
        }
        public static DetailId Restore(long id)
        {
            if (id <= 0) throw new BuissnessArgumentException(nameof(id), id);

            return new DetailId(id);
        }

        public static bool operator ==(DetailId id1, DetailId id2) => id1.Value == id2.Value;
        public static bool operator !=(DetailId id1, DetailId id2) => id1.Value != id2.Value;

        public override bool Equals(object obj)
            => obj is DetailId id ? id == this : false;

        public override int GetHashCode()
            => Value.GetHashCode();
    }
}