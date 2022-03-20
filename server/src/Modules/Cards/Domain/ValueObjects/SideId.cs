using Blueprints.Domain;

namespace Cards.Domain
{
    public readonly struct SideId
    {
        public long Value { get; }

        private SideId(long id)
        {
            Value = id;
        }

        private static SideId New() => new SideId(0);
        internal static SideId New(ISequenceGenerator sequenceGenerator)
        {
            var value = sequenceGenerator.Generate<SideId>();
            return Restore(value);
        }
        public static SideId Restore(long id)
        {
            if (id <= 0) throw new BuissnessArgumentException(nameof(id), id);

            return new SideId(id);
        }

        public static bool operator ==(SideId id1, SideId id2) => id1.Value == id2.Value;
        public static bool operator !=(SideId id1, SideId id2) => id1.Value != id2.Value;

        public override bool Equals(object obj)
            => obj is SideId id ? id == this : false;

        public override int GetHashCode()
            => Value.GetHashCode();
    }
}