using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Requests;
using FluentValidation;

namespace Users.Application.Queries;

public class UserDetails
{
    internal class QueryHandler : RequestHandlerBase<Query, Response>
    {
        public override Task<ResponseBase<Response>> Handle(Query request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class Query : RequestBase<Response>
    {
        public Guid? Id { get; set; }
    }

    internal class QueryValidator : AbstractValidator<Query>
    {
        public QueryValidator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }

    public class Response
    {

    }

}