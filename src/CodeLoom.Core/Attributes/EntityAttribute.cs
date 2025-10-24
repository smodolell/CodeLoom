namespace CodeLoom.Core.Attributes;


[AttributeUsage(AttributeTargets.Class)]
public class EntityAttribute : Attribute
{
    public string TableName { get; set; }

    public EntityAttribute(string tableName = "")
    {
        TableName = tableName;
    }
}
