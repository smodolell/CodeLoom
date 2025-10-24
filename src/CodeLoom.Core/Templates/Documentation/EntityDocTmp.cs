using DotLiquid;

namespace CodeLoom.Core.Templates.Documentation;

public class EntityDocTmp : Drop
{
    public string Name { get; set; } = string.Empty;
    public string TableName { get; set; } = string.Empty;
    public string PrimaryKey { get; set; } = string.Empty;
    public string KeyType { get; set; } = string.Empty;
    public List<PropertyDocTmp> Properties { get; set; } = new();
    public List<RelationshipDocTmp> Relationships { get; set; } = new();
}
