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
        /// <param name="serviceTypes"></param>    
        /// <param name="implementationType"></param> 
        /// <returns></returns>
        IRegisteredType RegisterMany(Type[] serviceTypes, Type implementationType);

        /// <summary>
        /// register
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implementationType"></param> 
        /// <returns></returns>
        IRegisteredType Register(Type serviceType, Type implementationType);

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
        IRegisteredType Register<TService, TImplementation>() where TImplementation : TService;

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
        void RegisterSingleton<Target>(Target target) where Target : class;

        /// <summary>
        /// register instance
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <returns></returns>
        void RegisterSingleton<Target>() where Target : class;


        /// <summary>
        /// register instance
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="factory"></param>
        /// <returns></returns>
        void RegisterSingleton<Target>(Func<object> factory);

        /// <summary>
        /// register instance
        /// </summary>
        /// <typeparam name="TImplementation"></typeparam>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        void RegisterSingleton<TService, TImplementation>() where TImplementation : TService;

        /// <summary>
        /// register instance
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        void RegisterSingleton(Type type);


        /// <summary>
        /// register instance
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implementationType"></param> 
        /// <returns></returns>
        void RegisterSingleton(Type serviceType, Type implementationType);


        /// <summary>
        /// register many instance
        /// </summary>
        /// <param name="serviceTypes"></param>    
        /// <param name="implementationType"></param> 
        /// <returns></returns>
        void RegisterManySingleton(Type[] serviceTypes, Type implementationType);

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
