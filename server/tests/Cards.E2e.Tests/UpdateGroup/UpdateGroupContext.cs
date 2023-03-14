using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;
using FizzWare.NBuilder;

namespace Cards.E2e.Tests.UpdateGroup
{
    public abstract class UpdateGroupContext
    {
        public Owner GivenOwner { get; }
        public Group GivenGroup { get; }

        public Api.Model.Requests.UpdateGroup GivenCommand => new("NewGroupName", "3", "4");

        protected UpdateGroupContext()
        {
            var owner = DataBuilder.SampleUser().Build();
            GivenGroup = DataBuilder.SampleGroup().Build();
            owner.Groups.Add(GivenGroup);

            GivenOwner = owner;
        }

        public Group ExpectedGroup => new ()
        {
            Front = GivenCommand.Front,
            Back = GivenCommand.Back,
            Name = GivenCommand.Name,
        };
    }
}