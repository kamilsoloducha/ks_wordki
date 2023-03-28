using System;

namespace Cards.Domain.Tests.StandardCalculatorTests.Context;

public abstract class CalcuateContext
{
    public DateTime GivenNow { get; } = new (2022, 1, 1);
    public abstract int GivenResult { get; }
    public abstract int GivenCorrectRepeat { get; }
    public abstract int GivenCounter { get; }

    public abstract DateTime ExpectedNextRepeat { get; }
}