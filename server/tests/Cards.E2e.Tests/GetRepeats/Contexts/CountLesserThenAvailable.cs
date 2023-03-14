using System.Collections.Generic;
using Cards.Application.Queries.Models;

namespace Cards.E2e.Tests.GetRepeats.Contexts;

internal class CountLesserThenAvailable : Count
{
    protected override int GivenCount => 5;
    public override IEnumerable<RepeatDto> ExpectedResponse { get; }

    public CountLesserThenAvailable()
    {
        var result = new RepeatDto[5];
        for (var i = 0; i < 5; i++)
        {
            result[i] = new RepeatDto(string.Empty, 1, 2, "FrontValue", "FrontExample", "1", "BackValue",
                "BackExample", "2");
        }

        ExpectedResponse = result;
    }
}