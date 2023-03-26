using System.Collections.Generic;
using Cards.Application.Queries.Models;
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.GetGroupsForLesson.Contexts;

public abstract class GetGroupsForLessonContext
{
    public abstract IEnumerable<Owner> GivenOwners { get; }

    public abstract IEnumerable<GroupToLessonDto> ExpectedResponse { get; }
}