using System.Threading;
using System.Threading.Tasks;
using Lessons.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("lesson")]
public class LessonController : BaseController
{
    public LessonController(IMediator mediator) : base(mediator) { }

    [HttpPost("start")]
    public async Task<IActionResult> StartLesson(StartLesson.Command command, CancellationToken cancellationToken)
        => await HandleRequest(command, cancellationToken);

    [HttpPost("answer")]
    public async Task<IActionResult> RegisterAnswer(RegisterAnswer.Command command, CancellationToken cancellationToken)
        => await HandleRequest(command, cancellationToken);
}