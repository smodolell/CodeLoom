using CodeLoom.Core.Attributes;
using CodeLoom.Core.Base;
using CodeLoom.Core.Helpers;
using CodeLoom.Core.Models;
using System.Reflection;

namespace CodeLoom.Core.Builders;

public class ApplicationBuilder : BuilderBase<ApplicationModel>
{
    private readonly ApplicationModel _model;

    public ApplicationBuilder()
    {
        _model = new ApplicationModel();
    }

    public ApplicationBuilder WithAssembly(Assembly assembly)
    {
        _model.AssemblyName = assembly.GetName().Name ?? string.Empty;
        _model.AssemblyVersion = assembly.GetName().Version?.ToString() ?? string.Empty;

        ScanEntities(assembly);
        ScanEnums(assembly);
        BuildRelationships(); // ← Esto ahora SÍ funcionará

        return this;
    }

    private void ScanEntities(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes().Where(t => t.IsClass))
        {
            if (type.HasAttribute<EntityAttribute>())
            {
                var entityAttribute = type.GetAttribute<EntityAttribute>();
                var entity = new EntityModel
                {
                    Name = type.Name,
                    TableName = entityAttribute?.TableName ?? type.Name
                };

                ScanProperties(type, entity);


                _model.Entities.Add(entity);
            }
        }
    }

    private void ScanProperties(Type type, EntityModel entity)
    {
        foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            // Saltar propiedades de navegación sin atributos (opcional)
            if (ShouldSkipProperty(prop)) continue;

            var propertyModel = new PropertyModel
            {
                Name = prop.Name,
                Type = GetPropertyTypeName(prop.PropertyType),
                IsRequired = IsPropertyRequired(prop),
                MaxLength = GetMaxLength(prop),
                IsEnum = prop.PropertyType.IsEnum,
                EnumType = prop.PropertyType.IsEnum ? prop.PropertyType.Name : null,
                IsPrimaryKey = prop.HasAttribute<KeyAttribute>()
            };
            if (propertyModel.IsPrimaryKey)
            {
                entity.PrimaryKeyType = propertyModel.Type;
                entity.PrimaryKeyName = propertyModel.Name;
            }
            // Detectar relaciones
            DetectRelationships(prop, propertyModel, entity);

            entity.Properties.Add(propertyModel);
        }
    }

    private void ScanEnums(Assembly assembly)
    {
        foreach (var enumType in assembly.GetTypes().Where(t => t.IsEnum))
        {
            var enumModel = new EnumModel
            {
                Name = enumType.Name,
                Namespace = enumType.Namespace ?? string.Empty
            };

            // Obtener valores del enum
            foreach (var field in enumType.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var enumValue = new EnumValueModel
                {
                    Name = field.Name,
                    Value = (int)field.GetRawConstantValue()!
                };

                // Obtener atributos Page si existen
                var pageAttr = field.GetPageAttribute();
                if (pageAttr != null)
                {
                    enumValue.DisplayName = pageAttr.DisplayName;
                    enumValue.Description = pageAttr.Description;
                }

                enumModel.Values.Add(enumValue);
            }

            _model.Enums.Add(enumModel);
        }
    }
    private bool ShouldSkipProperty(PropertyInfo prop)
    {
        // Saltar propiedades que son claramente de navegación pero no tienen atributos
        var type = prop.PropertyType;
        if (type.IsClass &&
            type != typeof(string) &&
            !type.IsValueType &&
            !prop.HasAttribute<ManyToOneAttribute>() &&
            !prop.HasAttribute<OneToOneAttribute>() &&
            !prop.HasAttribute<OneToManyAttribute>())
        {
            return true;
        }
        return false;
    }

    private void DetectRelationships(PropertyInfo prop, PropertyModel propertyModel, EntityModel entity)
    {
        // 1. Detectar ForeignKey (propiedades int, Guid, etc.)
        if (prop.HasAttribute<ForeignKeyAttribute>())
        {
            var foreignKeyAttr = prop.GetAttribute<ForeignKeyAttribute>();
            propertyModel.IsForeignKey = true;
            propertyModel.RelatedEntity = foreignKeyAttr.RelatedEntity;
            propertyModel.RelationshipType = "ManyToOne";

            entity.Relationships.Add(new RelationshipModel
            {
                PropertyName = prop.Name,
                RelationshipType = "ManyToOne",
                TargetEntity = foreignKeyAttr.RelatedEntity,
                ForeignKey = prop.Name,
                MappedBy = "" // Se completará en BuildRelationships
            });
        }

        // 2. Detectar ManyToOne (propiedades de navegación)
        else if (prop.HasAttribute<ManyToOneAttribute>())
        {
            var manyToOneAttr = prop.GetAttribute<ManyToOneAttribute>();
            propertyModel.RelationshipType = "ManyToOne";
            propertyModel.RelatedEntity = prop.PropertyType.Name; // ← CORREGIDO

            entity.Relationships.Add(new RelationshipModel
            {
                PropertyName = prop.Name,
                RelationshipType = "ManyToOne",
                TargetEntity = prop.PropertyType.Name, // ← CORREGIDO
                ForeignKey = manyToOneAttr.ForeignKey,
                MappedBy = "" // Se completará en BuildRelationships
            });
        }

        // 3. Detectar OneToMany (colecciones)
        else if (prop.HasAttribute<OneToManyAttribute>())
        {
            var oneToManyAttr = prop.GetAttribute<OneToManyAttribute>();
            propertyModel.RelationshipType = "OneToMany";
            propertyModel.RelatedEntity = GetGenericArgumentName(prop.PropertyType);
            propertyModel.MappedBy = oneToManyAttr.MappedBy;

            entity.Relationships.Add(new RelationshipModel
            {
                PropertyName = prop.Name,
                RelationshipType = "OneToMany",
                TargetEntity = GetGenericArgumentName(prop.PropertyType),
                MappedBy = oneToManyAttr.MappedBy,
                ForeignKey = "" // Se completará en BuildRelationships
            });
        }

        // 4. Detectar OneToOne
        else if (prop.HasAttribute<OneToOneAttribute>())
        {
            var oneToOneAttr = prop.GetAttribute<OneToOneAttribute>();
            propertyModel.RelationshipType = "OneToOne";
            propertyModel.RelatedEntity = prop.PropertyType.Name;

            entity.Relationships.Add(new RelationshipModel
            {
                PropertyName = prop.Name,
                RelationshipType = "OneToOne",
                TargetEntity = prop.PropertyType.Name,
                ForeignKey = oneToOneAttr.ForeignKey,
                MappedBy = oneToOneAttr.ForeignKey, // Usar FK como MappedBy temporal
                IsPrincipal = oneToOneAttr.IsPrincipal
            });
        }
    }

    private void BuildRelationships()
    {
        // Completar relaciones bidireccionales
        foreach (var entity in _model.Entities)
        {
            foreach (var relationship in entity.Relationships.ToList())
            {
                var targetEntity = _model.Entities.FirstOrDefault(e => e.Name == relationship.TargetEntity);
                if (targetEntity != null)
                {
                    // Buscar la relación inversa
                    var inverseRelationship = targetEntity.Relationships
                        .FirstOrDefault(r => r.TargetEntity == entity.Name);

                    if (inverseRelationship != null)
                    {
                        // Completar MappedBy en relaciones ManyToOne
                        if (relationship.RelationshipType == "ManyToOne" && string.IsNullOrEmpty(relationship.MappedBy))
                        {
                            relationship.MappedBy = inverseRelationship.PropertyName;
                        }

                        // Completar ForeignKey en relaciones OneToMany  
                        if (relationship.RelationshipType == "OneToMany" && string.IsNullOrEmpty(relationship.ForeignKey))
                        {
                            var targetFk = targetEntity.Properties
                                .FirstOrDefault(p => p.IsForeignKey && p.RelatedEntity == entity.Name);
                            relationship.ForeignKey = targetFk?.Name ?? $"{entity.Name}Id";
                        }
                    }
                }
            }
        }
    }

    private static string GetPropertyTypeName(Type type)
    {
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            var underlyingType = type.GetGenericArguments()[0];
            return $"{GetBuiltInTypeName(underlyingType)}?";
        }

        // Para colecciones (List<T>, ICollection<T>, etc.)
        if (type.IsGenericType && typeof(System.Collections.IEnumerable).IsAssignableFrom(type))
        {
            var genericArg = type.GetGenericArguments()[0];
            return $"List<{GetBuiltInTypeName(genericArg)}>";
        }

        return GetBuiltInTypeName(type);
    }

    private static string GetBuiltInTypeName(Type type)
    {
        return type.FullName switch // ← Usar FullName para más precisión
        {
            "System.String" => "string",
            "System.Int32" => "int",
            "System.Int64" => "long",
            "System.Boolean" => "bool",
            "System.Single" => "float",
            "System.Double" => "double",
            "System.Decimal" => "decimal",
            "System.Byte" => "byte",
            "System.SByte" => "sbyte",
            "System.Int16" => "short",
            "System.UInt16" => "ushort",
            "System.UInt32" => "uint",
            "System.UInt64" => "ulong",
            "System.Char" => "char",
            "System.Object" => "object",
            "System.Guid" => "Guid",
            "System.DateTime" => "DateTime",
            "System.TimeSpan" => "TimeSpan",
            _ => type.Name // Para entidades personalizadas
        };
    }

    private static string GetGenericArgumentName(Type type)
    {
        return type.IsGenericType ? GetBuiltInTypeName(type.GetGenericArguments()[0]) : GetBuiltInTypeName(type);
    }

    private static bool IsPropertyRequired(PropertyInfo prop)
    {
        return prop.HasAttribute<RequiredAttribute>() ||
               (prop.PropertyType.IsValueType &&
                Nullable.GetUnderlyingType(prop.PropertyType) == null);
    }

    private static int? GetMaxLength(PropertyInfo prop)
    {
        var maxLengthAttr = prop.GetCustomAttribute<MaxLengthAttribute>();
        return maxLengthAttr?.Length;
    }

    public ApplicationBuilder System(Guid systemId, string system, string version)
    {
        _model.SystemId = systemId;
        _model.System = system;
        _model.Version = version;
        return this;
    }

    public ApplicationBuilder WithName(string name)
    {
        _model.Name = name;
        return this;
    }

    public ApplicationBuilder WithNameSpaceBase(string nameSpaceBase)
    {
        _model.NameSpaceBase = nameSpaceBase;
        return this;
    }

    public override ApplicationModel Build()
    {
        return _model;
    }
}