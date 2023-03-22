using Cards.Domain.Commands;
using Cards.Domain.Enums;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.Domain.Tests.CardTests;

[TestFixture]
public class CardsTests
{
    private Card sut;

    [SetUp]
    public void Setup()
    {
        sut = Builder.SampleCard();
    }

    [Test]
    public void SimpleUpdate()
    {
        var updateCardCommand = new UpdateCard(
            new Commands.Side(new Label("newFrontValue"), new Example("newFrontExample"), "newComment", true),
            new Commands.Side(new Label("newBackValue"), new Example("newBackExample"), "newComment", true),
            true);

        sut.Update(updateCardCommand);

        sut.Id.Should().Be(2);
        
        sut.Front.Id.Should().Be(0);
        sut.Front.Label.Should().Be(updateCardCommand.Front.Label);
        sut.Front.Example.Should().Be(updateCardCommand.Front.Example);
        
        sut.Back.Id.Should().Be(0);
        sut.Back.Label.Should().Be(updateCardCommand.Back.Label);
        sut.Back.Example.Should().Be(updateCardCommand.Back.Example);
        
        sut.FrontDetails.Counter.Value.Should().Be(10);
        sut.FrontDetails.Drawer.Value.Should().Be(1);
        sut.FrontDetails.NextRepeat.Should().NotBeNull();
        sut.FrontDetails.IsTicked.Should().BeTrue();
        sut.FrontDetails.SideType.Should().Be(SideType.Front);
        sut.FrontDetails.IsQuestion.Should().BeTrue();
        
        sut.BackDetails.Counter.Value.Should().Be(10);
        sut.BackDetails.Drawer.Value.Should().Be(1);
        sut.BackDetails.NextRepeat.Should().NotBeNull();
        sut.BackDetails.IsTicked.Should().BeTrue();
        sut.BackDetails.SideType.Should().Be(SideType.Back);
        sut.BackDetails.IsQuestion.Should().BeTrue();
    }

    [TestCase(true)]
    [TestCase(false)]
    public void TickTests(bool initialValue)
    {
        foreach (var detail in sut.Details)
        {
            detail.SetProperty(nameof(Details.IsTicked), initialValue);
        }
        
        sut.Tick();

        sut.FrontDetails.IsTicked.Should().BeTrue();
        sut.BackDetails.IsTicked.Should().BeTrue();
    }
    
    [Test]
    public void RemoveShouldRemoveAllDetails()
    {
        sut.Remove();

        sut.Details.Should().BeEmpty();
    }
}
