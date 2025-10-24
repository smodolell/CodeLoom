using CodeLoom.Core.Base;

namespace CodeLoom.Core.Templates.Domain.Entities;
public class EntityTmp : TemplateBase
{
    public string Name { get; set; } = "";
    public string TableName { get; set; } = "";
    public string PrimaryKeyName { get; set; } = "";
    public string PrimaryKeyType { get; set; } = "";

    public List<PropertyTmp> Properties { get; set; } = new List<PropertyTmp>();
    public List<RelationshipTmp> Relationships { get; set; } = new List<RelationshipTmp>();

    // Propiedades para relaciones únicas (evitar duplicados)
    public List<RelationshipTmp> UniqueOneToManyRelationships
    {
        get
        {
            return Relationships
                .Where(r => r.RelationshipType == "OneToMany")
                .GroupBy(r => $"OneToMany_{r.TargetEntity}")
                .Select(g => g.First())
                .ToList();
        }
    }

    public List<RelationshipTmp> UniqueManyToOneRelationships
    {
        get
        {
            return Relationships
                .Where(r => r.RelationshipType == "ManyToOne" || r.RelationshipType == "OneToOne")
                .GroupBy(r => $"ManyToOne_{r.TargetEntity}")
                .Select(g => g.First())
                .ToList();
        }
    }

    public List<RelationshipTmp> UniqueOneToOneRelationships
    {
        get
        {
            return Relationships
                .Where(r => r.RelationshipType == "OneToOne")
                .GroupBy(r => $"OneToOne_{r.TargetEntity}")
                .Select(g => g.First())
                .ToList();
        }
    }

    // Método para verificar si esta entidad tiene una FK hacia otra entidad
    public bool HasForeignKeyTo(string targetEntity)
    {
        return Properties.Any(p =>
            p.IsForeignKey &&
            p.RelatedEntity.Equals(targetEntity, StringComparison.OrdinalIgnoreCase));
    }

    // Método para obtener la propiedad FK hacia una entidad específica
    public PropertyTmp GetForeignKeyTo(string targetEntity)
    {
        return Properties.FirstOrDefault(p =>
            p.IsForeignKey &&
            p.RelatedEntity.Equals(targetEntity, StringComparison.OrdinalIgnoreCase));
    }

    // Método para obtener el tipo de dato de una FK
    public string GetForeignKeyType(string targetEntity)
    {
        var fkProperty = GetForeignKeyTo(targetEntity);
        return fkProperty?.Type ?? "int"; // Default a int si no se encuentra
    }

    // Método para obtener propiedades de navegación válidas
    public List<PropertyTmp> ValidNavigationProperties
    {
        get
        {
            return Properties.Where(p =>
                (p.RelationshipType == "ManyToOne" || p.RelationshipType == "OneToOne") &&
                !string.IsNullOrEmpty(p.Name)).ToList();
        }
    }

    // Método para verificar si una propiedad de navegación existe
    public bool HasNavigationProperty(string propertyName)
    {
        return ValidNavigationProperties.Any(p => p.Name == propertyName);
    }

    // Métodos únicos e inteligentes para la interfaz del repositorio (MEJORADO)
    public List<RepositoryMethodTmp> IntelligentRepositoryMethods
    {
        get
        {
            var methods = new List<RepositoryMethodTmp>();

            // 1. Métodos GetBy{Entity} para relaciones OneToMany (solo si esta entidad tiene la FK)
            foreach (var relationship in UniqueOneToManyRelationships)
            {
                var fkProperty = GetForeignKeyTo(relationship.TargetEntity);
                if (fkProperty != null)
                {
                    methods.Add(new RepositoryMethodTmp
                    {
                        Name = $"GetBy{relationship.TargetEntity}Async",
                        ReturnType = $"Task<IEnumerable<{Name}>>",
                        Parameters = $"{GetForeignKeyType(relationship.TargetEntity)} {relationship.TargetEntity.ToLower()}Id",
                        RelationshipType = "OneToMany",
                        TargetEntity = relationship.TargetEntity,
                        ForeignKeyProperty = fkProperty.Name,
                        ForeignKeyType = fkProperty.Type,
                        IsValid = true
                    });
                }
            }

            // 2. Métodos GetWith{Entity} para relaciones ManyToOne/OneToOne (solo si existe la propiedad de navegación)
            foreach (var relationship in UniqueManyToOneRelationships)
            {
                if (HasNavigationProperty(relationship.PropertyName))
                {
                    methods.Add(new RepositoryMethodTmp
                    {
                        Name = $"GetWith{relationship.TargetEntity}Async",
                        ReturnType = $"Task<IEnumerable<{Name}>>",
                        Parameters = "",
                        RelationshipType = relationship.RelationshipType,
                        TargetEntity = relationship.TargetEntity,
                        NavigationProperty = relationship.PropertyName,
                        IsValid = true
                    });
                }
            }

            // 3. Método GetByIdWithDetails (solo si hay relaciones para incluir)
            if (UniqueManyToOneRelationships.Any() || UniqueOneToManyRelationships.Any())
            {
                methods.Add(new RepositoryMethodTmp
                {
                    Name = "GetByIdWithDetailsAsync",
                    ReturnType = $"Task<{Name}>",
                    Parameters = $"{PrimaryKeyType} id",
                    RelationshipType = "Details",
                    TargetEntity = "",
                    IsValid = true
                });
            }

            return methods.Where(m => m.IsValid).ToList();
        }
    }
    // Método para determinar si necesita using System
    public bool NeedsSystemUsing
    {
        get
        {
            return Properties.Any(p =>
                p.Type.Contains("Guid") ||
                p.Type.Contains("DateTime") ||
                p.Type.Contains("TimeSpan"));
        }
    }

    public bool NeedsEnumUsing
    {
        get
        {
            return Properties.Any(p => p.IsEnum);
        }
    }
    protected override List<string> GetManifestResourceNames()
    {
        return new List<string> {
            $"{GetType().Namespace}.Entity.liquid",
            $"{GetType().Namespace}.EntityConfiguration.liquid",
            $"{GetType().Namespace}.EntityRepository.liquid",
            $"{GetType().Namespace}.EntityRepositoryImplementation.liquid"
        };
    }
}