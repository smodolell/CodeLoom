namespace CodeLoom.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ManyToOneAttribute : Attribute
{
    public string ForeignKey { get; set; }

    public ManyToOneAttribute(string foreignKey = "")
    {
        ForeignKey = foreignKey;
    }
}
