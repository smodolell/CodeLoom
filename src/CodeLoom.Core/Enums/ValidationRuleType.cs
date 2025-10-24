namespace CodeLoom.Core.Enums;

public enum ValidationRuleType
{
    Required,
    Email,
    MaxLength,
    MinLength,
    Range,
    GreaterThan,
    LessThan,
    NotEmpty,
    NotNull,
    // Puedes agregar muchas más según necesites
    Custom // Para reglas personalizadas
}
