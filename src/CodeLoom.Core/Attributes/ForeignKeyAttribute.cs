namespace CodeLoom.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ForeignKeyAttribute : Attribute
{
    public string RelatedEntity { get; }
    public string RelatedProperty { get; set; } = "Id";

    public ForeignKeyAttribute(string relatedEntity)
    {
        RelatedEntity = relatedEntity;
    }
}