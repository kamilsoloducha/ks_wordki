using System;
using Domain.Utils;
using NUnit.Framework;

namespace Cards.Domain.Tests.NextRepeatMarkerTests;

[TestFixture]
public class NextRepeatTests
{

    private readonly DateTime _now = new (2021, 1, 2, 3, 4, 5);

    [SetUp]
    public void Setup()
    {
        SystemClock.Override(_now);
    }

    [Test]
    public void CreateNextRepeat()
    {
    }

    [Test]
    public void RestoreValue()
    {
        var dateTime = new DateTime(2021, 2, 3, 4, 5, 6);
    }

    [Test]
    public void OperatorEqueal()
    {
    }

    [Test]
    public void OperatorEqueal2()
    {
    }

    [Test]
    public void OperatorEqueal3()
    {
    }
}