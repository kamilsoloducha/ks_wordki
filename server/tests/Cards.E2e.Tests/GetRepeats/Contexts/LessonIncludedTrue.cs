using System.Collections.Generic;
using Cards.Application.Queries.Models;

namespace Cards.E2e.Tests.GetRepeats.Contexts;

internal class LessonIncludedTrue : LessonIncluded
{
    protected override bool GivenLessonIncluded => true;
    protected override string GivenLanguages => "1";
    public override IEnumerable<RepeatDto> ExpectedResponse { get; }

    public LessonIncludedTrue()
    {
        ExpectedResponse = new[]
        {
            new RepeatDto(string.Empty, 1, 2, "FrontValue", "FrontExample", "1", "BackValue", "BackExample", "2")
        };
    }
}