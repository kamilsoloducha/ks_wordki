using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cards.Application.Abstraction;
using Cards.Application.Queries.Models;
using Cards.Application.Services;
using Cards.Domain.Enums;
using Cards.Domain.OwnerAggregate;
using Cards.Domain.ValueObjects;
using Cards.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Cards.Infrastructure.Repository;

internal class QueryRepository : IQueryRepository
{

    #region Queries

    private const string CardSummaryQuery = """
                                            SELECT
                                            o."UserId" AS "UserId",
                                            c."Id" as "CardId",
                                            g."Id" as "GroupId",
                                            g."Name" AS "GroupName",
                                            g."Front" AS "Front",
                                            g."Back" AS "Back",
                                            f."Label" AS "FrontValue",
                                            f."Example" AS "FrontExample",
                                            b."Label" AS "BackValue",
                                            b."Example" AS "BackExample",
                                            df."Drawer" AS "FrontDrawer",
                                            df."IsQuestion" AS "FrontIsQuestion",
                                            df."IsTicked" AS "FrontIsTicked",
                                            bd."Drawer" AS "BackDrawer",
                                            bd."IsQuestion" AS "BackIsQuestion",
                                            bd."IsTicked" AS "BackIsTicked"
                                            FROM cards."Cards" c
                                            RIGHT JOIN cards."Details" df ON df."CardId" = c."Id" AND df."SideType" = 1
                                            RIGHT JOIN cards."Details" bd ON bd."CardId" = c."Id" AND bd."SideType" = 2
                                            JOIN cards."Sides" f ON f."Id" = c."FrontId"
                                            JOIN cards."Sides" b ON b."Id" = c."BackId"
                                            JOIN cards."Groups" g ON g."Id" = c."GroupId"
                                            JOIN cards."Owners" o ON o."Id" = g."OwnerId"
                                            """;

    private const string GroupSummaryQuery = """
                                             SELECT 
                                               g."OwnerId" "OwnerId",
                                               g."Id" "Id",
                                               g."Name" "Name",
                                               g."Front" "Front",
                                               g."Back" "Back",
                                               COUNT(gc."CardsId") "CardsCount"
                                             FROM cards."groups" g
                                             LEFT JOIN cards.groups_cards gc ON gc."GroupsId" = g."Id"
                                             GROUP BY g."Id"
                                             """;
    
    private const string RepeatQuery = """
                                       SELECT 
                                       random() AS "Random",
                                       d."SideType" as "SideType",
                                       d."CardId" as "CardId",
                                       q."Label" as "Question",
                                       a."Label" as "Answer",
                                       q."Example" as "QuestionExample",
                                       a."Example" as "AnswerExample",
                                       d."IsQuestion" as "LessonIncluded",
                                       d."NextRepeat" as "NextRepeat",
                                       d."Drawer" as "QuestionDrawer",
                                       case when d."SideType" = 1 then g."Front" else g."Back" end as "QuestionLanguage",
                                       case when d."SideType" = 1 then g."Back" else g."Front" end as "AnswerLanguage",
                                       o."UserId" as "UserId",
                                       g."Id" as "GroupId"
                                       from cards."Details" d
                                       join cards."Cards" c ON c."Id" = d."CardId"
                                       join cards."Sides" q ON (q."Id" = c."BackId" and d."SideType" = 2) OR (q."Id" = c."FrontId" and d."SideType" = 1)
                                       join cards."Sides" a ON (a."Id" = c."FrontId" and d."SideType" = 2) OR (a."Id" = c."BackId" and d."SideType" = 1)
                                       join cards."Groups" g ON g."Id" = c."GroupId"
                                       join cards."Owners" o ON o."Id" = g."OwnerId"
                                       ORDER BY 1
                                       """;

