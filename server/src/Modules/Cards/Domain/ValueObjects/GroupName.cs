using System;

namespace Cards.Domain2
{
    public readonly struct GroupName
    {
        public string Text { get; }

        private GroupName(string text)
        {
            Text = text;
        }

        public static GroupName Create(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException(nameof(text));

            var trimmedText = text.Trim();

            return new GroupName(trimmedText);
        }
    }
}