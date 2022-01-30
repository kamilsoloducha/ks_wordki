using System;
using System.Linq;
using Cards.Domain2;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Cards.Domain.Tests.Owner
{
    [TestFixture]
    public class AddGroupTests
    {

        private Mock<ISequenceGenerator> _sequenceGeneratorMock;
        private long _groupIdValue = 100;
        private Domain2.Owner _owner;

        [SetUp]
        public void Setup()
        {
            _sequenceGeneratorMock = new Mock<ISequenceGenerator>();
            _sequenceGeneratorMock.Setup(x => x.Generate<Domain2.GroupId>()).Returns(_groupIdValue);

            _owner = Domain2.Owner.Restore(
                Domain2.OwnerId.Restore(
                    Guid.Parse("227682f3-5c39-4fff-8e4f-61880795d8f5")
                )
            );
        }

        [Test]
        public void Add_Simple()
        {
            var groupName = Domain2.GroupName.Create("simple group name");
            var front = Domain2.Language.Create(1);
            var back = Domain2.Language.Create(2);

            var result = _owner.AddGroup(groupName, front, back, _sequenceGeneratorMock.Object);

            _owner.Groups.Count.Should().Be(1);
            var group = _owner.Groups.Single();

            group.Id.Value.Should().Be(_groupIdValue);
            result.Should().Be(Domain2.GroupId.Restore(_groupIdValue));
            group.Name.Should().Be(groupName);
            group.Front.Should().Be(front);
            group.Back.Should().Be(back);
            group.Cards.Count.Should().Be(0);
        }
    }
}