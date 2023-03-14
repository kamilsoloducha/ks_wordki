using MediatR;

namespace Application.Requests
{
    public abstract class RequestBase<TResponse> : IRequest<ResponseBase<TResponse>> { }
}