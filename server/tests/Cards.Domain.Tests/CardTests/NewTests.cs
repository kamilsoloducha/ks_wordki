using System;
using Cards.Domain.Enums;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.Domain.Tests.CardTests
{
    [TestFixture]
    public class NewTests
    {

        private Group _group;
    
        [SetUp]
        public void Setup()
        {
            _group = Activator.CreateInstance<Group>();
        }

        [Test]
        public void SimpleNew()
        {
            // arragne
            var frontValue = new Label("frontValue");
            var backValue = new Label("backValue");
            var frontExample = new Example("frontExample");
            var backExample = new Example("backExample");
        
            // act
            var card = new Card(frontValue, backValue, frontExample, backExample, _group);

            // assert
            card.Should().NotBeNull();
            card.Id.Should().Be(0);
            card.Front.Should().NotBeNull();
            card.Back.Should().NotBeNull();

            card.Front.Id.Should().Be(0);
            card.Front.Label.Should().Be(frontValue);
            card.Front.Example.Should().Be(frontExample);
        
            card.Back.Id.Should().Be(0);
            card.Back.Label.Should().Be(frontValue);
            card.Back.Example.Should().Be(frontExample);

            card.Details.Should().HaveCount(2);

            card.FrontDetails.Counter.Value.Should().Be(0);
            card.FrontDetails.Drawer.Value.Should().Be(0);
            card.FrontDetails.NextRepeat.Should().NotBeNull();
            card.FrontDetails.IsTicked.Should().BeFalse();
            card.FrontDetails.SideType.Should().Be(SideType.Front);
            card.FrontDetails.IsQuestion.Should().BeFalse();
        
            card.BackDetails.Counter.Value.Should().Be(0);
            card.BackDetails.Drawer.Value.Should().Be(0);
            card.BackDetails.NextRepeat.Should().NotBeNull();
            card.BackDetails.IsTicked.Should().BeFalse();
            card.BackDetails.SideType.Should().Be(SideType.Back);
            card.BackDetails.IsQuestion.Should().BeFalse();
        }
    }
}