    private const string RepeatCountQuery = """
                                            SELECT 
                                                count(0) AS "Count",
                                                date_trunc('day'::text, d."NextRepeat") AS "Date",
                                                o."UserId" AS "OwnerId"
                                            FROM cards."Details" d
                                               JOIN cards."Cards" ON "Cards"."Id" = d."CardId"
                                               JOIN cards."Groups" ON "Groups"."Id" = "Cards"."GroupId"
                                               JOIN cards."Owners" o ON o."Id" = "Groups"."OwnerId"
                                            GROUP BY "Date", o."UserId"
                                            ORDER BY "Date"
                                            """;

    private const string GroupToLessonQuery = """
                                              SELECT
                                              o."UserId" as "OwnerId",
                                              g."Id" AS "Id",
                                              g."Name" AS "Name",
                                              g."Front" AS "Front",
                                              g."Back" AS "Back",
                                              COUNT(CASE f."IsQuestion" when true then null else 1 end) as "FrontCount",
                                              COUNT(CASE b."IsQuestion" when true then null else 1 end) as "BackCount"
                                              FROM cards."Groups" g
                                              JOIN cards."Owners" o ON o."Id" = g."OwnerId"
                                              JOIN cards."Cards" c ON c."GroupId" = g."Id"
                                              JOIN cards."Details" f ON f."CardId" = c."Id" AND f."SideType" = 1
                                              JOIN cards."Details" b ON b."CardId" = c."Id" AND b."SideType" = 2
                                              GROUP BY g."Id", o."UserId"
                                              """;

    #endregion
    
    private readonly CardsContext _cardsContext;

    public QueryRepository(CardsContext cardsContext)
    {
        _cardsContext = cardsContext;
    }

    public async Task<IReadOnlyList<Repeat>> GetRepeats(
        UserId userId,
        DateTime dateTime,
        int? count,
        IEnumerable<string> questionLanguage,
        long? groupId,
        bool lessonIncluded,
        CancellationToken cancellationToken)
    {
        var languages = questionLanguage.ToArray();
        var realCount = count ?? int.MaxValue;

        var repeatQuery = _cardsContext.Database.SqlQueryRaw<Repeat>(RepeatQuery); 
        
        return lessonIncluded
            ? groupId.HasValue
                ? await repeatQuery
                    .Where(x =>
                        x.UserId == userId.Value &&
                        x.LessonIncluded == lessonIncluded &&
                        (!languages.Any() || languages.Contains(x.QuestionLanguage)) &&
                        x.NextRepeat <= dateTime &&
                        x.GroupId == groupId)
                    .Take(realCount)
                    .ToListAsync(cancellationToken)
                : await repeatQuery
                    .Where(x =>
                        x.UserId == userId.Value &&
                        x.LessonIncluded == lessonIncluded &&
                        (!languages.Any() || languages.Contains(x.QuestionLanguage)) &&
                        x.NextRepeat <= dateTime)
                    .Take(realCount)
                    .ToListAsync(cancellationToken)
            : await repeatQuery
                .Where(x =>
                    x.UserId == userId.Value &&
                    x.LessonIncluded == lessonIncluded &&
                    (!languages.Any() || languages.Contains(x.QuestionLanguage)) &&
                    x.NextRepeat == null &&
                    groupId.HasValue && x.GroupId == groupId)
                .Take(realCount)
                .ToListAsync(cancellationToken);
    }

    // public Task<int> GetDailyRepeatsCount(UserId userId, DateTime dateTime, IEnumerable<int> questionLanguage, CancellationToken cancellationToken)
    //     => _cardsContext.Repeats
    //         .CountAsync(x =>
    //                 x.OwnerId == userId.Value &&
    //                 x.NextRepeat <= dateTime &&
    //                 x.LessonIncluded == true &&
    //                 (!questionLanguage.Any() || questionLanguage.Contains(x.QuestionLanguage)),
    //             cancellationToken);

    public Task<int> GetDailyRepeatsCount(UserId userId, DateTime dateTime, string[] languages,
        CancellationToken cancellationToken)
        => _cardsContext.Details.CountAsync(
            x => x.Card.Group.Owner.UserId == userId &&
                 x.IsQuestion == true &&
                 (languages.Length == 0 || 
                  (x.SideType == SideType.Front && languages.Contains(x.Card.Group.Front)) ||
                  (x.SideType == SideType.Back && languages.Contains(x.Card.Group.Back))) &&
                 x.NextRepeat <= dateTime, cancellationToken
        );

