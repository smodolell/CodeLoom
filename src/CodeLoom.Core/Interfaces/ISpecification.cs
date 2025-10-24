using CodeLoom.Core.Base;

namespace CodeLoom.Core.Interfaces;

public interface ISpecification<TModel, TBuilder>
    where TModel : class
    where TBuilder : BuilderBase<TModel>
{
    string SpecificationName { get; }
    TModel Build();
    void Configure(TBuilder builder);
}
