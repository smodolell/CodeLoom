using DotLiquid;

namespace CodeLoom.Core.Templates.Domain.Entities;

public class RelationshipTmp : Drop
{
    public string PropertyName { get; set; } = string.Empty;
    public string RelationshipType { get; set; } = string.Empty;
    public string TargetEntity { get; set; } = string.Empty;
    public string ForeignKey { get; set; } = string.Empty;
    public string MappedBy { get; set; } = string.Empty;
    public bool IsPrincipal { get; set; }
}