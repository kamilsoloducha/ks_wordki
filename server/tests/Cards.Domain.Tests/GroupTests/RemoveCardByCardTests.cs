using System;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using FakeItEasy;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.Domain.Tests.GroupTests;

[TestFixture]
public class RemoveCardByCardTests
{
    [Test]
    public void RemoveExistingCard()
    {
        var sequenceGenerator = A.Fake<ISequenceGenerator>();
        A.CallTo(() => sequenceGenerator.Generate<CardId>()).ReturnsNextFromSequence(2, 3);
        A.CallTo(() => sequenceGenerator.Generate<SideId>()).Returns(2);

        var group = Builder<Group>.CreateNew().Build();
        var card = group.AddCard(
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

        group.RemoveCard(card);

        group.Cards.Should().HaveCount(1);
    }

    [Test]
    public void RemoveNotExistingCard()
    {
        var card = Builder<Card>.CreateNew().Build();
        var group = Builder<Group>.CreateNew().Build();

        Action action = () => group.RemoveCard(card);

        action.Should().Throw<Exception>();
    }
}