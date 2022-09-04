using System;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace Cards.Domain.Tests.OwnerTests;

[TestFixture]
public class RestoreTests
{
    [Test]
    public void Restore_Simple()
    {
        var guid = Guid.Parse("227682f3-5c39-4fff-8e4f-61880795d8f5");
        var ownerId = OwnerId.Restore(guid);

        var owner = Owner.Restore(ownerId);

        owner.Id.Should().Be(ownerId);
        owner.Groups.Count.Should().Be(0);
        owner.Details.Count.Should().Be(0);
    }
}