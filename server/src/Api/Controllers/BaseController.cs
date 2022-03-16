using System.Threading;
using System.Threading.Tasks;
using Blueprints.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator Mediator;

        public BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected async Task<IActionResult> HandleRequest<TResponse>(RequestBase<TResponse> request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return result.IsCorrect ? Ok(result) : BadRequest(result);
        }
    }
}