using Cards.Domain.Services;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.Domain.Tests.GetFibonacciNumberTests
{
    [TestFixture]
    public class GetFibonacciNumberTests
    {

        [Test]
        public void Test0Number()
        {
            var value = Helpers.GetFibbonacciNumber(0);
            value.Should().Be(0);
        }

        [Test]
        public void Test1Number()
        {
            var value = Helpers.GetFibbonacciNumber(1);
            value.Should().Be(1);
        }

        [Test]
        public void Test2Number()
        {
            var value = Helpers.GetFibbonacciNumber(2);
            value.Should().Be(1);
        }

        [Test]
        public void Test3Number()
        {
            var value = Helpers.GetFibbonacciNumber(3);
            value.Should().Be(2);
        }

        [Test]
        public void Test10Number()
        {
            var value = Helpers.GetFibbonacciNumber(10);
            value.Should().Be(55);
        }
    }
}