using CodeLoom.Core.Base;

namespace CodeLoom.Core.Templates;

public class SolutionTmp : TemplateBase
{
    public List<string> Entities { get; set; } = new List<string>();

    // GUIDs únicos para cada proyecto
    public string SolutionGuid { get; set; } = $"{{{Guid.NewGuid().ToString().ToUpper()}}}";
    public string DomainProjectGuid { get; set; } = $"{{{Guid.NewGuid().ToString().ToUpper()}}}";
    public string ApplicationProjectGuid { get; set; } = $"{{{Guid.NewGuid().ToString().ToUpper()}}}";
    public string InfrastructureProjectGuid { get; set; } = $"{{{Guid.NewGuid().ToString().ToUpper()}}}";
    public string WebApiProjectGuid { get; set; } = $"{{{Guid.NewGuid().ToString().ToUpper()}}}";
    public string TestsProjectGuid { get; set; } = $"{{{Guid.NewGuid().ToString().ToUpper()}}}";

    protected override List<string> GetManifestResourceNames()
    {
        return new List<string> {
            $"{GetType().Namespace}.Application.ApplicationProject.liquid",

            $"{GetType().Namespace}.Domain.Common.Interfaces.IRepository.liquid",
            $"{GetType().Namespace}.Domain.Common.Interfaces.IUnitOfWork.liquid",
            $"{GetType().Namespace}.Domain.DomainProject.liquid",

            $"{GetType().Namespace}.Infrastructure.Data.Repositories.Repository.liquid",
            $"{GetType().Namespace}.Infrastructure.Data.Repositories.UnitOfWork.liquid",
            $"{GetType().Namespace}.Infrastructure.InfrastructureProject.liquid",

            $"{GetType().Namespace}.WebApi.Controllers.WeatherForecastController.liquid",
            $"{GetType().Namespace}.WebApi.appsettings.Development.liquid",
            $"{GetType().Namespace}.WebApi.appsettings.liquid",
            $"{GetType().Namespace}.WebApi.Program.liquid",
            $"{GetType().Namespace}.WebApi.WeatherForecast.liquid",
            $"{GetType().Namespace}.WebApi.WebApiProject.liquid",

            $"{GetType().Namespace}.Solution.liquid",

            //$"{GetType().Namespace}.WebApiProject.liquid",
            //$"{GetType().Namespace}.TestsProject.liquid",
            //$"{GetType().Namespace}.AppSettings.liquid",
            //$"{GetType().Namespace}.Program.liquid",
            //$"{GetType().Namespace}.DependencyInjection.liquid"
        };
    }
}

