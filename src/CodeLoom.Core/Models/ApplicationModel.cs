using CodeLoom.Core.Enums;
using System.Net.Http.Headers;

namespace CodeLoom.Core.Models;

public class ApplicationModel
{
    public Guid SystemId { get; set; }
    public string Version { get; set; } = "";
    public string System { get; set; } = "";
    public string Overview { get; set; } = "";
    public string Name { get; set; } = "";
    public string NameSpaceBase { get; set; } = "";
    public string AssemblyName { get; set; } = "";
    public string AssemblyVersion { get; set; } = "";
    public List<EntityModel> Entities { get; set; } = new List<EntityModel>();
    public List<EnumModel> Enums { get; set; } = new List<EnumModel>();
}




public class EntityModel
{
    public string Name { get; set; } = "";
    public string TableName { get; set; } = "";
    public string PrimaryKeyName { get; set; } = "";
    public string PrimaryKeyType { get; set; } = "";
    public List<PropertyModel> Properties { get; set; } = new List<PropertyModel>();

    public List<RelationshipModel> Relationships { get; set; } = new();
}

public class RelationshipModel
{
    public string PropertyName { get; set; } = string.Empty;
    public string RelationshipType { get; set; } = string.Empty; // "OneToMany", "ManyToOne", "OneToOne"
    public string TargetEntity { get; set; } = string.Empty;
    public string ForeignKey { get; set; } = string.Empty;
    public string MappedBy { get; set; } = string.Empty;
    public bool IsPrincipal { get; set; }
}

public class PropertyModel
{

    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool IsRequired { get; set; }
    public int? MaxLength { get; set; }
    public bool IsForeignKey { get; set; }
    public string RelatedEntity { get; set; } = string.Empty;
    public string RelationshipType { get; set; } = string.Empty; // "OneToMany", "ManyToOne", "OneToOne"
    public string MappedBy { get; set; } = string.Empty;
    public bool IsEnum { get; set; }
    public string? EnumType { get; set; } = string.Empty;
    public bool IsPrimaryKey { get; set; }
}


public class EnumModel
{
    public string Name { get; set; } = string.Empty;
    public string Namespace { get; set; } = string.Empty;
    public List<EnumValueModel> Values { get; set; } = new();
}

public class EnumValueModel
{
    public string Name { get; set; } = string.Empty;
    public int Value { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}