    public Task<int> GetGroupsCount(UserId userId, CancellationToken cancellationToken)
        => _cardsContext.Groups
            .CountAsync(x => x.Owner.UserId == userId, cancellationToken);

    public Task<int> GetCardsCount(UserId userId, CancellationToken cancellationToken)
        => _cardsContext.Details
            .CountAsync(x => x.Card.Group.Owner.UserId == userId, cancellationToken);

    public async Task<IReadOnlyList<RepeatCount>> GetRepeatsCountSummary(UserId userId, DateTime dateFrom,
        DateTime dateTo, CancellationToken cancellationToken)
        => await _cardsContext.Database.SqlQueryRaw<RepeatCount>(RepeatCountQuery)
            .Where(x => x.OwnerId == userId.Value && x.Date >= dateFrom && x.Date <= dateTo)
            .ToListAsync(cancellationToken);

    public Task<int> GetNewRepeatsCount(UserId userId, string questionLanguage, long? groupId,
        CancellationToken cancellationToken) =>
        _cardsContext.Database.SqlQueryRaw<Repeat>(RepeatQuery)
            .CountAsync(x =>
                    x.UserId == userId.Value &&
                    x.LessonIncluded == true &&
                    (!groupId.HasValue || x.GroupId == groupId) &&
                    (questionLanguage == string.Empty || x.QuestionLanguage == questionLanguage),
                cancellationToken);

    public async Task<IReadOnlyList<GroupSummary>> GetGroupSummaries(Guid ownerId,
        CancellationToken cancellationToken)
        => await _cardsContext.Groups
            .Where(x => x.Owner.UserId == new UserId(ownerId))
            .Select(x => new GroupSummary(x.Id, x.Name.Text, x.Front, x.Back, x.Cards.Count))
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Card>> GetCards(UserId ownerId, long groupId, CancellationToken cancellationToken)
        => await _cardsContext.Cards
            .Where(x => 
                x.Group.Owner.UserId == ownerId &&
                x.Group.Id == groupId &&
                x.Details.Any())
            .ToListAsync(cancellationToken);

    public Task<Card> GetCard(UserId ownerId, long cardId, CancellationToken cancellationToken)
        => _cardsContext.Cards.SingleOrDefaultAsync(x => x.Group.Owner.UserId == ownerId && x.Id == cardId,
            cancellationToken);

    public async Task<IReadOnlyList<CardSummary>> GetCardSummaries(long groupId, CancellationToken cancellationToken) =>
        await _cardsContext.Database.SqlQueryRaw<CardSummary>(CardSummaryQuery)
            .Where(x => x.GroupId == groupId)
            .ToListAsync(cancellationToken);


    public Task<Group> GetGroup(Guid userId, long groupId, CancellationToken cancellationToken)
        => _cardsContext.Groups
            .Include(x => x.Cards)
            .SingleOrDefaultAsync(x => x.Id == groupId, cancellationToken);


    public async Task<IReadOnlyList<GroupToLesson>> GetGroups(Guid ownerId, CancellationToken cancellationToken)
        => await _cardsContext.Database.SqlQueryRaw<GroupToLesson>(GroupToLessonQuery)
            .Where(x => x.OwnerId == ownerId && (x.FrontCount > 0 || x.BackCount > 0))
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<RepeatCount>> GetRepeatsPerDay(Guid ownerId, DateTime start, DateTime stop,
        CancellationToken cancellationToken)
        => await _cardsContext.Database.SqlQueryRaw<RepeatCount>(RepeatCountQuery)
            .Where(x => x.OwnerId == ownerId && x.Date >= start.Date && x.Date <= stop.Date)
            .ToListAsync(cancellationToken);


