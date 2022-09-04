using System;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using Domain.Utils;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.Domain.Tests.DetailTests;

[TestFixture]
public class NewTests
{

    [SetUp]
    public void SetUp()
    {
        SystemClock.Override(new DateTime(2022, 1, 1));
    }

    [Test]
    public void SimpleNew()
    {
        var owner = OwnerBuilder.Default.Build();
        var side = SideBuilder.Default.Build();
        var comment = Comment.Create("commet");

        var details = Detail.New(owner, side, comment);

        details.OwnerId.Should().Be(owner.Id);
        details.SideId.Should().Be(side.Id);
        details.Drawer.Should().Be(Drawer.New());
        details.Counter.Should().Be(0);
        details.LessonIncluded.Should().Be(false);
        details.NextRepeat.Date.Should().Be(DateTime.MinValue);
        details.Comment.Should().Be(comment);
        details.Owner.Should().Be(owner);
    }
}