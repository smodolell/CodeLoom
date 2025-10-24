using CodeLoom.Business.Financial.Application;
using CodeLoom.Core.Base;
using LiteBus.Events.Abstractions;
using Tharga.Console.Commands.Base;

namespace CodeLoom.Build.Commands;

internal class Build_FinancialCommand : ActionCommandBase
{
    private readonly IEventPublisher _eventPublisher;

    public Build_FinancialCommand(IEventPublisher eventPublisher) : base("financial", "Documentacion de desarrollo")
    {
        _eventPublisher = eventPublisher;
    }

    public override async void Invoke(string[] param)
    {

        var path = "F:\\Yggdrasil_Services\\Output";
        var spec = new FinancialApplicationSpec();

        var model = spec.Build();

        ParameterBase.Instance.Configure(config =>
        {
            config.PathOutput = path;
            config.SolutionName = $"{model.Name}_output";
            config.NameSpaceBase = model.NameSpaceBase;
            config.ApplicationName = model.Name;
        });

        var appEvent = spec.BuildEvent();
        appEvent.LogEvent();

        await _eventPublisher.PublishAsync(spec.BuildEvent());
    }
}