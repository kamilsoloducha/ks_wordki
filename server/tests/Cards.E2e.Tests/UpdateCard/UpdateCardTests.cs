using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cards.E2e.Tests.Utils;
using E2e.Model.Tests.Model.Cards;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Cards.E2e.Tests.UpdateCard;

[TestFixture(typeof(UpdateCardHappyPath))]
[TestFixture(typeof(UpdatePublicCard))]
public class UpdateCardTests<TContext> : CardsTestBase where TContext : UpdateCardContext, new()
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
        var request = JsonConvert.SerializeObject(_context.GivenCommand);

        Request = new HttpRequestMessage(HttpMethod.Put, "cards/update")
        {
            Content = new StringContent(request, Encoding.UTF8, "application/json")
        };

        await SendRequest();

        Response.Should().BeSuccessful(Response.StatusCode.ToString());

        await using var dbContext = new CardsContext();
        var cards = await dbContext.Cards.ToListAsync();
        cards.Should().BeEquivalentTo(_context.ExpectedCards, options =>
            options.Excluding(x => x.Groups)
                .Excluding(x => x.Back)
                .Excluding(x => x.Front)
                .Excluding(x => x.BackId)
                .Excluding(x => x.FrontId)
                .Excluding(x => x.Id)
        );

        var sides = await dbContext.Sides.ToListAsync();
        sides.Should().BeEquivalentTo(_context.ExpectedSides, options =>
            options.Excluding(x => x.CardBacks)
                .Excluding(x => x.CardFronts)
                .Excluding(x => x.Id)
        );

        var details = await dbContext.Details.ToListAsync();
        details.Should().BeEquivalentTo(_context.ExpectedDetails, options =>
            options.Excluding(x => x.Owner)
                .Excluding(x => x.SideId)
                .Excluding(x => x.OwnerId)
                .Excluding(x => x.Id)
        );
    }
}

public abstract class UpdateCardContext
{
    public Owner GivenOwner { get; }
    public int GroupId { get; } = 1;
    public int CardId { get; } = 1;

    public virtual Application.Commands.UpdateCard.Command GivenCommand => new()
    {
        UserId = CardsTestBase.UserId,
        GroupId = GroupId.ToString(),
        CardId = CardId.ToString(),
        Front = new()
        {
            Value = "NewFrontValue",
            Example = "NewFrontExample",
            IsTicked = false,
            IsUsed = false
        },
        Back = new()
        {
            Value = "NewBackValue",
            Example = "NewBackExample",
            IsTicked = false,
            IsUsed = false
        },
        Comment = "NewComment"
    };

    protected UpdateCardContext()
    {
        var owner = DataBuilder.EmptyOwner().Build();
        var group = DataBuilder.EmptyGroup().With(x => x.Id = GroupId).Build();
        owner.Groups.Add(group);

        var card = new Card
        {
            Front = DataBuilder.FrontSide().With(x => x.Id = 1).Build(),
            Back = DataBuilder.BackSide().With(x => x.Id = 2).Build(),
            Id = CardId,
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

    public virtual Card[] ExpectedCards => new[]
    {
        new Card { Id = 1, IsPrivate = true }
    };

    public virtual Side[] ExpectedSides => new[]
    {
        new Side { Id = 1, Value = "NewFrontValue", Example = "NewFrontExample", Type = 1},
        new Side { Id = 2, Value = "NewBackValue", Example = "NewBackExample", Type = 2 }
    };

    public virtual Detail[] ExpectedDetails => new[]
    {
        DataBuilder.Detail()
            .With(x => x.Id = 1)
            .With(x => x.SideId = 1)
            .With(x => x.Comment = "Comment")
            .With(x => x.LessonIncluded = false)
            .With(x => x.IsTicked = false)
            .Build(),
        DataBuilder.Detail()
            .With(x => x.Id = 2)
            .With(x => x.SideId = 2)
            .With(x => x.Comment = "Comment")
            .With(x => x.LessonIncluded = false)
            .With(x => x.IsTicked = false)
            .Build(),
    };
}

public class UpdateCardHappyPath : UpdateCardContext
{
}

public class UpdatePublicCard : UpdateCardContext
{
    public UpdatePublicCard()
    {
        GivenOwner.Groups.First().Cards.First().IsPrivate = false;
    }
    
    public override Card[] ExpectedCards => new[]
    {
        new Card { Id = 1, IsPrivate = false },
        new Card { Id = 2, IsPrivate = true }
    };

    public override Side[] ExpectedSides => new[]
    {
        new Side { Id = 1, Value = "FrontValue", Example = "FrontExample", Type = 1},
        new Side { Id = 2, Value = "BackValue", Example = "BackExample", Type = 2 },
        new Side { Id = 3, Value = "NewFrontValue", Example = "NewFrontExample", Type = 1},
        new Side { Id = 4, Value = "NewBackValue", Example = "NewBackExample", Type = 2 }
    };

    public override Detail[] ExpectedDetails => new[]
    {
        DataBuilder.Detail()
            .With(x => x.Id = 1)
            .With(x => x.SideId = 3)
            .With(x => x.Comment = "Comment")
            .With(x => x.LessonIncluded = false)
            .With(x => x.IsTicked = false)
            .Build(),
        DataBuilder.Detail()
            .With(x => x.Id = 2)
            .With(x => x.SideId = 4)
            .With(x => x.Comment = "Comment")
            .With(x => x.LessonIncluded = false)
            .With(x => x.IsTicked = false)
            .Build(),
    };
}