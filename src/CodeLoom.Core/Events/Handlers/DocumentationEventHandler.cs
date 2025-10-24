using CodeLoom.Core.Base;
using CodeLoom.Core.Models;
using CodeLoom.Core.Templates.Documentation;
using LiteBus.Events.Abstractions;
using Mapster;

namespace CodeLoom.Core.Events.Handlers;

public class DocumentationEventHandler : IEventHandler<ApplicationBuiltEvent>
{
    public DocumentationEventHandler()
    {
    }

    public Task HandleAsync(ApplicationBuiltEvent @event, CancellationToken cancellationToken = default)
    {
        var model = @event.Model;
        var files = new List<GeneratedFile>();

        // Convertir a modelos de documentación
        var docTmp = new DocumentationTmp
        {
            Entities = model.Entities.Select(e => e.Adapt<EntityDocTmp>()).ToList(),
            Enums = model.Enums.Select(e => e.Adapt<EnumDocTmp>()).ToList(),
            Relationships = ExtractRelationships(model.Entities)
        };

        files.AddRange(docTmp.GetFiles());

        foreach (var file in files)
        {
            file.SaveFile(false); // Guardar en raíz/docs
        }

        Console.WriteLine($"📚 Documentación generada: {files.Count} archivos");
        return Task.CompletedTask;
    }

    private List<RelationshipDocTmp> ExtractRelationships(List<EntityModel> entities)
    {
        var relationships = new List<RelationshipDocTmp>();

        foreach (var entity in entities)
        {
            foreach (var rel in entity.Relationships)
            {
                relationships.Add(new RelationshipDocTmp
                {
                    FromEntity = entity.Name,
                    ToEntity = rel.TargetEntity,
                    Type = rel.RelationshipType,
                    ForeignKey = rel.ForeignKey
                });
            }
        }

        return relationships;
    }
}