using System.Threading;
using System.Threading.Tasks;
using Api.Configuration;
using Application.Services;
using Cards.Application.Commands;
using Cards.Application.Features.Cards;
using Cards.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("cards")]
public class CardsController : BaseController
{
    private readonly IHashIdsService _hashIds;

    public CardsController(IMediator mediator, IHashIdsService hashIds) : base(mediator)
    {
        _hashIds = hashIds;
    }

    [HttpPost("add/{groupId}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> Add([FromRoute] string groupId, [FromBody] Model.Requests.AddCard request,
        CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        if (!_hashIds.TryGetLongId(groupId, out var unHashedGroupId))
            return BadRequest();

        var addCardCommand = new AddCard.Command(
            userId,
            unHashedGroupId,
            new AddCard.CardSide(request.Front.Value, request.Front.Example, request.Front.IsUsed),
            new AddCard.CardSide(request.Back.Value, request.Back.Example, request.Back.IsUsed),
            request.Comment);

        var result = await Mediator.Send(addCardCommand, cancellationToken);

        if (!result.IsCorrect)
        {
            return BadRequest(result.Error);
        }

        var cardId = _hashIds.GetHash(result.Response);
        return Created($"{groupId}/{cardId}", cardId);
    }

    [HttpPost("add/extension")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize(Policy = AuthorizationExtensions.ChromeExtensionPolicy)]
    public async Task<IActionResult> Add(Model.Requests.AddCardFromExtension request,
        CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        var addCardCommand = new AddCardExtenstion.Command(userId, request.Value);

        var result = await Mediator.Send(addCardCommand, cancellationToken);

        return result.IsCorrect ? NoContent() : BadRequest(result.Error);
    }

    [HttpPost("add/file/{groupId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> Add([FromRoute] string groupId, Model.Requests.AddCardsFromFile request,
        CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        if (!_hashIds.TryGetLongId(groupId, out var unHashedGroupId))
            return BadRequest();

        var addCardsCommand = new AddCardsFromFile.Command(userId, unHashedGroupId, request.Content,
            request.ItemSeparator, request.ElementSeparator, request.ItemsOrder);

        var result = await Mediator.Send(addCardsCommand, cancellationToken);

        return result.IsCorrect ? NoContent() : BadRequest(result.Error);
    }


    [HttpPut("update/{groupId}/{cardId}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> Update(
        [FromRoute] string groupId,
        [FromRoute] string cardId,
        Model.Requests.UpdateCard request, CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        if (!_hashIds.TryGetLongId(groupId, out var unHashedGroupId))
            return BadRequest();

        if (!_hashIds.TryGetLongId(cardId, out var unHashedCardId))
            return BadRequest();

        var updateCardCommand = new UpdateCard.Command(userId, unHashedGroupId, unHashedCardId,
            new UpdateCard.CardSide(request.Front.Value, request.Front.Example, request.Front.IsUsed,
                request.Front.IsTicked),
            new UpdateCard.CardSide(request.Back.Value, request.Back.Example, request.Back.IsUsed,
                request.Back.IsTicked),
            request.Comment);

        var result = await Mediator.Send(updateCardCommand, cancellationToken);

        if (!result.IsCorrect)
            return BadRequest(result.Error);

        var newCardId = _hashIds.GetHash(result.Response);
        return Created($"{groupId}/{newCardId}", newCardId);
    }

    [HttpDelete("delete/{groupId}/{cardId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> Delete(
        [FromRoute] string groupId, [FromRoute] string cardId, CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        if (!_hashIds.TryGetLongId(groupId, out var unHashedGroupId))
            return BadRequest();

        if (!_hashIds.TryGetLongId(cardId, out var unHashedCardId))
            return BadRequest();

        var deleteCardCommand = new DeleteCard.Command(userId, unHashedGroupId, unHashedCardId);

        var result = await Mediator.Send(deleteCardCommand, cancellationToken);

        return result.IsCorrect ? NoContent() : BadRequest(result.Error);
    }

    [HttpPut("tick/{sideId}")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> Tick([FromRoute] string sideId, CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        if (!_hashIds.TryGetLongId(sideId, out var unHashedSideId))
            return BadRequest();

        var tickCommand = new TickCard.Command(userId, unHashedSideId);

        var result = await Mediator.Send(tickCommand, cancellationToken);

        return result.IsCorrect ? NoContent() : BadRequest(result.Error);
    }

    [HttpPut("append")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> Append(Model.Requests.AppendCards request, CancellationToken cancellationToken)
    {
        if (!TryGetUserIdFromToken(out var userId)) return Unauthorized();

        if (!_hashIds.TryGetLongId(request.GroupId, out var unHashedGroupId))
            return BadRequest();

        var appendCardsCommand = new AppendCards.Command(userId, unHashedGroupId, request.Count, request.Language);
        var result = await Mediator.Send(appendCardsCommand, cancellationToken);

        return result.IsCorrect ? NoContent() : BadRequest(result.Error);
    }

    [HttpGet("{ownerId}/{groupId}")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> Get([FromRoute] GetCardSummaries.Query query, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(query, cancellationToken));

    [HttpGet("{groupId}")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> Get([FromRoute] GetCards.Query query, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(query, cancellationToken));

    [HttpGet("{ownerId}/{groupId}/{cardId}")]
    [Authorize(Policy = AuthorizationExtensions.LoginUserPolicy)]
    public async Task<IActionResult> Get([FromRoute] GetCardSummary.Query query, CancellationToken cancellationToken)
        => Ok(await Mediator.Send(query, cancellationToken));


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