using System;
using Cards.Domain;

namespace Cards.Application.Queries.Models
{
    public class CardSummary
    {
        public Guid OwnerId { get; set; }
        public long GroupId { get; set; }
        public string GroupName { get; set; }
        public long CardId { get; set; }
        public string FrontValue { get; set; }
        public string FrontExample { get; set; }
        public int FrontLanguage { get; set; }
        public string BackValue { get; set; }
        public string BackExample { get; set; }
        public int BackLanguage { get; set; }
        public string FrontDetailsComment { get; set; }
        public int FrontDrawer { get; set; }
        public bool FrontLessonIncluded { get; set; }
        public bool FrontIsTicked { get; set; }
        public string BackDetailsComment { get; set; }
        public int BackDrawer { get; set; }
        public bool BackLessonIncluded { get; set; }
        public bool BackIsTicked { get; set; }

        public GetCardSummaries.CardSummary ToDto()
        {
            return new GetCardSummaries.CardSummary
            {
                Id = CardId,
                Front = new GetCardSummaries.SideSummary
                {
                    Type = (int)SideType.Front,
                    Value = FrontValue,
                    Example = FrontExample,
                    Comment = FrontDetailsComment,
                    Drawer = Math.Min(FrontDrawer + 1, 5),
                    IsUsed = FrontLessonIncluded,
                    IsTicked = FrontIsTicked,
                },
                Back = new GetCardSummaries.SideSummary
                {
                    Type = (int)SideType.Back,
                    Value = BackValue,
                    Example = BackExample,
                    Comment = BackDetailsComment,
                    Drawer = Math.Min(BackDrawer + 1, 5),
                    IsUsed = BackLessonIncluded,
                    IsTicked = BackIsTicked,
                },
            };
        }


    }
}