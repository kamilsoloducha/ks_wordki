using Cards.Domain.OwnerAggregate;
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
        public static ISingleObjectBuilder<Owner> Default
            => Builder<Owner>.CreateNew();
    }

    public static class DetailsBuilder
    {
        public static ISingleObjectBuilder<Details> Default
            => Builder<Details>.CreateNew();
    }
}