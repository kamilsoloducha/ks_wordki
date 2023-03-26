using System.Threading;
using System.Threading.Tasks;
using Api.Configuration;
using Application.Services;
using Lessons.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
[Route("lesson")]
public class LessonController : BaseController
{
    private readonly IHashIdsService _hashIds;

    public LessonController(IMediator mediator, IHashIdsService hashIds) : base(mediator)
    {
        _hashIds = hashIds;
    }

    [HttpPost("start")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> StartLesson(Api.Model.Requests.StartLesson request,
        CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        var startLessonCommand = new StartLesson.Command(userId, request.LessonType);

        await Mediator.Send(startLessonCommand, cancellationToken);

        return NoContent();
    }

    [HttpPost("answer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RegisterAnswer(Api.Model.Requests.RegisterAnswer request,
        CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        if (!_hashIds.TryGetLongId(request.CardId, out long unHashCardId)) return BadRequest();

        var registerAnswerCommand = new RegisterAnswer.Command(userId, unHashCardId, request.SideType, request.Result);

        await Mediator.Send(registerAnswerCommand, cancellationToken);

        return Ok();
    }
}