using System;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;
using FizzWare.NBuilder;

namespace Cards.E2e.Tests.DeleteCard;

public abstract class DeleteCardContext
{
    public Owner GivenOwner { get; }
    public Guid GivenUserId => CardsTestBase.UserId;
    public long GivenGroupId => 1;
    public virtual long GivenCardId => 1;

    protected DeleteCardContext()
    {
        var owner = DataBuilder.EmptyOwner().Build();
        var group = DataBuilder.EmptyGroup().Build();
        owner.Groups.Add(group);

        var card = new Card
        {
            Front = DataBuilder.FrontSide().With(x => x.Id = 1).Build(),
            Back = DataBuilder.BackSide().With(x => x.Id = 2).Build(),
            Id = 1,
            FrontId = 1,
            BackId = 2,
            IsPrivate = true
        };
        group.Cards.Add(card);

        owner.Details.Add(
            DataBuilder.Detail().With(x => x.Id = 1).With(x => x.SideId = 1).With(x => x.OwnerId = owner.Id)
                .Build());

        owner.Details.Add(
            DataBuilder.Detail().With(x => x.Id = 2).With(x => x.SideId = 2).With(x => x.OwnerId = owner.Id)
                .Build());

        GivenOwner = owner;
    }

    public virtual int ExpectedSideCount => 2;
    public virtual int ExpectedCardsCount => 1;
    public virtual int ExpectedGroupsCount => 1;
    public virtual int ExpectedDetailsCount => 0;
}