using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.Requests
{
    public abstract class RequestHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, ResponseBase<TResponse>> where TRequest : IRequest<ResponseBase<TResponse>>
    {
        public abstract Task<ResponseBase<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
    }
}