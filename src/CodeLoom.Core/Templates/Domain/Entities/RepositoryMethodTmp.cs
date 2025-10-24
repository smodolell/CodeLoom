using DotLiquid;

namespace CodeLoom.Core.Templates.Domain.Entities;

// Nuevo class para métodos del repositorio
public class RepositoryMethodTmp : Drop
{
    public string Name { get; set; } = string.Empty;
    public string ReturnType { get; set; } = string.Empty;
    public string Parameters { get; set; } = string.Empty;
    public string RelationshipType { get; set; } = string.Empty;
    public string TargetEntity { get; set; } = string.Empty;
    public string ForeignKeyProperty { get; set; } = string.Empty;
    public string ForeignKeyType { get; set; } = string.Empty;
    public string NavigationProperty { get; set; } = string.Empty;
    public bool IsValid { get; set; } = true;
}