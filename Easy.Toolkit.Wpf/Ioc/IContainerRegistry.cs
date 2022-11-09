
using System;
namespace Easy.Toolkit
{
    public interface IContainerRegistry
    {  
        IRegisteredType RegisterMany(Type ImplementationType, Type[] serviceTypes);

        IRegisteredType Register(Type ImplementationType, Type serviceType);

        IRegisteredType Register(Type type);
         
        IRegisteredType Register<TImplementation, TService>()  where TImplementation : TService;

        IRegisteredType Register<Target>(Func<object> factory);
         
        IRegisteredType Register<Target>();
    }


    public interface IContainerProvider
    { 
        Target Resolve<Target>();

        object Resolve(Type type);
    }



}
