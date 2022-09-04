using System;

namespace Cards.Domain.Tests.StandardCalculatorTests.Context;

public class CorrectDrawer3 : Correct
{
    public override int GivenCorrectRepeat => 2;
    public override int GivenCounter => 0;
    public override DateTime ExpectedNextRepeat => new DateTime(2022, 1, 6);
}