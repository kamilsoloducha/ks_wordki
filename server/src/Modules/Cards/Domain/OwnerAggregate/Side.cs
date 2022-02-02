namespace Cards.Domain
{
    public class Side
    {
        public SideId Id { get; private set; }
        public SideType Type { get; private set; }
        public Label Value { get; private set; }
        public string Example { get; private set; }

        private Side()
        { }

        public static Side New(
            CardId cardId,
            SideType type,
            Label value,
            string example,
            ISequenceGenerator sequenceGenerator)
        {
            return new Side
            {
                Id = SideId.New(sequenceGenerator),
                Type = type,
                Value = value,
                Example = example,
            };
        }

        internal void Update(Label value, string example)
        {
            Value = value;
            Example = example;
        }
    }
}