using System;

#nullable disable

namespace Api.Tests.Model.Cards
{
    public partial class Grouptolesson
    {
        public Guid? OwnerId { get; set; }
        public long? Id { get; set; }
        public string Name { get; set; }
        public int? Front { get; set; }
        public int? Back { get; set; }
        public long? FrontCount { get; set; }
        public long? BackCount { get; set; }
    }
}
