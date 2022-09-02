using System;
using System.Threading.Tasks;
using Api.Tests.Model.Cards;
using Cards.E2e.Tests.Models.Cards;
using E2e.Tests;
using Microsoft.EntityFrameworkCore;

namespace Cards.E2e.Tests;

public class CardsTestBase : TestBase
{
    public static Guid OwnerId = Guid.Parse("12345678-1234-1234-1234-1234567890AB"); 
    
    protected readonly Owner Owner = new()
    {
        Id = OwnerId
    };
    
    protected async Task ClearCardsSchema()
    {
        await using var dbContext = new CardsContext();
        await dbContext.Database.ExecuteSqlRawAsync("Delete from cards.\"owners\"");
        await dbContext.Database.ExecuteSqlRawAsync("Delete from cards.\"cards\"");

        await dbContext.Owners.AddAsync(Owner);
        await dbContext.SaveChangesAsync();
    }
}