using System.Collections.Generic;
using Cards.Application.Queries.Models;

namespace Cards.E2e.Tests.GetRepeats.Contexts;

internal class CountBiggerThenAvailable : Count
{
    protected override int GivenCount => 20;
    public override IEnumerable<RepeatDto> ExpectedResponse { get; }

    public CountBiggerThenAvailable()
    {
        var result = new RepeatDto[10];
        for (var i = 0; i < 10; i++)
        {
            result[i] = new RepeatDto(string.Empty, 1, 2, "FrontValue", "FrontExample", "1", "BackValue",
                "BackExample", "2");
        }

        ExpectedResponse = result;
    }
}