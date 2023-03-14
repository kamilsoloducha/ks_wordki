using Application.Services;
using Cards.Domain.OwnerAggregate;

namespace Cards.Application.Queries.Models;

public static class Mapper
{
    public static CardSummaryDto ToDto(Card card, IHashIdsService hash)
        => new(
            hash.GetHash(card.Id),
            new SideSummaryDto((int)card.FrontDetails.SideType, card.Front.Label.Text, card.Front.Example.Text,
                string.Empty, card.FrontDetails.Drawer.Value, card.FrontDetails.IsQuestion,
                card.FrontDetails.IsTicked),
            new SideSummaryDto((int)card.BackDetails.SideType, card.Back.Label.Text, card.Back.Example.Text,
                string.Empty, card.BackDetails.Drawer.Value, card.BackDetails.IsQuestion,
                card.BackDetails.IsTicked));
}