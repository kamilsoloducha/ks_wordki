using System.Collections.Generic;

namespace Cards.Infrastructure.DataAccess.Model;

internal class CardState
{
    public long Id { get; set; }
    public SideState Front { get; set; }
    public SideState Back { get; set; }
    public bool IsPrivete { get; set; }

    public IList<GroupState> Groups { get; set; }
}