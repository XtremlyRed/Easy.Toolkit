
using System;
namespace Easy.Toolkit
{
    /// <summary>
    /// container   registry
    /// </summary>
    public interface IContainerRegistry
    {  
        /// <summary>
        /// register many
        /// </summary>
        /// <param name="ImplementationType"></param>
        /// <param name="serviceTypes"></param>
        /// <returns></returns>
        IRegisteredType RegisterMany(Type ImplementationType, Type[] serviceTypes);

        /// <summary>
        /// register
        /// </summary>
        /// <param name="ImplementationType"></param>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        IRegisteredType Register(Type ImplementationType, Type serviceType);

        /// <summary>
        /// register
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IRegisteredType Register(Type type);

        /// <summary>
        /// register
        /// </summary>
        /// <typeparam name="TImplementation"></typeparam>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        IRegisteredType Register<TImplementation, TService>()  where TImplementation : TService;

        /// <summary>
        /// register
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="factory"></param>
        /// <returns></returns>
        IRegisteredType Register<Target>(Func<object> factory);

        /// <summary>
        /// register
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <returns></returns>
        IRegisteredType Register<Target>();

        /// <summary>
        /// register instance
        /// </summary>
        /// <param name="target"></param>
        void RegisterInstance(object target);


        /// <summary>
        /// build provider
        /// </summary>
        /// <returns></returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        IContainerProvider Buidler();
    }

    /// <summary>
    /// resolve an any object from container
    /// </summary>
    public interface IContainerProvider
    {
        /// <summary>
        /// resolve <typeparamref name="Target"/> object from container
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <returns></returns>
        Target Resolve<Target>();

        /// <summary>
        /// resolve object from container
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object Resolve(Type type);
    }



}
