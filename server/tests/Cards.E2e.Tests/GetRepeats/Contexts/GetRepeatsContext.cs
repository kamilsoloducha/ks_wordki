using System.Collections.Generic;
using Cards.Application.Queries.Models;
using E2e.Model.Tests.Model.Cards;

namespace Cards.E2e.Tests.GetRepeats.Contexts;

public abstract class GetRepeatsContext
{
    protected abstract string GivenGroupId { get; }
    protected abstract int GivenCount { get; }
    protected abstract bool GivenLessonIncluded { get; }
    protected abstract string GivenLanguages { get; }
    public abstract Owner GivenOwner { get; }

    public string GivenRequest =>
        $"?GroupId={GivenGroupId}&Count={GivenCount}&Languages={GivenLanguages}&LessonIncluded={GivenLessonIncluded}";

    public abstract IEnumerable<RepeatDto> ExpectedResponse { get; }
}