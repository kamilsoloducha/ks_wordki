using System;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Cards.Domain.Tests.Owner
{
    [TestFixture]
    public class UpdateGroupTests
    {
        private Domain.Owner _owner;
        private Mock<ISequenceGenerator> _sequenceGeneratorMock;
        private long _groupIdValue = 100;

        [SetUp]
        public void Setup()
        {
            _sequenceGeneratorMock = new Mock<ISequenceGenerator>();
            _sequenceGeneratorMock.Setup(x => x.Generate<Domain.GroupId>()).Returns(_groupIdValue);

            _owner = Domain.Owner.Restore(
                Domain.OwnerId.Restore(
                    Guid.Parse("227682f3-5c39-4fff-8e4f-61880795d8f5")
                )
            );

            _owner.AddGroup(
                Domain.GroupName.Create("simple group name"),
                Domain.Language.Create(1),
                Domain.Language.Create(2),
                _sequenceGeneratorMock.Object);
        }

        [Test]
        public void Update_Simple()
        {
            var groupId = Domain.GroupId.Restore(_groupIdValue);
            var groupName = Domain.GroupName.Create("test");
            var front = Domain.Language.Create(3);
            var back = Domain.Language.Create(4);

            _owner.UpdateGroup(groupId, groupName, front, back);

            _owner.Groups.Count.Should().Be(1);
            var group = _owner.Groups.Single();

            group.Id.Should().Be(groupId);
            group.Name.Should().Be(groupName);
            group.Front.Should().Be(front);
            group.Back.Should().Be(back);
            group.Cards.Count.Should().Be(0);
        }

        [Test]
        public void UpdateFailed_GroupNotFound()
        {
            var groupId = Domain.GroupId.Restore(1);
            var groupName = Domain.GroupName.Create("test");
            var front = Domain.Language.Create(3);
            var back = Domain.Language.Create(4);

            Action act = () => _owner.UpdateGroup(groupId, groupName, front, back);

            act.Should().Throw<Exception>();
        }
    }
}