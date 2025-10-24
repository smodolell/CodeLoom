namespace CodeLoom.Core.Enums;

public static class PropertyTypeMapper
{
    public static Type ToType(this PropertyType propertyType) => propertyType switch
    {
        PropertyType.String => typeof(string),
        PropertyType.Int => typeof(int),
        PropertyType.Guid => typeof(Guid),
        PropertyType.DateTime => typeof(DateTime),
        PropertyType.Decimal => typeof(decimal),
        PropertyType.Boolean => typeof(bool),
        _ => throw new ArgumentOutOfRangeException(nameof(propertyType), propertyType, null)
    };

    public static string ToTypeName(this PropertyType propertyType) => propertyType switch
    {
        PropertyType.String => "string",
        PropertyType.Int => "int",
        PropertyType.Guid => "Guid",
        PropertyType.DateTime => "DateTime",
        PropertyType.Decimal => "decimal",
        PropertyType.Boolean => "bool",
        _ => throw new ArgumentOutOfRangeException(nameof(propertyType), propertyType, null)
    };
}