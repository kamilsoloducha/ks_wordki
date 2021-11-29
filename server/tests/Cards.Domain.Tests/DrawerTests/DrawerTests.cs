using NUnit.Framework;
using FluentAssertions;
using System;

namespace Cards.Domain.Tests.DrawerTests
{
    [TestFixture]
    public class DrawerTests
    {

        [Test]
        public void CreateInitailDrawer()
        {
            var initial = Drawer.Initial;
            initial.Value.Should().Be(1);
        }

        [Test]
        public void CreateDefaultDrawer()
        {
            const int initValue = 3;
            var drawer = Drawer.Create(initValue);

            drawer.Value.Should().Be(initValue);
        }

        [Test]
        public void IncreseDrawer()
        {
            const int initValue = 3;
            var drawer = Drawer.Create(initValue);

            drawer = drawer.Increse();
            drawer.Value.Should().Be(initValue + 1);
        }

        [Test]
        public void IncreseDrawerWhenMaxValue()
        {
            var initValue = Drawer.MaxValue;
            var drawer = Drawer.Create(initValue);

            drawer = drawer.Increse();
            drawer.Value.Should().Be(initValue);
        }

        [Test]
        public void ExceptionWhenValueIsTooSmall()
        {
            const int initValue = 0;
            Action action = () => Drawer.Create(initValue);

            action.Should().Throw<Exception>();
        }

        [Test]
        public void ExceptionWhenValueIsTooBig()
        {
            const int initValue = 6;
            Action action = () => Drawer.Create(initValue);

            action.Should().Throw<Exception>();
        }
    }
}