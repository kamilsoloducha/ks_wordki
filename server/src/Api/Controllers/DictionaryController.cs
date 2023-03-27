using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Configuration;
using Cards.Application.Queries;
using Cards.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("dictionary")]
public class DictionaryController : BaseController
{
    private readonly IDictionary _dictionary;
    
    public DictionaryController(IMediator mediator, IEnumerable<IDictionary> dictionaries) : base(mediator)
    {
        _dictionary = dictionaries.Last();
    }

    [HttpGet("translate")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> GetTranslation([FromQuery] Model.Requests.Translate query,
        CancellationToken cancellationToken)
    {
        var request = new GetTranslation.Query(query.Phrase);

        var response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }
    
    [HttpGet("cam")]
    public async Task<IActionResult> GetTranslationCam([FromQuery] Model.Requests.Translate query,
        CancellationToken cancellationToken)
    {
        var request = new DictionaryRequest(query.Phrase);

        var response = await _dictionary.Translate(request, cancellationToken);

        return Ok(response);
    }
    
    [HttpGet("mac")]
    public async Task<IActionResult> GetTranslationMac([FromQuery] Model.Requests.Translate query,
        CancellationToken cancellationToken)
    {
        var request = new DictionaryRequest(query.Phrase);

        var response = await _dictionary.Translate(request, cancellationToken);

        return Ok(response);
    }
}