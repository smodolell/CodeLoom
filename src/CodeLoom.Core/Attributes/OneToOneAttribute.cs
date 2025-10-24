namespace CodeLoom.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class OneToOneAttribute : Attribute
{
    public string ForeignKey { get; set; }
    public bool IsPrincipal { get; set; }

    public OneToOneAttribute(string foreignKey = "", bool isPrincipal = false)
    {
        ForeignKey = foreignKey;
        IsPrincipal = isPrincipal;
    }
}