using Domain;

namespace Cards.Domain.ValueObjects;

public readonly struct GroupName
{
    public static readonly GroupName ChromeExtenstionGroupName = new GroupName("Chrome Extension"); 
    public string Text { get; }

    private GroupName(string text)
    {
        Text = text;
    }

    public static GroupName Create(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) throw new BuissnessArgumentException(nameof(text), text);

        var trimmedText = text.Trim();

        return new GroupName(trimmedText);
    }
    
    public static bool operator !=(GroupName groupName1, GroupName groupName2)
        => groupName1.Text != groupName2.Text;
    public static bool operator == (GroupName groupName1, GroupName groupName2)
        => groupName1.Text == groupName2.Text;

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        var groupName = (GroupName)obj;
        return Text == groupName.Text;
    }

    public override int GetHashCode()
        => Text.GetHashCode();
}