using System;

namespace Cards.Domain
{
    public readonly struct Label
    {
        internal static readonly Label Empty = new Label(string.Empty);
        public string Text { get; }

        private Label(string text)
        {
            Text = text;
        }

        public static Label Create(string text)
        {
            if (text is null) throw new ArgumentException(nameof(text));

            var trimmedText = text.Trim();

            return string.IsNullOrEmpty(trimmedText) ? Empty : new Label(trimmedText);
        }
    }
}