using System;
using System.Threading.Tasks;
using E2e.Model.Tests.Model.Cards;
using E2e.Tests;
using Microsoft.EntityFrameworkCore;

namespace Cards.E2e.Tests;

public class CardsTestBase : TestBase
{
    public static Guid UserId = new ("12345678-1234-1234-1234-1234567890ab");

    protected Owner Owner => new()
    {
        Id = UserId
    };
    
    protected async Task ClearCardsSchema()
    {
        await using var dbContext = new CardsContext();
        await dbContext.Database.ExecuteSqlRawAsync("Delete from cards.\"owners\"");
        await dbContext.Database.ExecuteSqlRawAsync("Delete from cards.\"cards\"");
        await dbContext.Database.ExecuteSqlRawAsync("Delete from cards.\"sides\"");

        
    }
}