using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using FakeItEasy;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.Domain.Tests.GroupTests;

[TestFixture]
public class AddCardTests
{
    [Test]
    public void RemoveExistingCard()
    {
        var sequenceGenerator = A.Fake<ISequenceGenerator>();
        A.CallTo(() => sequenceGenerator.Generate<CardId>()).ReturnsNextFromSequence(2);
        A.CallTo(() => sequenceGenerator.Generate<SideId>()).ReturnsNextFromSequence(2, 3);

        var group = Builder<Group>.CreateNew().Build();


        var card = group.AddCard(
            Label.Create("front"),
            Label.Create("back"),
            new Example("frontExample"),
            new Example("backExample"),
            sequenceGenerator);

        group.Cards.Should().Contain(card);
        card.Id.Value.Should().Be(2);

        card.Front.Value.Text.Should().Be("front");
        card.Front.Example.Value.Should().Be("frontExample");
        card.Front.Id.Value.Should().Be(2);

        card.Back.Value.Text.Should().Be("back");
        card.Back.Example.Value.Should().Be("backExample");
        card.Back.Id.Value.Should().Be(3);
    }
}