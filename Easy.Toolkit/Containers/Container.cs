using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Easy.Toolkit
{

    /// <summary>
    /// Inversion of control container handles dependency injection for registered types
    /// </summary>
    public class Container : IContainer
    {
        // Map of registered types
        private readonly Dictionary<Type, FunCreator> registeredTypes = new Dictionary<Type, FunCreator>();

        // Lifetime management
        private readonly ContainerLifetime lifetime;

        /// <summary>
        /// Creates a new instance of IoC Container
        /// </summary>
        public Container()
        {
            lifetime = new ContainerLifetime(t => registeredTypes[t].Creator);
        }

        /// <summary>
        /// IsRegistered
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public bool IsRegistered(Type serviceType)
        {
            return registeredTypes.ContainsKey(serviceType);
        }


        /// <summary>
        /// Registers a factory function which will be called to resolve the specified serviceTypes
        /// </summary>
        /// <param name="serviceType">Interface to register</param>
        /// <param name="factory">Factory function</param>
        /// <returns></returns>
        public IRegisteredType Register(Type serviceType, Func<object> factory)
        {
            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return RegisterType(new[] { serviceType }, _ => factory());
        }

        /// <summary>
        /// Registers an implementationType type for the specified serviceTypes
        /// </summary>
        /// <param name="serviceTypes">Interface to register</param>
        /// <param name="implementationType">Implementing type</param>
        /// <returns></returns>
        public IRegisteredType Register(Type implementationType, Type[] serviceTypes)
        {
            if (implementationType is null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }
            if (serviceTypes is null || serviceTypes.Length == 0)
            {
                throw new ArgumentNullException(nameof(serviceTypes));
            }

            return RegisterType(serviceTypes, FactoryFromType(implementationType));
        }



        private IRegisteredType RegisterType(Type[] serviceTypes, Func<ILifetime, object> factory)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            return new RegisteredType(serviceTypes, (t, creator) => registeredTypes[t] = creator, new FunCreator(factory));
        }


        /// <summary>
        /// Returns the object registered for the given type, if registered
        /// </summary>
        /// <param name="type">Type as registered with the container</param>
        /// <returns>Instance of the registered type, if registered; otherwise <see langword="null"/></returns>
        public T GetService<T>()
        {
            if (GetService(typeof(T)) is T target)
            {
                return target;
            }

            return default;
        }


        /// <summary>
        /// Returns the object registered for the given type, if registered
        /// </summary>
        /// <param name="serviceType">Type as registered with the container</param>
        /// <returns>Instance of the registered type, if registered; otherwise <see langword="null"/></returns>
        public object GetService(Type serviceType)
        {
            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (registeredTypes.TryGetValue(serviceType, out FunCreator registeredType) == false)
            {
                return default;
            }

            return registeredType.Creator(lifetime);
        }


        /// <summary>
        /// Creates a new scope
        /// </summary>
        /// <returns>Scope object</returns>
        public IContainer CreateScope()
        {
            return new ScopeLifetime(lifetime);
        }

        /// <summary>
        /// Disposes any <see cref="IDisposable"/> objects owned by this container.
        /// </summary>
        public void Dispose()
        {
            lifetime.Dispose();
        }

        internal struct FunCreator
        {
            public FunCreator(Func<ILifetime, object> creator)
            {
                Creator = creator;

                HashCode = creator.GetHashCode();
            }

            public Func<ILifetime, object> Creator { get; }


            public readonly int HashCode;

            public override int GetHashCode()
            {
                return HashCode;
            }

        }


        #region Lifetime management
        // ILifetime management adds resolution strategies to an IContainer
        internal interface ILifetime : IContainer
        {
            object GetServiceAsSingleton(Type type, FunCreator factory);

            object GetServicePerScope(Type type, FunCreator factory);
        }

        // ObjectCache provides common caching logic for lifetimes
        internal abstract class ObjectCache : IDisposable
        {
            // Instance cache
            private readonly ConcurrentDictionary<FunCreator, object> _instanceCache = new ConcurrentDictionary<FunCreator, object>();

            // Get from cache or create and cache object
            protected object GetCached(Type type, FunCreator factory, ILifetime lifetime)
            {
                return _instanceCache.GetOrAdd(factory, _ => factory.Creator(lifetime));
            }

            public void Dispose()
            {
                foreach (object obj in _instanceCache.Values)
                {
                    (obj as IDisposable)?.Dispose();
                }

                _instanceCache?.Clear();
            }
        }

        // Container lifetime management
        private class ContainerLifetime : ObjectCache, ILifetime
        {
            // Retrieves the factory functino from the given type, provided by owning container
            public Func<Type, Func<ILifetime, object>> GetFactory { get; private set; }

            public ContainerLifetime(Func<Type, Func<ILifetime, object>> getFactory)
            {
                GetFactory = getFactory;
            }

            public object GetService(Type type)
            {
                return GetFactory(type)(this);
            }

            // Singletons get cached per container
            public object GetServiceAsSingleton(Type type, FunCreator factory)
            {
                return GetCached(type, factory, this);
            }

            // At container level, per-scope items are equivalent to singletons
            public object GetServicePerScope(Type type, FunCreator factory)
            {
                return GetServiceAsSingleton(type, factory);
            }
        }

        // Per-scope lifetime management
        private class ScopeLifetime : ObjectCache, ILifetime
        {
            // Singletons come from parent container's lifetime
            private readonly ContainerLifetime _parentLifetime;

            public ScopeLifetime(ContainerLifetime parentContainer)
            {
                _parentLifetime = parentContainer;
            }

            public object GetService(Type type)
            {
                return _parentLifetime.GetFactory(type)(this);
            }

            // Singleton resolution is delegated to parent lifetime
            public object GetServiceAsSingleton(Type type, FunCreator factory)
            {
                return _parentLifetime.GetServiceAsSingleton(type, factory);
            }

            // Per-scope objects get cached
            public object GetServicePerScope(Type type, FunCreator factory)
            {
                return GetCached(type, factory, this);
            }
        }
        #endregion

        #region Container items

        // Compiles a lambda that calls the given type's first constructor resolving arguments
        private static Func<ILifetime, object> FactoryFromType(Type itemType)
        {
            // Get first constructor for the type
            ConstructorInfo[] constructors = itemType.GetConstructors();
            if (constructors.Length == 0)
            {
                // If no public constructor found, search for an internal constructor
                constructors = itemType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
            }
            ConstructorInfo constructor = constructors[0];

            // Compile constructor call as a lambda expression
            ParameterExpression arg = Expression.Parameter(typeof(ILifetime));
            return (Func<ILifetime, object>)Expression.Lambda(
                Expression.New(constructor, constructor.GetParameters().Select(
                    param =>
                    {
                        Func<ILifetime, object> resolve = new Func<ILifetime, object>(lifetime => lifetime.GetService(param.ParameterType));
                        return Expression.Convert(
                            Expression.Call(Expression.Constant(resolve.Target), resolve.Method, arg),
                            param.ParameterType);
                    })),
                arg).Compile();
        }

        // RegisteredType is supposed to be a short lived object tying an item to its container
        // and allowing users to mark it as a singleton or per-scope item
        private class RegisteredType : IRegisteredType
        {
            private readonly Type[] _itemTypes;
            private readonly Action<Type, FunCreator> _registerFactory;
            private readonly FunCreator _factory;

            public RegisteredType(Type[] itemTypes, Action<Type, FunCreator> registerFactory, FunCreator factory)
            {
                _itemTypes = itemTypes;
                _registerFactory = registerFactory;
                _factory = factory;

                AsTransient();
            }

            private void AsTransient()
            {
                foreach (Type _itemType in _itemTypes)
                {
                    _registerFactory(_itemType, _factory);
                }
            }

            public void AsSingleton()
            {
                foreach (Type _itemType in _itemTypes)
                {
                    _registerFactory(_itemType, new FunCreator(lifetime => lifetime.GetServiceAsSingleton(_itemType, _factory)));
                }
            }

            public void PerScope()
            {
                foreach (Type _itemType in _itemTypes)
                {
                    _registerFactory(_itemType, new FunCreator(lifetime => lifetime.GetServicePerScope(_itemType, _factory)));
                }
            }
        }
        #endregion
    }
}