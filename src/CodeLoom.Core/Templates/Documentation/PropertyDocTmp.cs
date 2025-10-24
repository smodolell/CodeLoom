using DotLiquid;

namespace CodeLoom.Core.Templates.Documentation;


public class PropertyDocTmp : Drop
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool IsRequired { get; set; }
    public bool IsKey { get; set; }
    public bool IsForeignKey { get; set; }
    public int? MaxLength { get; set; }
    public string RelatedEntity { get; set; } = string.Empty;
}
