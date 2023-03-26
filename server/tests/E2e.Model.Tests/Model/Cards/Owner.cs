using System;
using System.Collections.Generic;

namespace E2e.Model.Tests.Model.Cards;

public partial class Owner
{
    public Owner()
    {
        Groups = new HashSet<Group>();
    }

    public long Id { get; set; }
    public Guid UserId { get; set; }

    public virtual ICollection<Group> Groups { get; set; }
}