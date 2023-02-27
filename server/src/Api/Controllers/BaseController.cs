using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public abstract class BaseController : ControllerBase
{
    protected readonly IMediator Mediator;

    protected BaseController(IMediator mediator)
    {
        Mediator = mediator;
    }

    protected async Task<IActionResult> HandleRequest<TResponse>(RequestBase<TResponse> request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return result.IsCorrect ? Ok(result) : BadRequest(result);
    }

    protected bool TryGetUserIdFromToken(out Guid guid)
    {
        var tokenValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(tokenValue, out guid);
    }
}