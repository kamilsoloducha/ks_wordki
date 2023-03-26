using System;

namespace Cards.Infrastructure.DataAccess.Model;

internal class DetailState
{
    public long Id { get; set; }
    public string Value { get; set; }
    public string Example { get; set; }
    public string Commnet { get; set; }
    public int Drawer { get; set; }
    public int Counter { get; set; }
    public DateTime NextRepeat { get; set; }
    public bool LessonIncluded { get; set; }

    public SideState Side { get; set; }
    public OwnerState Owner { get; set; }
}