using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Api.Configuration;
using Cards.Application.Features.Dictionaries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("dictionary")]
[Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
public class DictionaryController(IMediator mediator) : BaseController(mediator)
{
    [HttpGet("diki/{searchTerm}")]
    public async Task<IActionResult> Diki([FromRoute] string searchTerm, CancellationToken cancellationToken)
    {
        var query = new DikiTranslation.Query(searchTerm);
        var result = await Mediator.Send(query, cancellationToken);
        return result is not null ? Ok(result) : StatusCode((int)HttpStatusCode.InternalServerError);
    }

    [HttpGet("cambridge/{searchTerm}")]
    public async Task<IActionResult> Cambridge([FromRoute] string searchTerm, CancellationToken cancellationToken)
    {
        var query = new CambridgeTranslation.Query(searchTerm);
        var result = await Mediator.Send(query, cancellationToken);
        return result is not null ? Ok(result) : StatusCode((int)HttpStatusCode.InternalServerError);
    }

    [HttpGet("api/{searchTerm}")]
    public async Task<IActionResult> ApiDictionary([FromRoute] string searchTerm, CancellationToken cancellationToken)
    {
        var query = new ApiDictionaryTranslation.Query(searchTerm);
        var result = await Mediator.Send(query, cancellationToken);
        return result is not null ? Ok(result) : StatusCode((int)HttpStatusCode.InternalServerError);
    }
}