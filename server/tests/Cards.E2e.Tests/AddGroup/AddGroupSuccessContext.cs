
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.AddGroup
{
    public abstract class AddGroupSuccessContext
    {
        public abstract Api.Model.Requests.AddGroup GivenRequest { get; }

        public abstract Group ExpectedGroup { get; }
    }
}