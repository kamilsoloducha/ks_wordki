using Blueprints.Infrastructure.DataAccess;
using Cards.Application.Services;
using Cards.Domain;
using Cards.Infrastructure.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cards.Infrastructure
{
    public static class Module
    {
        public static IServiceCollection AddCardsInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseConfiguration>(options => configuration.GetSection(nameof(DatabaseConfiguration)).Bind(options));

            services.AddDbContext<CardsContext>();
            services.AddScoped<IOwnerRepository, CardsRepository>();
            services.AddScoped<IQueryRepository, QueryRepository>();
            services.AddScoped<ISequenceGenerator, DbSequenceGenerator>();

            return services;
        }

        // public static async Task CreateCardsDb(this IServiceProvider services)
        // {
        //     var connectionProvider = services.GetService<IConnectionStringProvider>();
        //     var sequenceGenerator = services.GetService<ISequenceGenerator>();

        //     using (var dbContext = new CardsContextNew(connectionProvider))
        //     {
        //         await dbContext.Database.EnsureDeletedAsync();
        //         await dbContext.Database.EnsureCreatedAsync();
        //         var result = await dbContext.GroupSummaries.ToListAsync();
        //     }

        //     var ownerId = OwnerId.Restore(Guid.NewGuid());
        //     Domain2.GroupId groupId;
        //     SideId sideId;

        //     using (var dbContext = new CardsContextNew(connectionProvider))
        //     {
        //         var owner = Owner.Restore(ownerId);

        //         await dbContext.Set<Owner>().AddAsync(owner);
        //         await dbContext.SaveChangesAsync();
        //     }

        //     using (var dbContext = new CardsContextNew(connectionProvider))
        //     {
        //         var owner = await dbContext.Set<Owner>().FirstOrDefaultAsync(x => x.Id == ownerId);

        //         var groupName = Domain2.GroupName.Create("name");
        //         var front = Domain2.Language.Create(1);
        //         var back = Domain2.Language.Create(2);
        //         groupId = owner.AddGroup(groupName, front, back, sequenceGenerator);

        //         dbContext.Set<Owner>().Update(owner);
        //         await dbContext.SaveChangesAsync();
        //     }

        //     using (var dbContext = new CardsContextNew(connectionProvider))
        //     {
        //         var owner = await dbContext.Set<Owner>()
        //             .Include(x => x.Groups)
        //                 .ThenInclude(x => x.Cards)
        //                 .ThenInclude(x => x.Front).AsSplitQuery()
        //             .Include(x => x.Groups)
        //                 .ThenInclude(x => x.Cards)
        //                 .ThenInclude(x => x.Back).AsSplitQuery()
        //             .Include(x => x.Details).AsSplitQuery()
        //             .FirstOrDefaultAsync(x => x.Id == ownerId);
        //         var group = owner.Groups.FirstOrDefault();

        //         for (var i = 0; i < 30; i++)
        //             owner.AddCard(group.Id,
        //                 Label.Create("front_value"),
        //                 Label.Create("back_value"),
        //                 "front_example",
        //                 "back_example",
        //                 Comment.Create("front_comment"),
        //                 Comment.Create("back_comment"),
        //                 sequenceGenerator);

        //         sideId = group.Cards.First().Front.Id;

        //         dbContext.Set<Owner>().Update(owner);
        //         await dbContext.SaveChangesAsync();
        //     }

        //     using (var dbContext = new CardsContextNew(connectionProvider))
        //     {
        //         var owner = await dbContext.Set<Owner>()
        //             .Include(x => x.Groups)
        //                 .ThenInclude(x => x.Cards)
        //                 .ThenInclude(x => x.Front).AsSplitQuery()
        //             .Include(x => x.Groups)
        //                 .ThenInclude(x => x.Cards)
        //                 .ThenInclude(x => x.Back).AsSplitQuery()
        //             .Include(x => x.Details.Where(x => x.SideId == sideId)).AsSplitQuery()
        //             .FirstOrDefaultAsync(x => x.Id == ownerId);

        //         // var cardId = owner.Groups.First().Cards.First().Id;

        //         // owner.UpdateGroup(groupId, Domain2.GroupName.Create("testest"), Domain2.Language.Create(1), Domain2.Language.Create(2));

        //         // await dbContext.SaveChangesAsync();
        //     }


        // }
    }
}