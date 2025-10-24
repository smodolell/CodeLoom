using CodeLoom.Core.Base;
using CodeLoom.Core.Models;
using static System.Net.Mime.MediaTypeNames;

namespace CodeLoom.Core.Events;

public class ApplicationBuiltEvent : SpecificationBuiltEventBase<ApplicationModel>
{
    public ApplicationBuiltEvent(ApplicationModel model, string specificationName) : base(model, specificationName)
    {
    }
    public void LogEvent()
    {
        Console.WriteLine($"Aplicación: {Model.Name}");
        Console.WriteLine($"Namespace Base: {Model.NameSpaceBase}");
        Console.WriteLine($"Sistema: {Model.System} v{Model.Version}");
        Console.WriteLine($"Assembly: {Model.AssemblyName} v{Model.AssemblyVersion}");
        Console.WriteLine($"Especificación: {SpecificationName}");

        foreach (var entity in Model.Entities)
        {
            Console.WriteLine($"\n📦 {entity.Name} ({entity.TableName})");

            Console.WriteLine("  Propiedades:");
            foreach (var prop in entity.Properties)
            {
                var relationInfo = prop.IsForeignKey ? $"[FK → {prop.RelatedEntity}]" :
                                  !string.IsNullOrEmpty(prop.RelationshipType) ? $"[{prop.RelationshipType} → {prop.RelatedEntity}]" : "";

                Console.WriteLine($"    • {prop.Name} : {prop.Type} {relationInfo}");
            }
            if (entity.Relationships.Any())
            {
                Console.WriteLine("  Relaciones:");
                foreach (var rel in entity.Relationships)
                {
                    Console.WriteLine($"    • {rel.PropertyName} : {rel.RelationshipType} → {rel.TargetEntity}");
                }
            }
        }

    }
}

