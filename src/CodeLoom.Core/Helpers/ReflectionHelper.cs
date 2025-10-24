using System.Reflection;

namespace CodeLoom.Core.Helpers;

public static class ReflectionHelper
{
    public static bool HasAttribute<T>(this Type type) where T : Attribute
    {
        return type.GetCustomAttribute<T>() != null;
    }

    public static T? GetAttribute<T>(this Type type) where T : Attribute
    {
        return type.GetCustomAttribute<T>();
    }
    // Extensiones para PropertyInfo
    public static bool HasAttribute<T>(this PropertyInfo property) where T : Attribute
    {
        return property.GetCustomAttribute<T>() != null;
    }

    public static T GetAttribute<T>(this PropertyInfo property) where T : Attribute
    {
        return property.GetCustomAttribute<T>();
    }

    public static IEnumerable<T> GetAttributes<T>(this PropertyInfo property) where T : Attribute
    {
        return property.GetCustomAttributes<T>();
    }

    // Método helper para detectar si es una colección
    public static bool IsCollection(this PropertyInfo property)
    {
        return property.PropertyType.IsGenericType &&
               typeof(System.Collections.IEnumerable).IsAssignableFrom(property.PropertyType);
    }

    // Método helper para obtener el tipo genérico de una colección
    public static Type GetCollectionItemType(this PropertyInfo property)
    {
        if (property.IsCollection())
        {
            return property.PropertyType.GetGenericArguments()[0];
        }
        return null;
    }
}
