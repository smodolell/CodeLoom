using CodeLoom.Core.Base;
using LiteBus.Events;
using LiteBus.Extensions.Microsoft.DependencyInjection;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace CodeLoom.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {

        services.AddLogging(configure => configure.AddConsole());
        services.AddSingleton<IMapper>(serviceProvider =>
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            return new Mapper(config);
        });
        services.AddLiteBus(configuration =>
        {
            configuration.AddEventModule(m => m.RegisterFromAssembly(typeof(DependencyInjection).Assembly)); // registra handlers
        });

        TemplateConfig.Configure();

        return services;
    }

 
}
