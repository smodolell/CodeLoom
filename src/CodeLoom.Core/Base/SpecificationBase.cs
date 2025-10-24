using CodeLoom.Core.Interfaces;

namespace CodeLoom.Core.Base;

public abstract class SpecificationBase<TModel, TBuilder> : ISpecification<TModel, TBuilder>
    where TModel : class, new()
    where TBuilder : BuilderBase<TModel>, new()
{
    private TModel _builtModel;
    private readonly TBuilder _builder;
    private bool _isBuilt = false;

    protected SpecificationBase(string specificationName)
    {
        _builtModel = new TModel();
        _builder = new TBuilder();
        SpecificationName = specificationName;
    }
    public string SpecificationName { get; }

    public TModel Build()
    {
        if (!_isBuilt)
        {
            Configure(_builder);
            _builtModel = _builder.Build();
            _isBuilt = true;
        }
        return _builtModel;
        //Configure(_builder);
        //return _builder.Build();
    }

    public abstract void Configure(TBuilder builder);

    // Método genérico para construir cualquier tipo de evento
    protected TEvent BuildEvent<TEvent>(Func<TModel, TEvent> eventFactory)
    {
        var model = Build();        
        return eventFactory(model);
    }

    // Método genérico para eventos que necesiten el nombre de la especificación
    protected TEvent BuildEvent<TEvent>(Func<TModel, string, TEvent> eventFactory)
    {
        var model = Build();
        return eventFactory(model, SpecificationName);
    }

    // Método para resetear si necesitas reconstruir
    public void Reset()
    {
        _isBuilt = false;
        _builtModel = null;
    }
}
