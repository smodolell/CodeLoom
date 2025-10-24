using DotLiquid;

namespace CodeLoom.Core.Templates.Documentation;
public class EnumValueDocTmp : Drop
{
    public string Name { get; set; } = string.Empty;
    public int Value { get; set; }
    public string DisplayName { get; set; } = string.Empty;
}
