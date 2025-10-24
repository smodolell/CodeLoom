namespace CodeLoom.Core.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Enum)]
public class PageAttribute : Attribute
{
    public string DisplayName { get; set; }
    public string Description { get; set; }
    public string Group { get; set; }
    public int Order { get; set; } = 0;
    public bool IsVisible { get; set; } = true;

    public PageAttribute(string displayName, string description)
    {
        DisplayName = displayName;
        Description = description;
        Group = "";
    }

    public PageAttribute(string displayName = "", string description = "", string group = "")
    {
        DisplayName = displayName;
        Description = description;
        Group = group;
    }

    public PageAttribute(string displayName, string description, string group, int order) : this(displayName, description, group)
    {
        Order = order;
    }
}
