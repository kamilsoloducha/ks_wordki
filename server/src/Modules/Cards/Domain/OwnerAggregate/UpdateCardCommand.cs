namespace Cards.Domain
{
    public class UpdateCardCommand
    {
        public Side Front { get; set; }
        public Side Back { get; set; }

        public class Side
        {
            public Label Value { get; set; }
            public string Example { get; set; }
            public Comment Comment { get; set; }
            public bool? IncludeLesson { get; set; }
            public bool IsTicked { get; set; }
        }
    }
}