//using CodeLoom.Core.Base;
//using CodeLoom.Core.Builders;
//using CodeLoom.Core.Events;
//using CodeLoom.Core.Models;
//using System.Reflection;

//namespace CodeLoom.Core.Specifications;

//public abstract class ApplicationSpecificationBase : SpecificationBase<ApplicationModel, ApplicationBuilder>
//{
//    private readonly Assembly _assembly;
//    public ApplicationSpecificationBase(Assembly assembly, string specificationName) : base(specificationName)
//    {
//        _assembly = assembly;
//    }

//    public ApplicationBuiltEvent BuildEvent()
//    {
//        return BuildEvent((model, specName) =>
//            new ApplicationBuiltEvent(_assembly, model, specName));
//    }
//}


using CodeLoom.Core.Base;
using CodeLoom.Core.Builders;
using CodeLoom.Core.Events;
using CodeLoom.Core.Models;
using System.Reflection;

namespace CodeLoom.Core.Specifications;

public abstract class ApplicationSpecificationBase : SpecificationBase<ApplicationModel, ApplicationBuilder>
{
    public ApplicationSpecificationBase(string specificationName) : base(specificationName)
    {
    }

    public ApplicationBuiltEvent BuildEvent()
    {
        return BuildEvent((model, specName) =>
            new ApplicationBuiltEvent(model, specName));
    }

}
