using Tharga.Console.Commands.Base;

namespace CodeLoom.Build.Commands;

internal class Build_ContainerCommand : ContainerCommandBase
{
    public Build_ContainerCommand() : base("build")
    {
        RegisterCommand<Build_InventoryCommand>();
        RegisterCommand<Build_FinancialCommand>();
    }
}
