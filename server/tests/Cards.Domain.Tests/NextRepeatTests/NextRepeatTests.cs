using System;
using Cards.Domain2;
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
            var nextRepeat = NextRepeatMarker.New();

            nextRepeat.Date.Should().Be(new DateTime(2021, 1, 2));
        }

        [Test]
        public void NullValue()
        {
            // var nullObject = NextRepeatMarker.NullValue;

            // nullObject.Value.Should().BeNull();
        }

        [Test]
        public void RestoreValue()
        {
            var dateTime = new DateTime(2021, 2, 3, 4, 5, 6);
            var nextRepeat = NextRepeatMarker.Restore(dateTime);

            nextRepeat.Date.Should().Be(new DateTime(2021, 2, 3));
        }

        [Test]
        public void RestoreNullValue()
        {
            // var nextRepeat = NextRepeatMarker.Restore(null);

            // nextRepeat.Should().Be(NextRepeat.NullValue);
        }

        [Test]
        public void OperatorEqueal()
        {
            // var nextRepeat1 = NextRepeatMarker.Restore(new DateTime(2021, 1, 2, 3, 4, 5));
            // var nextRepeat2 = NextRepeatMarker.Restore(new DateTime(2021, 1, 2, 3, 4, 5));
            // var result = nextRepeat1 == nextRepeat2;

            // result.Should().BeTrue();
        }

        [Test]
        public void OperatorEqueal2()
        {
            // var nextRepeat1 = NextRepeatMarker.Restore(new DateTime(2021, 1, 2, 3, 4, 5));
            // var nextRepeat2 = NextRepeatMarker.Restore(new DateTime(2021, 1, 2, 3, 4, 6));
            // var result = nextRepeat1 == nextRepeat2;

            // result.Should().BeTrue();
        }

        [Test]
        public void OperatorEqueal3()
        {
            // var nextRepeat1 = NextRepeatMarker.Restore(new DateTime(2021, 1, 2));
            // var nextRepeat2 = NextRepeatMarker.Restore(new DateTime(2021, 1, 2));
            // var result = nextRepeat1 == nextRepeat2;

            // result.Should().BeTrue();
        }
    }
}