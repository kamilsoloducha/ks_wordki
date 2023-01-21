using System;
using System.Linq;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Cards.Domain.Tests.OwnerTests;

[TestFixture]
public class AddCardTests
{
    private Owner _owner;
    private Mock<ISequenceGenerator> _sequenceGeneratorMock;
    private long _groupIdValue = 100;
    private long _cardIdValue = 100;

    [SetUp]
    public void Setup()
    {
        _sequenceGeneratorMock = new Mock<ISequenceGenerator>();
        _sequenceGeneratorMock.Setup(x => x.Generate<GroupId>()).Returns(_groupIdValue);
        _sequenceGeneratorMock.Setup(x => x.Generate<CardId>()).Returns(_cardIdValue);
        _sequenceGeneratorMock.SetupSequence(x => x.Generate<SideId>())
            .Returns(200)
            .Returns(201);

        _owner = Owner.Restore(
            OwnerId.Restore(
                Guid.Parse("227682f3-5c39-4fff-8e4f-61880795d8f5")
            )
        );

        _owner.AddGroup(
            GroupName.Create("simple group name"),
            Language.Create(1),
            Language.Create(2),
            _sequenceGeneratorMock.Object);
    }

    [Test]
    public void AddCard_Simple()
    {
        var groupId = GroupId.Restore(_groupIdValue);
        var frontValue = Label.Create("front value");
        var backValue = Label.Create("back value");
        var frontExample = new Example("front example");
        var backExample = new Example("back example");
        var frontComment = Comment.Create("front comment");
        var backComment = Comment.Create("back comment");

        var result = _owner.AddCard(groupId,
            frontValue,
            backValue,
            frontExample,
            backExample,
            frontComment,
            backComment,
            _sequenceGeneratorMock.Object);

        result.Value.Should().Be(_cardIdValue);

        var card = _owner.Groups.Single().Cards.Single();
        card.Id.Value.Should().Be(_cardIdValue);

        card.Front.Should().NotBeNull();
        card.FrontId.Value.Should().Be(200);

        card.Back.Should().NotBeNull();
        card.BackId.Value.Should().Be(201);

        card.IsPrivate.Should().BeTrue();

        var front = card.Front;

        card.Front.Id.Value.Should().Be(200);
        card.Front.Value.Text.Should().Be(frontValue.Text);
        card.Front.Example.Should().Be(frontExample);

        card.Back.Id.Value.Should().Be(201);
        card.Back.Value.Text.Should().Be(backValue.Text);
        card.Back.Example.Should().Be(backExample);

        _owner.Details.Count.Should().Be(2);

        _owner.Details.Count(x => x.OwnerId == _owner.Id && x.SideId == card.FrontId).Should().Be(1);
        _owner.Details.Count(x => x.OwnerId == _owner.Id && x.SideId == card.BackId).Should().Be(1);

        foreach (var item in _owner.Details)
        {
            // item.Comment.Text.Should().Be("front comment");
            item.Counter.Should().Be(0);
            item.Drawer.Value.Should().Be(1);
            item.Drawer.CorrectRepeat.Should().Be(0);
            item.LessonIncluded.Should().BeFalse();
            item.NextRepeat.Date.Should().Be(DateTime.MinValue);
        }

    }
}