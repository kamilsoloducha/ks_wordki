using System.Collections.Generic;
using Cards.Application.Queries.Models;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;
using FizzWare.NBuilder;

namespace Cards.E2e.Tests.GetGroupSummery.Contexts
{
    internal class EmptyGroup : GetGroupSummaryContext
    {
        public override Group GivenGroup { get; }
        public override Owner GivenOwner { get; }
        public override GroupSummaryDto ExpectedResponse { get; }

        public EmptyGroup()
        {
            GivenGroup = DataBuilder.SampleGroup().Build();
            GivenOwner = DataBuilder.SampleUser().With(x => x.Groups = new List<Group>
            {
                GivenGroup
            }).Build();

            ExpectedResponse = new GroupSummaryDto(string.Empty, "GroupName", "1", "2", 0);
        }
    }
}