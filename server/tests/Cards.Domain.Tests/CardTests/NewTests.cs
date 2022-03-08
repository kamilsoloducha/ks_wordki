using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.Domain.Tests.CardTests
{
    [TestFixture]
    public class NewTests
    {
        private ISequenceGenerator sequenceGenerator;

        [SetUp]
        public void Setup()
        {
            sequenceGenerator = A.Fake<ISequenceGenerator>();
            A.CallTo(() => sequenceGenerator.Generate<CardId>()).Returns(2);
            A.CallTo(() => sequenceGenerator.Generate<SideId>()).ReturnsNextFromSequence(2, 3);
        }

        [Test]
        public void SimpleNew()
        {
            Label frontValue = Label.Create("frontValue");
            Label backValue = Label.Create("backValue");
            string frontExample = "frontExample";
            string backExample = "backExample";
            var card = Card.New(frontValue, backValue, frontExample, backExample, sequenceGenerator);

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
}