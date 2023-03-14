using System;

namespace Cards.Domain.Tests.StandardCalculatorTests.Context
{
    public class CorrectCorrectRepeat8 : Correct
    {
        public override int GivenCorrectRepeat => 8;
        public override int GivenCounter => 0;
        public override DateTime ExpectedNextRepeat => new DateTime(2022, 1, 1).AddDays(89);
    }
}