using System.Collections.Generic;

namespace E2e.Model.Tests.Model.Cards;

public partial class Group
{
    public Group()
    {
        Cards = new HashSet<Card>();
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public long? ParentId { get; set; }
    public string Front { get; set; }
    public string Back { get; set; }
    public long? OwnerId { get; set; }

    public virtual Owner Owner { get; set; }
    public virtual ICollection<Card> Cards { get; set; }
}