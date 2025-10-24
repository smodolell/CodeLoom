using CodeLoom.Core.Attributes;
using System.Reflection;

namespace CodeLoom.Core.Helpers;

public static class PageAttributeExtensions
    {
        public static PageAttribute GetPageAttribute(this Type type)
        {
            return type.GetCustomAttribute<PageAttribute>();
        }

        public static PageAttribute GetPageAttribute(this PropertyInfo property)
        {
            return property.GetCustomAttribute<PageAttribute>();
        }

        public static PageAttribute GetPageAttribute(this FieldInfo field)
        {
            return field.GetCustomAttribute<PageAttribute>();
        }

        public static bool HasPageAttribute(this Type type)
        {
            return type.GetCustomAttribute<PageAttribute>() != null;
        }

        public static bool HasPageAttribute(this PropertyInfo property)
        {
            return property.GetCustomAttribute<PageAttribute>() != null;
        }
    }
