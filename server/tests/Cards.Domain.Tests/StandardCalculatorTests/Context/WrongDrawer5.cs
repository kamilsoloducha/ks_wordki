using System;

namespace Cards.Domain.Tests.StandardCalculatorTests.Context
{
    public class WrongDrawer5 : Wrong
    {
        public override int GivenCorrectRepeat => 10;
        public override int GivenCounter => 0;
        public override DateTime ExpectedNextRepeat => new DateTime(2022, 1, 2);
    }
}