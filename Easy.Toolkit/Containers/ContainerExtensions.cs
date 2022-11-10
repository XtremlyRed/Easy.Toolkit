using System;

namespace Easy.Toolkit
{
    /// <summary>
    /// Extension methods for Container
    /// </summary>
    public static partial class ContainerExtensions
    {
        /// <summary>
        /// Registers an implementation implementationType for the specified interface
        /// </summary>
        /// <typeparam name="T">service Type to register</typeparam>
        /// <param name="container">This container instance</param>
        /// <param name="implementationType">Implementing implementationType</param>
        /// <returns>IRegisteredType object</returns>
        public static IRegisteredType Register<T>(this Container container, Type implementationType)
        {
            return container.Register(typeof(T), new[] { implementationType });
        }

        /// <summary>
        /// Registers an implementation implementationType for the specified interface
        /// </summary>
        /// <typeparam name="TServiceType">Interface to register</typeparam>
        /// <typeparam name="TImplementationType">Implementing implementationType</typeparam>
        /// <param name="container">This container instance</param>
        /// <returns>IRegisteredType object</returns>
        public static IRegisteredType Register<TImplementationType, TServiceType>(this Container container)
            where TImplementationType : TServiceType
        {
            return container.Register(typeof(TImplementationType), new[] { typeof(TServiceType) });
        }



        /// <summary>
        /// Registers an implementation implementationType for the specified interface
        /// </summary>
        /// <typeparam name="TInterface">Interface to register</typeparam>
        /// <typeparam name="TImplementation">Implementing implementationType</typeparam>
        /// <param name="container">This container instance</param>
        /// <returns>IRegisteredType object</returns>
        public static IRegisteredType Register(this Container container, Type implementationType, Type[] serviceTypes)
        {
            if (serviceTypes is null || serviceTypes.Length == 0)
            {
                throw new ArgumentNullException(nameof(serviceTypes));
            }

            return container.Register(implementationType, serviceTypes);
        }


        /// <summary>
        /// Registers a factory function which will be called to resolve the specified interface
        /// </summary>
        /// <typeparam name="T">Interface to register</typeparam>
        /// <param name="container">This container instance</param>
        /// <param name="factory">Factory method</param>
        /// <returns>IRegisteredType object</returns>
        public static IRegisteredType Register<T>(this Container container, Func<object> factory)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return container.Register(typeof(T), factory);
        }

        /// <summary>
        /// Registers a implementationType
        /// </summary>
        /// <param name="container">This container instance</param>
        /// <typeparam name="T">Type to register</typeparam>
        /// <returns>IRegisteredType object</returns>
        public static IRegisteredType Register<T>(this Container container)
        {
            return container.Register(typeof(T), new[] { typeof(T) });
        }

        /// <summary>
        /// Registers a implementationType
        /// </summary>
        /// <param name="container">This container instance</param>
        /// <typeparam name="T">Type to register</typeparam>
        /// <returns>IRegisteredType object</returns>
        public static IRegisteredType Register(this Container container, Type implementationType)
        {
            return container.Register(implementationType, new[] { implementationType });
        }

        /// <summary>
        /// Returns an implementation of the specified interface
        /// </summary>
        /// <typeparam name="T">Interface implementationType</typeparam>
        /// <param name="container">This container instance</param>
        /// <returns>Object implementing the interface</returns>
        public static T Resolve<T>(this Container container)
        {
            return (T)container.GetService(typeof(T));
        }


        /// <summary>
        /// Returns an implementation of the specified interface
        /// </summary>
        /// <typeparam name="T">Interface implementationType</typeparam>
        /// <param name="container">This container instance</param>
        /// <returns>Object implementing the interface</returns>
        public static object Resolve(this Container container, Type serviceType)
        {
            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            return container.GetService(serviceType);
        }

    }
}