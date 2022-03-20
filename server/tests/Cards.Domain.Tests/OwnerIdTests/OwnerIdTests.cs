using System;
using Blueprints.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.Domain.Tests.OwnerIdTests
{
    [TestFixture]
    public class OwnerIdTests
    {

        [Test]
        public void Restore()
        {
            var guid = Guid.Parse("227682f3-5c39-4fff-8e4f-61880795d8f5");
            var ownerId = Domain.OwnerId.Restore(guid);

            ownerId.Value.Should().Be(guid);
        }

        [Test]
        public void RestoreWithError()
        {
            var guid = Guid.Empty;
            Action act = () => Domain.OwnerId.Restore(guid);
            act.Should().ThrowExactly<BuissnessArgumentException>();
        }

        [Test]
        public void EqualOperator()
        {
            var guid = Guid.Parse("227682f3-5c39-4fff-8e4f-61880795d8f5");
            var ownerId1 = Domain.OwnerId.Restore(guid);
            var ownerId2 = Domain.OwnerId.Restore(guid);

            var equal = ownerId1 == ownerId2;
            equal.Should().BeTrue();
        }

        [Test]
        public void NotEqualOperator()
        {
            var ownerId1 = Domain.OwnerId.Restore(Guid.Parse("227682f3-5c39-4fff-8e4f-61880795d8f5"));
            var ownerId2 = Domain.OwnerId.Restore(Guid.Parse("227682f3-5c39-4fff-8e4f-61880795d8f6"));

            var equal = ownerId1 != ownerId2;
            equal.Should().BeTrue();
        }
    }
}