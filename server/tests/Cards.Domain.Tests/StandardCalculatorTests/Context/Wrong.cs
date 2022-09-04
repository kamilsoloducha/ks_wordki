namespace Cards.Domain.Tests.StandardCalculatorTests.Context;

public abstract class Wrong : CalcuateContext
{
    public override int GivenResult => -1;
}