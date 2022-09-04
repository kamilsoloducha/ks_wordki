using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Cards.Application.Queries.Models;
using Cards.Application.Services;
using MediatR;

namespace Cards.Application.Queries;

public class SearchGroups
{
    internal class QueryHandler : IRequestHandler<Query, IEnumerable<GroupSummaryDto>>
    {
        private readonly IQueryRepository _queryRepository;
        private readonly IHashIdsService _hash;

        public QueryHandler(IQueryRepository queryRepository, IHashIdsService hash)
        {
            _queryRepository = queryRepository;
            _hash = hash;
        }

        public async Task<IEnumerable<GroupSummaryDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = SearchGroupsQuery.Create(request.Name, request.PageNumber, request.PageSize);
            var groups = await _queryRepository.GetGroupSummaries(query, cancellationToken);
            return groups.Select(x => ToDto(x, _hash));
        }

        private GroupSummaryDto ToDto(GroupSummary groupSummary, IHashIdsService hashIds)
            => new GroupSummaryDto
            {
                Id = hashIds.GetHash(groupSummary.Id),
                Name = groupSummary.Name,
                Front = groupSummary.Front,
                Back = groupSummary.Back,
                CardsCount = groupSummary.CardsCount
            };
    }
    public class Query : IRequest<IEnumerable<GroupSummaryDto>>
    {
        public string Name { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GroupSummaryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Front { get; set; }
        public int Back { get; set; }
        public int CardsCount { get; set; }
    }
}

public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        var visitor = new ParameterReplaceVisitor(right.Parameters[0], left.Parameters[0]);

        var rewritternRight = visitor.Visit(right.Body);
        var exprBody = rewritternRight != null ? Expression.AndAlso(left.Body, rewritternRight) : left.Body;
        var finalExpr = Expression.Lambda<Func<T, bool>>(exprBody, left.Parameters);

        return finalExpr;
    }

    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        var visitor = new ParameterReplaceVisitor(right.Parameters[0], left.Parameters[0]);

        var rewritternRight = visitor.Visit(right.Body);
        var exprBody = rewritternRight != null ? Expression.OrElse(left.Body, rewritternRight) : left.Body;
        var finalExpr = Expression.Lambda<Func<T, bool>>(exprBody, left.Parameters);

        return finalExpr;
    }

    public static Expression<Func<TSource, TResult>> Compose<TSource, TIntermediate, TResult>(this Expression<Func<TSource, TIntermediate>> first, Expression<Func<TIntermediate, TResult>> second)
    {
        var param = Expression.Parameter(typeof(TSource));

        var intermediateValue = first.Body.ReplaceParamter(first.Parameters[0], param);
        var body = second.Body.ReplaceParamter(second.Parameters[0], intermediateValue);
        return Expression.Lambda<Func<TSource, TResult>>(body, param);
    }

    public static Expression ReplaceParamter(this Expression expression, ParameterExpression toReplace, Expression newExpression)
    {
        return new ParameterReplaceVisitor(toReplace, newExpression).Visit(expression);
    }
}

internal class ParameterReplaceVisitor : ExpressionVisitor
{
    private readonly ParameterExpression _from;
    private readonly Expression _to;
    public ParameterReplaceVisitor(ParameterExpression from, Expression to)
    {
        _from = from;
        _to = to;
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        return node == _from ? _to : node;
    }
}