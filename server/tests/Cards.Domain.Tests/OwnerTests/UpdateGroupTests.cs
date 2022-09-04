using System;
using System.Linq;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Cards.Domain.Tests.OwnerTests;

[TestFixture]
public class UpdateGroupTests
{
    private Owner _owner;
    private Mock<ISequenceGenerator> _sequenceGeneratorMock;
    private long _groupIdValue = 100;

    [SetUp]
    public void Setup()
    {
        _sequenceGeneratorMock = new Mock<ISequenceGenerator>();
        _sequenceGeneratorMock.Setup(x => x.Generate<GroupId>()).Returns(_groupIdValue);

        _owner = Owner.Restore(
            OwnerId.Restore(
                Guid.Parse("227682f3-5c39-4fff-8e4f-61880795d8f5")
            )
        );

        _owner.AddGroup(
            GroupName.Create("simple group name"),
            Language.Create(1),
            Language.Create(2),
            _sequenceGeneratorMock.Object);
    }

    [Test]
    public void Update_Simple()
    {
        var groupId = GroupId.Restore(_groupIdValue);
        var groupName = GroupName.Create("test");
        var front = Language.Create(3);
        var back = Language.Create(4);

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
        var groupId = GroupId.Restore(1);
        var groupName = GroupName.Create("test");
        var front = Language.Create(3);
        var back = Language.Create(4);

        Action act = () => _owner.UpdateGroup(groupId, groupName, front, back);

        act.Should().Throw<Exception>();
    }
}