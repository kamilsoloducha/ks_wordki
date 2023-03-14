using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using FakeItEasy;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.Domain.Tests.GroupTests
{
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
        }
    }
}