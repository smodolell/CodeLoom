namespace CodeLoom.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class OneToManyAttribute : Attribute
{
    public string MappedBy { get; }

    public OneToManyAttribute(string mappedBy)
    {
        MappedBy = mappedBy;
    }
}