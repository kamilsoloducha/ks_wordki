using System;

namespace Cards.Domain.Tests.StandardCalculatorTests
{
    public class CorrectDrawer5 : Correct
    {
        public override int GivenCorrectRepeat => 4;
        public override int GivenCounter => 0;
        public override DateTime ExpectedNextRepeat => new DateTime(2022, 1, 14);
    }

}