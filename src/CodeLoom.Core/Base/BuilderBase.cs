namespace CodeLoom.Core.Base;

public abstract class BuilderBase<TModel>
{
    protected BuilderBase() { }
    public abstract TModel Build();
}
