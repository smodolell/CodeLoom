using CodeLoom.Core.Base;
using CodeLoom.Core.Templates;
using CodeLoom.Core.Templates.Domain.Entities;
using CodeLoom.Core.Templates.Domain.Enums;
using CodeLoom.Core.Templates.Infrastructure.Data;
using CodeLoom.Core.Templates.Test;
using LiteBus.Events.Abstractions;
using Mapster;

namespace CodeLoom.Core.Events.Handlers;

public class ApplicationEventHandler : IEventHandler<ApplicationBuiltEvent>
{

    public ApplicationEventHandler()
    {
    }

    public Task HandleAsync(ApplicationBuiltEvent @event, CancellationToken cancellationToken = default)
    {
        var model = @event.Model;
        var files = new List<GeneratedFile>();

        var solutionTmp = new SolutionTmp
        {
            Entities = model.Entities.Select(e => e.Name).ToList()
        };
        files.AddRange(solutionTmp.GetFiles());

        foreach (var entity in model.Entities)
        {
            var entityTmp = entity.Adapt<EntityTmp>();
            files.AddRange(entityTmp.GetFiles());
        }

        foreach (var e in model.Enums)
        {
            var enumTmp = e.Adapt<EnumTmp>();
            files.AddRange(enumTmp.GetFiles());
        }

        var dbContextTmp = new DbContextTmp
        {
            EntityNames = model.Entities.Select(e => e.Name).ToList()
        };

        files.AddRange(dbContextTmp.GetFiles());


        foreach (var file in files)
        {
            file.SaveFile(true);
        }

        //Proyecto ts
        var testProjectTmp = new TestProjectTmp();


        foreach (var file in testProjectTmp.GetFiles())
        {
            file.SaveFile(false);
        }
        return Task.CompletedTask;
    }
}
