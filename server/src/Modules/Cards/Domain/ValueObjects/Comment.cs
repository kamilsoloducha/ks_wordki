namespace Cards.Domain.ValueObjects
{
    public class Comment
    {
        public string Text { get; }

        public Comment(string text)
        {
            Text = text.Trim();
        }
    }
}