using System.Collections.Generic;

namespace E2e.Model.Tests.Model.Cards;

public partial class Card
{
    public Card()
    {
        Details = new HashSet<Detail>();
    }

    public long Id { get; set; }
    public long? FrontId { get; set; }
    public long? BackId { get; set; }
    public long? GroupId { get; set; }

    public virtual Side Back { get; set; }
    public virtual Side Front { get; set; }
    public virtual Group Group { get; set; }
    public virtual ICollection<Detail> Details { get; set; }
}