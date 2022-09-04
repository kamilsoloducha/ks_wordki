#nullable disable

using System;

namespace Cards.E2e.Tests.Models.Cards;

public partial class Detail
{
    public Guid OwnerId { get; set; }
    public long SideId { get; set; }
    public int Drawer { get; set; }
    public int Counter { get; set; }
    public DateTime NextRepeat { get; set; }
    public bool LessonIncluded { get; set; }
    public string Comment { get; set; }
    public bool IsTicked { get; set; }

    public virtual Owner Owner { get; set; }
}