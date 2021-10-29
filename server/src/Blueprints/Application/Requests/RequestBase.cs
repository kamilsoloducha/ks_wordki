using MediatR;

namespace Blueprints.Application.Requests
{
    public abstract class RequestBase<TResponse> : IRequest<ResponseBase<TResponse>> { }
}