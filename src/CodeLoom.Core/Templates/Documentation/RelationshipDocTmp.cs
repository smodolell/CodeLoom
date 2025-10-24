using DotLiquid;

namespace CodeLoom.Core.Templates.Documentation;
public class RelationshipDocTmp : Drop
{
    public string FromEntity { get; set; } = string.Empty;
    public string ToEntity { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // "OneToMany", "ManyToOne", "OneToOne"
    public string ForeignKey { get; set; } = string.Empty;
}