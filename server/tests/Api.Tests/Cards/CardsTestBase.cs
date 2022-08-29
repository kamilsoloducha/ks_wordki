using System;
using System.Threading.Tasks;
using Api.Tests.Model.Cards;
using Microsoft.EntityFrameworkCore;
using Wordki.Tests.E2E.Feature;

namespace Api.Tests.Cards;

public class CardsTestBase : TestBase
{
    public static Guid OwnerId = Guid.NewGuid(); 
    
    protected readonly Owner _owner = new()
    {
        Id = OwnerId
    };
    
    protected async Task ClearCardsSchema()
    {
        await using var dbContext = new CardsContext();
        await dbContext.Database.ExecuteSqlRawAsync("Delete from cards.\"owners\"");
        await dbContext.Database.ExecuteSqlRawAsync("Delete from cards.\"cards\"");

        await dbContext.Owners.AddAsync(_owner);
    }
}