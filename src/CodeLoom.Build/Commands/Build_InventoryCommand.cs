using CodeLoom.Business.Inventory.Application;
using CodeLoom.Core.Base;
using LiteBus.Events.Abstractions;
using Tharga.Console.Commands.Base;

namespace CodeLoom.Build.Commands;

internal class Build_InventoryCommand : ActionCommandBase
{
    private readonly IEventPublisher _eventPublisher;

    public Build_InventoryCommand(IEventPublisher eventPublisher) : base("inventory", "Documentacion de desarrollo")
    {
        _eventPublisher = eventPublisher;
    }

    public override async void Invoke(string[] param)
    {

        var path = "F:\\Yggdrasil_Services\\Output";
        var spec = new InventoryApplicationSpec();

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
