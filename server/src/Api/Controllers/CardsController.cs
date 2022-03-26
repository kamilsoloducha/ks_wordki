using System.Threading;
using System.Threading.Tasks;
using Api.Configuration;
using Cards.Application.Commands;
using Cards.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api
{
    [ApiController]
    [Route("cards")]
    public class CardsController : BaseController
    {
        public CardsController(IMediator mediator) : base(mediator) { }

        [HttpGet("{ownerId}/{groupId}")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Get([FromRoute] GetCardSummaries.Query query, CancellationToken cancellationToken)
            => Ok(await Mediator.Send(query, cancellationToken));

        [HttpGet("{groupId}")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Get([FromRoute] GetCards.Query query, CancellationToken cancellationToken)
            => Ok(await Mediator.Send(query, cancellationToken));

        [HttpPost("add")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Add(AddCard.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);


        [HttpPost("add/file")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Add(AddCardsFromFile.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpPut("update")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Update(UpdateCard.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpPut("tick")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Tick(TickCard.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpPut("enable")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Update(AddCardSide.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpDelete("delete/{userId}/{groupId}/{cardId}")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Delete([FromRoute] DeleteCard.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpGet("dashboard/summary/{userId}")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Get([FromRoute] GetDashboardSummary.Query query, CancellationToken cancellationToken)
            => Ok(await Mediator.Send(query, cancellationToken));

        [HttpPut("append")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Append(AppendCards.Command command, CancellationToken cancellationToken)
            => await HandleRequest(command, cancellationToken);

        [HttpPut("search")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Get(SearchCards.Query query, CancellationToken cancellationToken)
            => Ok(await Mediator.Send(query, cancellationToken));

        [HttpPut("search/count")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Get(SearchCardsCount.Query query, CancellationToken cancellationToken)
            => Ok(await Mediator.Send(query, cancellationToken));

        [HttpGet("overview/{ownerId}")]
        [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
        public async Task<IActionResult> Get([FromRoute] GetCardsOverview.Query query, CancellationToken cancellationToken)
            => Ok(await Mediator.Send(query, cancellationToken));
    }
}