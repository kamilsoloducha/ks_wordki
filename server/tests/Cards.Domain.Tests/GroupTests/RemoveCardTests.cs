using System;
using FakeItEasy;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.Domain.Tests
{
    [TestFixture]
    public class RemoveCardTests
    {
        [Test]
        public void RemoveExistingCard()
        {
            var sequenceGenerator = A.Fake<ISequenceGenerator>();
            A.CallTo(() => sequenceGenerator.Generate<CardId>()).ReturnsNextFromSequence(2, 3);
            A.CallTo(() => sequenceGenerator.Generate<SideId>()).Returns(2);

            var group = Builder<Group>.CreateNew().Build();
            group.AddCard(
                Label.Create("front"),
                Label.Create("back"),
                "frontExample",
                "backExample",
                sequenceGenerator);
            group.AddCard(
                Label.Create("front"),
                Label.Create("back"),
                "frontExample",
                "backExample",
                sequenceGenerator);

            group.RemoveCard(CardId.Restore(2));

            group.Cards.Should().HaveCount(1);
        }

        [Test]
        public void RemoveNotExistingCard()
        {
            var group = Builder<Group>.CreateNew().Build();

            Action action = () => group.RemoveCard(CardId.Restore(1));

            action.Should().Throw<Exception>();
        }
    }
}