using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.Domain.Tests.CardTests;

[TestFixture]
public class UpdateTests
{
    private Card sut;

    [SetUp]
    public void Setup()
    {
        sut = Builder<Card>.CreateNew().Build();
        sut.SetProperty(nameof(sut.Id), CardId.Restore(2));
        sut.SetProperty(nameof(sut.IsPrivate), true);
        sut.SetProperty(nameof(sut.Front), Builder<Side>.CreateNew().Build());
        sut.Front.SetProperty(nameof(sut.Front.Id), SideId.Restore(2));
        sut.SetProperty(nameof(sut.FrontId), SideId.Restore(2));
        sut.SetProperty(nameof(sut.Back), Builder<Side>.CreateNew().Build());
        sut.Back.SetProperty(nameof(sut.Back.Id), SideId.Restore(3));
        sut.SetProperty(nameof(sut.BackId), SideId.Restore(3));
    }

    [Test]
    public void SimpleUpdate()
    {
        Label frontValue = Label.Create("frontValue");
        Label backValue = Label.Create("backValue");
        var frontExample = new Example("frontExample");
        var backExample = new Example("backExample");
        sut.Update(frontValue, backValue, frontExample, backExample);

        sut.Id.Value.Should().Be(2);
        sut.FrontId.Value.Should().Be(2);
        sut.BackId.Value.Should().Be(3);

        sut.Front.Id.Value.Should().Be(2);
        sut.Front.Value.Should().Be(frontValue);
        sut.Front.Example.Should().Be(frontExample);

        sut.Back.Id.Value.Should().Be(3);
        sut.Back.Value.Should().Be(backValue);
        sut.Back.Example.Should().Be(backExample);
    }
}