using CodeLoom.Core.Models;
using CodeLoom.Core.Templates.Domain.Entities;
using Mapster;

namespace CodeLoom.Core.Mappings;


public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<EntityModel, EntityTmp>();
        config.NewConfig<PropertyModel, PropertyTmp>();
        config.NewConfig<RelationshipModel, RelationshipTmp>();
    }
}
