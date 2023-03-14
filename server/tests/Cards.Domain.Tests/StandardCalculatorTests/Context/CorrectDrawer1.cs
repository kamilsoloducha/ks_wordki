using System;

namespace Cards.Domain.Tests.StandardCalculatorTests.Context
{
    public class CorrectDrawer1 : Correct
    {
        public override int GivenCorrectRepeat => 0;
        public override int GivenCounter => 0;
        public override DateTime ExpectedNextRepeat => new DateTime(2022, 1, 3);
    }
}