using System;

namespace E2e.Model.Tests.Model.Cards
{
    public partial class Detail
    {
        public int SideType { get; set; }
        public long CardId { get; set; }
        public short Drawer { get; set; }
        public short Counter { get; set; }
        public bool IsQuestion { get; set; }
        public bool IsTicked { get; set; }
        public DateTime? NextRepeat { get; set; }

        public virtual Card Card { get; set; }
    }
}
