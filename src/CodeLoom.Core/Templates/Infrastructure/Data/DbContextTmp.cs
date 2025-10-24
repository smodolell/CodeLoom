using CodeLoom.Core.Base;

namespace CodeLoom.Core.Templates.Infrastructure.Data;


public class DbContextTmp : TemplateBase
{
    public List<string> EntityNames { get; set; } = new List<string>();

    protected override List<string> GetManifestResourceNames()
    {
        return new List<string> { $"{GetType().Namespace}.DbContext.liquid" };
    }
}
