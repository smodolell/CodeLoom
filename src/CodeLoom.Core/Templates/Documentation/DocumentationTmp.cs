using CodeLoom.Core.Base;

namespace CodeLoom.Core.Templates.Documentation;

public class DocumentationTmp : TemplateBase
{
    public List<EntityDocTmp> Entities { get; set; } = new();
    public List<EnumDocTmp> Enums { get; set; } = new();
    public List<RelationshipDocTmp> Relationships { get; set; } = new();

    protected override List<string> GetManifestResourceNames()
    {
        return new List<string> {
            $"{GetType().Namespace}.README.liquid",
            $"{GetType().Namespace}.ENTITIES.liquid",
            $"{GetType().Namespace}.ENUMS.liquid",
            $"{GetType().Namespace}.DATABASE.liquid",
            $"{GetType().Namespace}.API.liquid"
        };
    }
}
