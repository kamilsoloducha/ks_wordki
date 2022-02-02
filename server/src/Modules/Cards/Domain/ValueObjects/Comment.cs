namespace Cards.Domain
{
    public readonly struct Comment
    {
        public string Text { get; }

        private Comment(string text)
        {
            Text = text;
        }

        public static Comment Create(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new Comment(string.Empty);

            var trimmedText = text.Trim();

            return new Comment(trimmedText);
        }
    }
}