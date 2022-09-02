#nullable disable

using System;

namespace Cards.E2e.Tests.Models.Cards
{
    public partial class Cardsummary
    {
        public Guid? OwnerId { get; set; }
        public long? GroupId { get; set; }
        public string GroupName { get; set; }
        public long? CardId { get; set; }
        public string FrontValue { get; set; }
        public string FrontExample { get; set; }
        public int? FrontLanguage { get; set; }
        public string BackValue { get; set; }
        public string BackExample { get; set; }
        public int? BackLanguage { get; set; }
        public string FrontDetailsComment { get; set; }
        public int? FrontDrawer { get; set; }
        public bool? FrontLessonIncluded { get; set; }
        public bool? FrontIsTicked { get; set; }
        public string BackDetailsComment { get; set; }
        public int? BackDrawer { get; set; }
        public bool? BackLessonIncluded { get; set; }
        public bool? BackIsTicked { get; set; }
    }
}
