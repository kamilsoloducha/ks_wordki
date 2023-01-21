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
            new Example("frontExample"),
            new Example("backExample"),
            sequenceGenerator);
        group.AddCard(
            Label.Create("front"),
            Label.Create("back"),
            new Example("frontExample"),
            new Example("backExample"),
            sequenceGenerator);

        group.RemoveCard(CardId.Restore(2));

        group.Cards.Should().HaveCount(1);
    }

    [Test]
    public void RemoveNotExistingCard()
    {
        var group = Builder<Group>.CreateNew().Build();

        var action = () => group.RemoveCard(CardId.Restore(1));

        action.Should().NotThrow();
    }
}