using Cards.Domain.Commands;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;
using Side = Cards.Domain.OwnerAggregate.Side;

namespace Cards.Domain.Tests.CardTests
{
    [TestFixture]
    public class UpdateTests
    {
        private Card sut;

        [SetUp]
        public void Setup()
        {
            sut = Builder<Card>.CreateNew().Build();
            sut.SetProperty(nameof(sut.Id), CardId.Restore(2));
            sut.SetProperty(nameof(sut.Front), Builder<Side>.CreateNew().Build());
            sut.Front.SetProperty(nameof(sut.Front.Id), SideId.Restore(2));
            sut.SetProperty(nameof(sut.Back), Builder<Side>.CreateNew().Build());
            sut.Back.SetProperty(nameof(sut.Back.Id), SideId.Restore(3));
        }

        [Test]
        public void SimpleUpdate()
        {
            var updateCardCommand = new UpdateCard(
                new Commands.Side(new Label("frontValue"), new Example("frontExample"), "Comment", true),
                new Commands.Side(new Label("backValue"), new Example("backExample"), "Comment", true),
                true);
        
            sut.Update(updateCardCommand);

            sut.Id.Should().Be(2);
        }
    }
}