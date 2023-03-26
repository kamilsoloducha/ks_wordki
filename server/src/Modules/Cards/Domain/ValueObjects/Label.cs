using System;

namespace Cards.Domain.ValueObjects;

public class Label
{
    public string Text { get; }

    public Label(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) throw new Exception();
        Text = text.Trim();
    }
}