    public async Task<IReadOnlyList<GroupSummary>> GetGroupSummaries(SearchGroupsQuery query,
        CancellationToken cancellationToken)
        => await _cardsContext.Database.SqlQueryRaw<GroupSummary>(GroupSummaryQuery)
            .Where(x => x.Name.Contains(query.SearchingTerm))
            .Skip(query.Skip)
            .Take(query.Take)
            .ToListAsync(cancellationToken);

    public Task<int> GetGroupSummariesCount(SearchGroupsQuery query, CancellationToken cancellationToken)
        => _cardsContext.Database.SqlQueryRaw<GroupSummary>(GroupSummaryQuery)
            .Where(x => x.Name.Contains(query.SearchingTerm))
            .CountAsync(cancellationToken);

    public async Task<IReadOnlyList<CardSummary>> SearchCards(SearchCardsQuery query,
        CancellationToken cancellationToken)
        => await _cardsContext.Database.SqlQueryRaw<CardSummary>(CardSummaryQuery)
            .Where(x => string.IsNullOrWhiteSpace(query.SearchingTerm) ||
                        x.FrontValue.Contains(query.SearchingTerm) ||
                        x.BackValue.Contains(query.SearchingTerm))
            .Where(x => !query.LessonIncluded.HasValue || x.FrontIsQuestion == query.LessonIncluded.Value ||
                        x.BackIsQuestion == query.LessonIncluded.Value)
            .Where(x => !query.OnlyTicked.HasValue || x.FrontIsTicked == query.OnlyTicked.Value ||
                        x.BackIsTicked == query.OnlyTicked.Value)
            .Where(x => x.UserId == query.OwnerId)
            .OrderBy(x => x.CardId)
            .Skip(query.Skip)
            .Take(query.Take)
            .ToListAsync(cancellationToken);

    public Task<int> SearchCardsCount(SearchCardsQuery query, CancellationToken cancellationToken)
        => _cardsContext.Database.SqlQueryRaw<CardSummary>(CardSummaryQuery)
            .Where(x => string.IsNullOrWhiteSpace(query.SearchingTerm) ||
                        x.FrontValue.Contains(query.SearchingTerm) ||
                        x.BackValue.Contains(query.SearchingTerm))
            .Where(x => !query.LessonIncluded.HasValue || x.FrontIsQuestion == query.LessonIncluded.Value ||
                        x.BackIsQuestion == query.LessonIncluded.Value)
            .Where(x => !query.OnlyTicked.HasValue || x.FrontIsTicked == query.OnlyTicked.Value ||
                        x.BackIsTicked == query.OnlyTicked.Value)
            .Where(x => x.UserId == query.OwnerId)
            .CountAsync(cancellationToken);

    public async Task<CardsOverview> GetCardsOverview(Guid ownerId, CancellationToken cancellationToken)
    {
        var cards = await _cardsContext.Cards
            .Where(x => x.Group.Owner.UserId == new UserId(ownerId) && x.Details.Any())
            .ToListAsync(cancellationToken);
        var details = cards.SelectMany(x => x.Details).ToList();

        return new CardsOverview
        {
            All = cards.Count,
            Ticked = details.Count(x => x.IsTicked),
            LessonIncluded = details.Count(x => x.IsQuestion),
            Drawer1 = details.Count(x => x.Drawer.Value == 1),
            Drawer2 = details.Count(x => x.Drawer.Value == 2),
            Drawer3 = details.Count(x => x.Drawer.Value == 3),
            Drawer4 = details.Count(x => x.Drawer.Value == 4),
            Drawer5 = details.Count(x => x.Drawer.Value == 5),
        };
    }


    public IEnumerable<LanguageDto> GetLanguages(UserId userId, CancellationToken cancellationToken)
        => _cardsContext.Groups.Where(x => x.Owner.UserId == userId).Select(x => x.Front)
            .Union(_cardsContext.Groups.Where(x => x.Owner.UserId == userId).Select(x => x.Back))
            .Select(x => new LanguageDto(x));
}