using CodeLoom.Core.Builders;
using CodeLoom.Core.Specifications;

namespace CodeLoom.Business.Financial.Application;

public class FinancialApplicationSpec : ApplicationSpecificationBase
{
    public FinancialApplicationSpec() : base("financial")
    {
    }

    public override void Configure(ApplicationBuilder builder)
    {
        builder.WithAssembly(typeof(FinancialApplicationSpec).Assembly);
        builder.System(Guid.NewGuid(), "FinancialSystem", "1.0.0");
        builder.WithName("Financial");
        builder.WithNameSpaceBase("Yggdrasil");

    }
}
