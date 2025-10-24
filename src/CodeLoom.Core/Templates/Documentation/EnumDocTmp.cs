using DotLiquid;

namespace CodeLoom.Core.Templates.Documentation;


public class EnumDocTmp : Drop
{
    public string Name { get; set; } = string.Empty;
    public List<EnumValueDocTmp> Values { get; set; } = new();
}
