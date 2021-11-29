using System;
using FluentAssertions;
using NUnit.Framework;
using Utils;

namespace Cards.Domain.Tests.NextRepeatTests
{
    [TestFixture]
    public class NextRepeatTests
    {

        private readonly DateTime _now = new DateTime(2021, 1, 2, 3, 4, 5);

        [SetUp]
        public void Setup()
        {
            SystemClock.Override(_now);
        }

        [Test]
        public void CreateNextRepeat()
        {
            var nextRepeat = NextRepeat.Create();

            nextRepeat.Value.Should().Be(new DateTime(2021, 1, 2));
        }

        [Test]
        public void NullValue()
        {
            var nullObject = NextRepeat.NullValue;

            nullObject.Value.Should().BeNull();
        }

        [Test]
        public void RestoreValue()
        {
            var dateTime = new DateTime(2021, 2, 3, 4, 5, 6);
            var nextRepeat = NextRepeat.Restore(dateTime);

            nextRepeat.Value.Should().Be(new DateTime(2021, 2, 3));
        }

        [Test]
        public void RestoreNullValue()
        {
            var nextRepeat = NextRepeat.Restore(null);

            nextRepeat.Should().Be(NextRepeat.NullValue);
        }

        [Test]
        public void OperatorEqueal()
        {
            var nextRepeat1 = NextRepeat.Restore(new DateTime(2021, 1, 2, 3, 4, 5));
            var nextRepeat2 = NextRepeat.Restore(new DateTime(2021, 1, 2, 3, 4, 5));
            var result = nextRepeat1 == nextRepeat2;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorEqueal2()
        {
            var nextRepeat1 = NextRepeat.Restore(new DateTime(2021, 1, 2, 3, 4, 5));
            var nextRepeat2 = NextRepeat.Restore(new DateTime(2021, 1, 2, 3, 4, 6));
            var result = nextRepeat1 == nextRepeat2;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorEqueal3()
        {
            var nextRepeat1 = NextRepeat.Restore(new DateTime(2021, 1, 2));
            var nextRepeat2 = NextRepeat.Restore(new DateTime(2021, 1, 2));
            var result = nextRepeat1 == nextRepeat2;

            result.Should().BeTrue();
        }
    }
}