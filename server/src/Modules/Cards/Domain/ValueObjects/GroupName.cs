using System;

namespace Cards.Domain.ValueObjects;

public class GroupName
{
    public static readonly GroupName ChromeExtenstionGroupName = new ("Chrome Extension"); 
    public string Text { get; }

    public GroupName(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) throw new Exception();
        Text = text.Trim();
    }
}