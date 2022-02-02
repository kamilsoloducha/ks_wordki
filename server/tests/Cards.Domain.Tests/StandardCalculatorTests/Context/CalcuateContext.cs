using System;

namespace Cards.Domain.Tests.StandardCalculatorTests
{
    public abstract class CalcuateContext
    {
        public virtual DateTime GivenNow { get; } = new DateTime(2022, 1, 1);
        public virtual int GivenResult { get; }
        public virtual int GivenCorrectRepeat { get; }
        public virtual int GivenCounter { get; }

        public virtual DateTime ExpectedNextRepeat { get; }
    }

}