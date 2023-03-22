using NUnit.Framework;
using FluentAssertions;
using System;
using Cards.Domain.ValueObjects;
using Domain;

namespace Cards.Domain.Tests.DrawerTests;

[TestFixture]
public class DrawerTests
{

    [Test]
    public void CreateInitailDrawer()
    {
        var initial = new Drawer();
        initial.Value.Should().Be(1);
        initial.Correct.Should().Be(0);

    }

    [Test]
    public void CreateDefaultDrawer()
    {
        const int initValue = 3;
        var drawer = new Drawer(initValue);

        drawer.Value.Should().Be(initValue + 1);
        drawer.Correct.Should().Be(initValue);
    }

    [Test]
    public void CreateDrawerOverMaxValue()
    {
        const int initValue = 10;
        var drawer = new Drawer(initValue);

        drawer.Value.Should().Be(Drawer.MaxValue);
        drawer.Correct.Should().Be(initValue);
    }

    [Test]
    public void IncreaseDrawer()
    {
        const int initValue = 3;
        var drawer = new Drawer(initValue);

        drawer = drawer.Increase();
        drawer.Value.Should().Be(initValue + 2);
        drawer.Correct.Should().Be(initValue + 1);
    }

    [Test]
    public void IncreaseDrawerByValue()
    {
        const int initValue = 3;
        var drawer = new Drawer(initValue);

        drawer = drawer.Increase(2);
        drawer.Value.Should().Be(initValue + 2);
        drawer.Correct.Should().Be(initValue + 2);
    }

    [Test]
    public void IncreaseDrawerWhenMaxValue()
    {
        var initValue = Drawer.MaxValue;
        var drawer = new Drawer(initValue);

        drawer = drawer.Increase();
        drawer.Value.Should().Be(initValue);
        drawer.Correct.Should().Be(initValue + 1);
    }

    [Test]
    public void ExceptionWhenValueIsTooSmall()
    {
        const int initValue = -1;
        Action action = () => new Drawer(initValue);

        action.Should().Throw<BuissnessArgumentException>();
    }
}