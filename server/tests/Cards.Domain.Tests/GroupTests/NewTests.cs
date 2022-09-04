using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.Domain.Tests.GroupTests;

[TestFixture]
public class NewTests
{
    [Test]
    public void SimpleNew()
    {
        var name = GroupName.Create("groupName");
        var front = Language.Create(1);
        var back = Language.Create(2);
        var sequenceGenerator = A.Fake<ISequenceGenerator>();
        A.CallTo(() => sequenceGenerator.Generate<GroupId>()).Returns(2);

        var group = Group.New(name, front, back, sequenceGenerator);
        group.Cards.Should().BeEmpty();
        group.Id.Value.Should().Be(2);
        group.Name.Should().Be(name);
        group.Front.Should().Be(front);
        group.Back.Should().Be(back);
    }
}