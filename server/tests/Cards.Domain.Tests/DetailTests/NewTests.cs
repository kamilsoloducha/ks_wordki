using System;
using Cards.Domain.Enums;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using Domain.Utils;
using NUnit.Framework;

namespace Cards.Domain.Tests.DetailTests;

[TestFixture]
public class NewTests
{

    private Card _card;
    
    [SetUp]
    public void SetUp()
    {
        _card = Activator.CreateInstance<Card>();
        SystemClock.Override(new DateTime(2022, 1, 1));
    }

    [Test]
    public void SimpleNew()
    {
        var owner = OwnerBuilder.Default.Build();
        var side = SideBuilder.Default.Build();
        var comment = new Comment("commet");

        var details = new Details(SideType.Front, true, _card);
    }
}