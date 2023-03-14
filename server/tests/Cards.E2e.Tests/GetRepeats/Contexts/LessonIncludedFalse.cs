using System.Collections.Generic;
using Cards.Application.Queries.Models;

namespace Cards.E2e.Tests.GetRepeats.Contexts;

internal class LessonIncludedFalse : LessonIncluded
{
    protected override bool GivenLessonIncluded => false;
    protected override string GivenLanguages => "2";
    public override IEnumerable<RepeatDto> ExpectedResponse { get; }

    public LessonIncludedFalse()
    {
        ExpectedResponse = new[]
        {
            new RepeatDto(string.Empty, 2, 2, "BackValue", "BackExample", "2", "FrontValue", "FrontExample", "1")
        };
    }
}