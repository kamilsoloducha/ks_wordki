using Cards.Domain.Enums;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.Tests.StandardCalculatorTests.Context;
using Domain.Utils;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.Domain.Tests.StandardCalculatorTests
{
    [TestFixture(typeof(WrongDrawer1))]
    [TestFixture(typeof(WrongDrawer5))]
    [TestFixture(typeof(AcceptedDrawer1))]
    [TestFixture(typeof(AcceptedDrawer5))]
    [TestFixture(typeof(CorrectDrawer1))]
    [TestFixture(typeof(CorrectDrawer3))]
    [TestFixture(typeof(CorrectDrawer5))]
    [TestFixture(typeof(CorrectCorrectRepeat8))]
    public class CalculateTests<TContext> where TContext : CalcuateContext, new()
    {
        private readonly TContext _context = new ();
        private readonly INextRepeatCalculator _sut = new StandartCalculator();

        [SetUp]
        public void Setup()
        {
            SystemClock.Override(_context.GivenNow);
        }

        [Test]
        public void Test()
        {
            var givenDetail = new Details(SideType.Back, true, null);
            var result = _sut.Calculate(givenDetail, _context.GivenResult);

            result.Should().Be(_context.ExpectedNextRepeat);
        }
    }
}