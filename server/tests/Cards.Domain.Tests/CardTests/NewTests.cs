using Cards.Domain.Enums;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.Domain.Tests.CardTests;

[TestFixture]
public class NewTests
{
    private readonly ISequenceGenerator _sequenceGenerator = A.Fake<ISequenceGenerator>();

    [SetUp]
    public void Setup()
    {
        A.CallTo(() => _sequenceGenerator.Generate<CardId>()).Returns(2);
        A.CallTo(() => _sequenceGenerator.Generate<SideId>()).ReturnsNextFromSequence(2, 3);
    }

    [Test]
    public void SimpleNew()
    {
        var frontValue = Label.Create("frontValue");
        var backValue = Label.Create("backValue");
        const string frontExample = "frontExample";
        const string backExample = "backExample";
        var card = Card.New(frontValue, backValue, frontExample, backExample, _sequenceGenerator);

        card.Should().NotBeNull();
        card.Id.Value.Should().Be(2);
        card.FrontId.Value.Should().Be(2);
        card.BackId.Value.Should().Be(3);

        card.Front.Id.Value.Should().Be(2);
        card.Front.Value.Should().Be(frontValue);
        card.Front.Example.Should().Be(frontExample);
        card.Front.Type.Should().Be(SideType.Front);

        card.Back.Id.Value.Should().Be(3);
        card.Back.Value.Should().Be(backValue);
        card.Back.Example.Should().Be(backExample);
        card.Back.Type.Should().Be(SideType.Back);
    }
}