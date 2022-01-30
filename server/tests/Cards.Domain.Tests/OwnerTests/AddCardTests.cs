using System;
using System.Linq;
using Cards.Domain2;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Cards.Domain.Tests.Owner
{
    [TestFixture]
    public class AddCardTests
    {
        private Domain2.Owner _owner;
        private Mock<ISequenceGenerator> _sequenceGeneratorMock;
        private long _groupIdValue = 100;
        private long _cardIdValue = 100;

        [SetUp]
        public void Setup()
        {
            _sequenceGeneratorMock = new Mock<ISequenceGenerator>();
            _sequenceGeneratorMock.Setup(x => x.Generate<Domain2.GroupId>()).Returns(_groupIdValue);
            _sequenceGeneratorMock.Setup(x => x.Generate<Domain2.CardId>()).Returns(_cardIdValue);

            _owner = Domain2.Owner.Restore(
                Domain2.OwnerId.Restore(
                    Guid.Parse("227682f3-5c39-4fff-8e4f-61880795d8f5")
                )
            );

            _owner.AddGroup(
                Domain2.GroupName.Create("simple group name"),
                Domain2.Language.Create(1),
                Domain2.Language.Create(2),
                _sequenceGeneratorMock.Object);
        }

        [Test]
        public void AddCard_Simple()
        {
            var groupId = Domain2.GroupId.Restore(_groupIdValue);
            var frontValue = Label.Create("front value");
            var backValue = Label.Create("back value");
            var frontExample = "front example";
            var backExample = "back example";
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
            card.Front.Comment.Text.Should().Be(frontComment.Text);
            card.Front.Example.Should().Be(frontExample);

            card.Back.Id.Value.Should().Be(201);
            card.Back.Value.Text.Should().Be(backValue.Text);
            card.Back.Comment.Text.Should().Be(backComment.Text);
            card.Back.Example.Should().Be(backExample);

            _owner.Details.Count.Should().Be(2);

            _owner.Details.Count(x => x.OwnerId == _owner.Id && x.SideId == card.FrontId).Should().Be(1);
            _owner.Details.Count(x => x.OwnerId == _owner.Id && x.SideId == card.BackId).Should().Be(1);

            foreach (var item in _owner.Details)
            {
                item.Value.Text.Should().BeEmpty();
                item.Example.Should().BeEmpty();
                item.Comment.Text.Should().BeEmpty();
                item.Counter.Should().Be(0);
                item.Drawer.Value.Should().Be(1);
                item.Drawer.CorrectRepeat.Should().Be(0);
                item.LessonIncluded.Should().BeFalse();
                item.NextRepeat.Date.Should().Be(DateTime.MinValue);
            }

        }
    }


    [TestFixture]
    public class UpdateCardTests
    {
        private Domain2.Owner _owner;
        private Mock<ISequenceGenerator> _sequenceGeneratorMock;
        private long _groupIdValue = 100;
        private long _cardIdValue = 100;

        [SetUp]
        public void Setup()
        {
            _sequenceGeneratorMock = new Mock<ISequenceGenerator>();
            _sequenceGeneratorMock.Setup(x => x.Generate<Domain2.GroupId>()).Returns(_groupIdValue);
            _sequenceGeneratorMock.Setup(x => x.Generate<Domain2.CardId>()).Returns(_cardIdValue);
            _sequenceGeneratorMock.SetupSequence(x => x.Generate<Domain2.SideId>()).Returns(200).Returns(201);

            _owner = Domain2.Owner.Restore(
                Domain2.OwnerId.Restore(
                    Guid.Parse("227682f3-5c39-4fff-8e4f-61880795d8f5")
                )
            );

            _owner.AddGroup(
                Domain2.GroupName.Create("simple group name"),
                Domain2.Language.Create(1),
                Domain2.Language.Create(2),
                _sequenceGeneratorMock.Object);

            _owner.AddCard(Domain2.GroupId.Restore(_groupIdValue),
                Label.Create("front value"),
                Label.Create("back value"),
                "front example",
                "back example",
                Comment.Create("front comment"),
                Comment.Create("back comment"),
                _sequenceGeneratorMock.Object);
        }

        [Test]
        public void UpdateCard_PrivateCard()
        {
            var groupId = Domain2.GroupId.Restore(_groupIdValue);
            var cardId = Domain2.CardId.Restore(_cardIdValue);
            var frontValue = Label.Create("front value updated");
            var backValue = Label.Create("back value updated");
            var frontExample = "front example updated";
            var backExample = "back example updated";
            var frontComment = Comment.Create("front comment updated");
            var backComment = Comment.Create("back comment updated");

            _owner.UpdateCard(groupId,
                cardId,
                frontValue,
                backValue,
                frontExample,
                backExample,
                frontComment,
                backComment);

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
            card.Front.Comment.Text.Should().Be(frontComment.Text);
            card.Front.Example.Should().Be(frontExample);

            card.Back.Id.Value.Should().Be(201);
            card.Back.Value.Text.Should().Be(backValue.Text);
            card.Back.Comment.Text.Should().Be(backComment.Text);
            card.Back.Example.Should().Be(backExample);

            _owner.Details.Count.Should().Be(2);

            _owner.Details.Count(x => x.OwnerId == _owner.Id && x.SideId == card.FrontId).Should().Be(1);
            _owner.Details.Count(x => x.OwnerId == _owner.Id && x.SideId == card.BackId).Should().Be(1);

            foreach (var item in _owner.Details)
            {
                item.Value.Text.Should().BeEmpty();
                item.Example.Should().BeEmpty();
                item.Comment.Text.Should().BeEmpty();
                item.Counter.Should().Be(0);
                item.Drawer.Value.Should().Be(1);
                item.Drawer.CorrectRepeat.Should().Be(0);
                item.LessonIncluded.Should().BeFalse();
                item.NextRepeat.Date.Should().Be(DateTime.MinValue);
            }

        }

        [Test]
        public void UpdateCard_PublicCard()
        {
            var groupId = Domain2.GroupId.Restore(_groupIdValue);
            var cardId = Domain2.CardId.Restore(_cardIdValue);
            var frontValue = Label.Create("front value updated");
            var backValue = Label.Create("back value updated");
            var frontExample = "front example updated";
            var backExample = "back example updated";
            var frontComment = Comment.Create("front comment updated");
            var backComment = Comment.Create("back comment updated");

            _owner.UpdateCard(groupId,
                cardId,
                frontValue,
                backValue,
                frontExample,
                backExample,
                frontComment,
                backComment);

            var card = _owner.Groups.Single().Cards.Single();
            card.Id.Value.Should().Be(_cardIdValue);

            card.Front.Should().NotBeNull();
            card.FrontId.Value.Should().Be(200);

            card.Back.Should().NotBeNull();
            card.BackId.Value.Should().Be(201);

            card.IsPrivate.Should().BeTrue();

            var front = card.Front;

            card.Front.Id.Value.Should().Be(200);
            card.Front.Value.Text.Should().Be("front value");
            card.Front.Comment.Text.Should().Be("front comment");
            card.Front.Example.Should().Be("front example");

            card.Back.Id.Value.Should().Be(201);
            card.Back.Value.Text.Should().Be("back value");
            card.Back.Comment.Text.Should().Be("back comment");
            card.Back.Example.Should().Be("back example");

            _owner.Details.Count.Should().Be(2);

            _owner.Details.Count(x => x.OwnerId == _owner.Id && x.SideId == card.FrontId).Should().Be(1);
            _owner.Details.Count(x => x.OwnerId == _owner.Id && x.SideId == card.BackId).Should().Be(1);

            foreach (var item in _owner.Details)
            {
                item.Value.Text.Should().BeEmpty();
                item.Example.Should().BeEmpty();
                item.Comment.Text.Should().BeEmpty();
                item.Counter.Should().Be(0);
                item.Drawer.Value.Should().Be(1);
                item.Drawer.CorrectRepeat.Should().Be(0);
                item.LessonIncluded.Should().BeFalse();
                item.NextRepeat.Date.Should().Be(DateTime.MinValue);
            }

        }
    }
}