using System.Collections.Generic;
using Cards.Application.Queries.Models;
using Cards.Domain.Enums;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.GetCardSummaries.Contexts;

internal class GroupWithCard : GetCardSummariesContext
{
    private Group Group { get; }
    public override string GivenGroupId => Group.Id.ToString();
    public override IEnumerable<Owner> GivenOwners { get; }
    public override IEnumerable<CardSummaryDto> ExpectedResponse { get; }

    public GroupWithCard()
    {
        Group = DataBuilder.SampleGroup().Build();
        Group.Cards.Add(DataBuilder.SampleCard().Build());
        var owner = DataBuilder.SampleUser().Build();
        owner.Groups.Add(Group);
        GivenOwners = new[]
        {
            owner
        };

        ExpectedResponse = new[]
        {
            new CardSummaryDto(
                string.Empty,
                new SideSummaryDto((int)SideType.Front, "FrontValue", "FrontExample", string.Empty, 3, true, true),
                new SideSummaryDto((int)SideType.Back, "BackValue", "BackExample", string.Empty, 3, true, true)
            )
        };
    }
}

internal class GroupWithCardsVariousValues : GetCardSummariesContext
{
    private Group Group { get; }
    public override string GivenGroupId => Group.Id.ToString();
    public override IEnumerable<Owner> GivenOwners { get; }
    public override IEnumerable<CardSummaryDto> ExpectedResponse { get; }

    public GroupWithCardsVariousValues()
    {
        Group = DataBuilder.SampleGroup().Build();
        Group.Cards.Add(DataBuilder.SampleCard().Build());
        var owner = DataBuilder.SampleUser().Build();
        owner.Groups.Add(Group);
        GivenOwners = new[]
        {
            owner
        };

        ExpectedResponse = new[]
        {
            new CardSummaryDto(
                string.Empty,
                new SideSummaryDto((int)SideType.Front, "FrontValue", "FrontExample", string.Empty, 3, true, true),
                new SideSummaryDto((int)SideType.Back, "BackValue", "BackExample", string.Empty, 3, true, true)
            )
        };
    }
}