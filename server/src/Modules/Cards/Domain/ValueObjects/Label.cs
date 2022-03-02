using System;

namespace Cards.Domain
{
    public readonly struct Label
    {
        public string Text { get; }

        private Label(string text)
        {
            Text = text;
        }

        public static Label Create(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException(nameof(text));

            var trimmedText = text.Trim();

            return new Label(trimmedText);
        }
    }
}