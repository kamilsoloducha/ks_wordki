namespace Cards.Domain.Tests.StandardCalculatorTests
{
    public abstract class Wrong : CalcuateContext
    {
        public override int GivenResult => -1;
    }

}