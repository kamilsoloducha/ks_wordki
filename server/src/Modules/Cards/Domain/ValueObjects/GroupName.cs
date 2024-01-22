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

    public override bool Equals(object obj)
    {
        if (obj is GroupName groupName)
        {
            return groupName.Text == Text;
        }

        return false;
    }
    public override int GetHashCode() => Text != null ? Text.GetHashCode() : 0;
    public static bool operator ==(GroupName obj1, GroupName obj2) => obj1 is not null && obj1.Equals(obj2);
    public static bool operator !=(GroupName obj1, GroupName obj2) => !(obj1 == obj2);
}