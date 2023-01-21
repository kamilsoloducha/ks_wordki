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
public class UpdateCardTests
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

        _owner.AddCard(GroupId.Restore(_groupIdValue),
            Label.Create("front value"),
            Label.Create("back value"),
            new Example("front example"),
            new Example("back example"),
            Comment.Create("front comment"),
            Comment.Create("back comment"),
            _sequenceGeneratorMock.Object);
    }

    [Test]
    public void UpdateCard_PrivateCard()
    {
        var groupId = GroupId.Restore(_groupIdValue);
        var cardId = CardId.Restore(_cardIdValue);
        var frontValue = Label.Create("front value updated");
        var backValue = Label.Create("back value updated");
        var frontExample = new Example("front example updated");
        var backExample = new Example("back example updated");
        var frontComment = Comment.Create("front comment updated");
        var backComment = Comment.Create("back comment updated");

        var command = new UpdateCardCommand
        {
            Front = new UpdateCardCommand.Side
            {
                Comment = frontComment,
                Example = frontExample,
                Value = frontValue,
                IncludeLesson = true,
                IsTicked = true
            },
            Back = new UpdateCardCommand.Side
            {
                Comment = backComment,
                Example = backExample,
                Value = backValue,
                IncludeLesson = true,
                IsTicked = true
            },
        };

        _owner.UpdateCard(groupId,
            cardId,
            command,
            _sequenceGeneratorMock.Object);

        var card = _owner.Groups.Single().Cards.Single();
        card.Id.Value.Should().Be(_cardIdValue);

        card.Front.Should().NotBeNull();
        card.FrontId.Value.Should().Be(200);

        card.Back.Should().NotBeNull();
        card.BackId.Value.Should().Be(201);

        card.IsPrivate.Should().BeTrue();

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
            item.Counter.Should().Be(0);
            item.Drawer.Value.Should().Be(1);
            item.Drawer.CorrectRepeat.Should().Be(0);
            item.LessonIncluded.Should().BeTrue();
            item.NextRepeat.Date.Should().Be(DateTime.MinValue);
        }

    }

    [Test]
    public void UpdateCard_PublicCard()
    {
        _sequenceGeneratorMock = new Mock<ISequenceGenerator>();

        _sequenceGeneratorMock.Setup(x => x.Generate<CardId>()).Returns(205);
        _sequenceGeneratorMock.SetupSequence(x => x.Generate<SideId>())
            .Returns(202)
            .Returns(203);

        var groupId = GroupId.Restore(_groupIdValue);
        var cardId = CardId.Restore(_cardIdValue);
        var frontValue = Label.Create("front value updated");
        var backValue = Label.Create("back value updated");
        var frontExample = new Example("front example updated");
        var backExample = new Example("back example updated");
        var frontComment = Comment.Create("front comment updated");
        var backComment = Comment.Create("back comment updated");

        var command = new UpdateCardCommand
        {
            Front = new UpdateCardCommand.Side
            {
                Comment = frontComment,
                Example = frontExample,
                Value = frontValue,
                IncludeLesson = true,
                IsTicked = true
            },
            Back = new UpdateCardCommand.Side
            {
                Comment = backComment,
                Example = backExample,
                Value = backValue,
                IncludeLesson = true,
                IsTicked = true
            },
        };

        _owner.Groups.SelectMany(x => x.Cards).First().IsPrivate = false;

        _owner.UpdateCard(groupId,
            cardId,
            command,
            _sequenceGeneratorMock.Object);

        var card = _owner.Groups.Single().Cards.Single();
        card.Id.Value.Should().Be(205);

        card.Front.Should().NotBeNull();
        card.FrontId.Value.Should().Be(202);

        card.Back.Should().NotBeNull();
        card.BackId.Value.Should().Be(203);

        card.IsPrivate.Should().BeTrue();

        card.Front.Id.Value.Should().Be(202);
        card.Front.Value.Text.Should().Be("front value updated");
        card.Front.Example.Value.Should().Be("front example updated");

        card.Back.Id.Value.Should().Be(203);
        card.Back.Value.Text.Should().Be("back value updated");
        card.Back.Example.Value.Should().Be("back example updated");

        _owner.Details.Count.Should().Be(2);

        _owner.Details.Count(x => x.OwnerId == _owner.Id && x.SideId == card.FrontId).Should().Be(1);
        _owner.Details.Count(x => x.OwnerId == _owner.Id && x.SideId == card.BackId).Should().Be(1);

        foreach (var item in _owner.Details)
        {
            item.Counter.Should().Be(0);
            item.Drawer.Value.Should().Be(1);
            item.Drawer.CorrectRepeat.Should().Be(0);
            item.LessonIncluded.Should().BeTrue();
            item.NextRepeat.Date.Should().Be(DateTime.MinValue);
        }

    }
}