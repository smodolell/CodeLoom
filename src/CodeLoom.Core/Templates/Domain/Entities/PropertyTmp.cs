using DotLiquid;

namespace CodeLoom.Core.Templates.Domain.Entities;

public class PropertyTmp : Drop
{

    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool IsRequired { get; set; }
    public int? MaxLength { get; set; }
    public bool IsForeignKey { get; set; }
    public string RelatedEntity { get; set; } = string.Empty;
    public string RelationshipType { get; set; } = string.Empty;
    public string MappedBy { get; set; } = string.Empty;
    public bool IsEnum { get; set; }
    public bool IsPrimaryKey { get; set; }
    public string PropertyInitializer => _propertyInitializer();


    public string _propertyInitializer()
    {
        if (Type == "string")
            return " = string.Empty;";
        else if (Type == "Guid")
            return " = Guid.NewGuid();";
        else if (Type.StartsWith("List<") || Type.StartsWith("ICollection<"))
            return $" = new {Type}();";
        else if (Type.EndsWith("?")) // Nullable types
            return ";";
        else
            return "";
    }
}
