using System.Collections.Generic;

namespace Cards.Infrastructure.DataAccess.Model
{
    internal class SideState
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public string Example { get; set; }
        public string Comment { get; set; }

        public IList<DetailState> Details { get; set; }
        public CardState Card { get; set; }
    }
}