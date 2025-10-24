using DotLiquid;

namespace CodeLoom.Core.Templates.Domain.Enums;

public class EnumValueTmp:Drop
{
    public string Name { get; set; } = string.Empty;
    public int Value { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
