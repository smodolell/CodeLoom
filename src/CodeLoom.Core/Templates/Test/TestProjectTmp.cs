using CodeLoom.Core.Base;

namespace CodeLoom.Core.Templates.Test;

internal class TestProjectTmp : TemplateBase
{
    protected override List<string> GetManifestResourceNames()
    {
        return new List<string>
        {
            $"{GetType().Namespace}.TestProject.liquid",
            $"{GetType().Namespace}.UnitTest1.liquid",
        };
    }
}
