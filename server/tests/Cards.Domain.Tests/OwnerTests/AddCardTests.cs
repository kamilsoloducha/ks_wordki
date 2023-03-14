using System;
using System.Linq;
using Cards.Domain.Commands;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.Services;
using Cards.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Cards.Domain.Tests.OwnerTests
{
    [TestFixture]
    public class AddCardTests
    {
        private Owner _owner;

        [SetUp]
        public void Setup()
        {
            _owner = new Owner(UserId.Restore(Guid.Parse("227682f3-5c39-4fff-8e4f-61880795d8f5")));

        }

        [Test]
        public void AddCard_Simple()
        {
        
        }
    }
}