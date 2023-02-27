using System;
using System.Net.Http;
using System.Threading.Tasks;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Cards.E2e.Tests.DeleteGroup;

[TestFixture(typeof(DeleteGroupHappyPath))]
[TestFixture(typeof(DeleteNotExistedGroup))]
public class DeleteGroupTests<TContext> : CardsTestBase where TContext : DeleteGroupContext, new()
{
    private readonly TContext _context = new();

    [SetUp]
    public async Task Setup()
    {
        await ClearCardsSchema();

        await using var dbContext = new CardsContext();

        await dbContext.Owners.AddAsync(_context.GivenOwner);
        await dbContext.SaveChangesAsync();
    }

    [Test]
    public async Task Test()
    {
        Request = new HttpRequestMessage(HttpMethod.Delete,
            $"groups/delete/{_context.GivenUserId}/{_context.GivenGroupId}");

        await SendRequest();

        Response.Should().BeSuccessful(Response.StatusCode.ToString());

        await using var dbContext = new CardsContext();

        (await dbContext.Sides.CountAsync()).Should().Be(_context.ExpectedSideCount);
        (await dbContext.Cards.CountAsync()).Should().Be(_context.ExpectedCardsCount);
        (await dbContext.Groups.CountAsync()).Should().Be(_context.ExpectedGroupsCount);
        (await dbContext.Details.CountAsync()).Should().Be(_context.ExpectedDetailsCount);
    }
}

public abstract class DeleteGroupContext
{
    public Owner GivenOwner { get; }
    public Guid GivenUserId => CardsTestBase.UserId;
    public virtual long GivenGroupId => 1;

    protected DeleteGroupContext()
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
    public virtual int ExpectedGroupsCount => 0;
    public virtual int ExpectedDetailsCount => 0;
}

public class DeleteGroupHappyPath : DeleteGroupContext
{
    public override int ExpectedSideCount => 2;
    public override int ExpectedCardsCount => 1;
    public override int ExpectedGroupsCount => 0;
    public override int ExpectedDetailsCount => 0;
}


public class DeleteNotExistedGroup : DeleteGroupContext
{
    public override long GivenGroupId => 2;
    public override int ExpectedSideCount => 2;
    public override int ExpectedCardsCount => 1;
    public override int ExpectedGroupsCount => 1;
    public override int ExpectedDetailsCount => 2;
}