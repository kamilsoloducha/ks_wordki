#nullable disable

using System.Collections.Generic;

namespace Cards.E2e.Tests.Models.Cards;

public partial class Card
{
    public Card()
    {
        GroupsCards = new HashSet<GroupsCard>();
    }

    public long Id { get; set; }
    public bool IsPrivate { get; set; }
    public long FrontId { get; set; }
    public long BackId { get; set; }

    public virtual Side Back { get; set; }
    public virtual Side Front { get; set; }
    public virtual ICollection<GroupsCard> GroupsCards { get; set; }
}