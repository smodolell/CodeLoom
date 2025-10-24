using LiteBus.Events.Abstractions;

namespace CodeLoom.Core.Base;

public class SpecificationBuiltEventBase<TModel> : IEvent
    where TModel : class
{
    public TModel Model { get; }
    public string SpecificationName { get; }
    public SpecificationBuiltEventBase(TModel model, string specificationName)
    {
        Model = model;
        SpecificationName = specificationName;
    }

    public DateTime Timestamp { get; }

}
