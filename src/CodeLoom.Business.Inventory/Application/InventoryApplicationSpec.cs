using CodeLoom.Core.Builders;
using CodeLoom.Core.Specifications;

namespace CodeLoom.Business.Inventory.Application;

public class InventoryApplicationSpec : ApplicationSpecificationBase
{
    public InventoryApplicationSpec() : base("inventory")
    {
    }

    public override void Configure(ApplicationBuilder builder)
    {
        builder.WithAssembly(typeof(InventoryApplicationSpec).Assembly);
        builder.System(Guid.NewGuid(), "InventorySystem", "1.0.0");
        builder.WithName("Inventory");
        builder.WithNameSpaceBase("Yggdrasil");

    }
}
