using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.Domain.Tests
{
    [TestFixture]
    public class UpdateTests
    {

        [Test]
        public void SimpleUpdate()
        {
            var name = GroupName.Create("groupName");
            var front = Language.Create(1);
            var back = Language.Create(2);

            var group = Builder<Group>.CreateNew().Build();

            group.Update(name, front, back);

            group.Name.Should().Be(name);
            group.Front.Should().Be(front);
            group.Back.Should().Be(back);
        }
    }
}