using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using FakeItEasy;
using FizzWare.NBuilder;
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
    }

    [Test]
    public void RemoveNotExistingCard()
    {
        var card = Builder<Card>.CreateNew().Build();
        var group = Builder<Group>.CreateNew().Build();
    }
}