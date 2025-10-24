namespace CodeLoom.Core.Base;

public sealed class ParameterBase
{
    private static readonly Lazy<ParameterBase> _instance = new Lazy<ParameterBase>(() => new ParameterBase());



    public string PathOutput { get; set; } = "";
    public string SolutionName { get; set; } = "";
    public string ApplicationName { get; set; } = "";
    public string NameSpaceBase { get; set; } = "";
    //public string DbContextClass => $"{ModuleName}DbContext";
    //public string ModuleName { get; set; } = "";





    private ParameterBase()
    {

    }


    public static ParameterBase Instance => _instance.Value;
}


public static class TemplateParameterGlobalExtensions
{
    public static ParameterBase Configure(this ParameterBase instance, Action<ParameterBase> configureAction)
    {
        if (configureAction == null)
            throw new ArgumentNullException(nameof(configureAction));

        configureAction(instance);
        return instance;
    }
}