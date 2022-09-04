using System;

namespace Cards.Domain.Tests.StandardCalculatorTests.Context;

public class AcceptedDrawer5 : Accepted
{
    public override int GivenCorrectRepeat => 10;
    public override int GivenCounter => 0;
    public override DateTime ExpectedNextRepeat => new DateTime(2022, 1, 3);
}