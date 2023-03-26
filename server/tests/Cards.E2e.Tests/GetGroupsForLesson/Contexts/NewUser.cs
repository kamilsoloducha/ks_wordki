using System.Collections.Generic;
using System.Linq;
using Cards.Application.Queries.Models;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.GetGroupsForLesson.Contexts;

public class NewUser : GetGroupsForLessonContext
{
    public override IEnumerable<Owner> GivenOwners { get; }
    public override IEnumerable<GroupToLessonDto> ExpectedResponse => Enumerable.Empty<GroupToLessonDto>();

    public NewUser()
    {
        GivenOwners = new[] { DataBuilder.SampleUser().Build() };
    }
}