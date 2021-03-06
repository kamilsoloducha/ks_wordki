using System;
using FakeItEasy;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.Domain.Tests
{
    [TestFixture]
    public class GetCardTests
    {
        [Test]
        public void GetExistingCard()
        {
            var sequenceGenerator = A.Fake<ISequenceGenerator>();
            A.CallTo(() => sequenceGenerator.Generate<CardId>()).ReturnsNextFromSequence(2);
            A.CallTo(() => sequenceGenerator.Generate<SideId>()).Returns(2);

            var group = Builder<Group>.CreateNew().Build();
            group.AddCard(
                Label.Create("front"),
                Label.Create("back"),
                "frontExample",
                "backExample",
                sequenceGenerator);

            var card = group.GetCard(CardId.Restore(2));

            card.Should().NotBeNull();
            card.Id.Value.Should().Be(2);
        }

        [Test]
        public void GetNotExistingCard()
        {
            var group = Builder<Group>.CreateNew().Build();

            Action action = () => group.GetCard(CardId.Restore(1));

            action.Should().Throw<Exception>();
        }
    }
}