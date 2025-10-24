using CodeLoom.Core.Base;

namespace CodeLoom.Core.Templates.Domain.Enums;

internal class EnumTmp : TemplateBase
{
    public string Name { get; set; } = string.Empty;
    public string Namespace { get; set; } = string.Empty;
    public List<EnumValueTmp> Values { get; set; } = new();
    protected override List<string> GetManifestResourceNames()
    {
        return new List<string> {
            $"{GetType().Namespace}.Enum.liquid",
        };
    }
}
