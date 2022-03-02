using FizzWare.NBuilder;

namespace Cards.Domain.Tests
{
    public static class SideBuilder
    {
        public static ISingleObjectBuilder<Side> Default
            => Builder<Side>.CreateNew();
    }

    public static class OwnerBuilder
    {
        public static ISingleObjectBuilder<Cards.Domain.Owner> Default
            => Builder<Cards.Domain.Owner>.CreateNew();
    }

    public static class DetailsBuilder
    {
        public static ISingleObjectBuilder<Detail> Default
            => Builder<Detail>.CreateNew();
    }
}