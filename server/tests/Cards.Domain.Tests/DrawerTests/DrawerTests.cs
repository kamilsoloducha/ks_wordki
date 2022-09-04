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
        var initial = Drawer.New();
        initial.Value.Should().Be(1);
        initial.CorrectRepeat.Should().Be(0);

    }

    [Test]
    public void CreateDefaultDrawer()
    {
        const int initValue = 3;
        var drawer = Drawer.Create(initValue);

        drawer.Value.Should().Be(initValue + 1);
        drawer.CorrectRepeat.Should().Be(initValue);
    }

    [Test]
    public void CreateDrawerOverMaxValue()
    {
        const int initValue = 10;
        var drawer = Drawer.Create(initValue);

        drawer.Value.Should().Be(Drawer.MaxValue);
        drawer.CorrectRepeat.Should().Be(initValue);
    }

    [Test]
    public void IncreseDrawer()
    {
        const int initValue = 3;
        var drawer = Drawer.Create(initValue);

        drawer = drawer.Increase();
        drawer.Value.Should().Be(initValue + 2);
        drawer.CorrectRepeat.Should().Be(initValue + 1);
    }

    [Test]
    public void IncreseDrawerByValue()
    {
        const int initValue = 3;
        var drawer = Drawer.Create(initValue);

        drawer = drawer.Increase(2);
        drawer.Value.Should().Be(initValue + 2);
        drawer.CorrectRepeat.Should().Be(initValue + 2);
    }

    [Test]
    public void IncreseDrawerWhenMaxValue()
    {
        var initValue = Drawer.MaxValue;
        var drawer = Drawer.Create(initValue);

        drawer = drawer.Increase();
        drawer.Value.Should().Be(initValue);
        drawer.CorrectRepeat.Should().Be(initValue + 1);
    }

    [Test]
    public void ExceptionWhenValueIsTooSmall()
    {
        const int initValue = -1;
        Action action = () => Drawer.Create(initValue);

        action.Should().Throw<BuissnessArgumentException>();
    }
}