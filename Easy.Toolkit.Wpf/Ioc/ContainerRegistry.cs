using System.Windows;

namespace Easy.Toolkit
{
    internal sealed class ContainerRegistry : IContainerRegistry
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal Container Container = new();


        [EditorBrowsable(EditorBrowsableState.Never)]
        public IContainerProvider Buidler()
        {
            return new ContainerProvider(this);
        }

        public IRegisteredType Register<Target>(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            return Container.Register<Target>(type);
        }

        public IRegisteredType Register<TService, TImplementation>() where TImplementation : TService
        {
            return Container.Register<TService, TImplementation>();
        }

        public IRegisteredType Register<Target>(Func<object> factory)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            return Container.Register<Target>(factory);
        }

        public IRegisteredType Register<Target>()
        {
            return Container.Register<Target>();
        }


        public IRegisteredType Register(Type serviceType, Type implementationType)
        {
            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }
            if (implementationType is null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }
            return Container.Register(new[] { serviceType }, implementationType);
        }

        public IRegisteredType Register(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            return Container.Register(type);
        }


        public void RegisterSingleton<Target>(Target target) where Target : class
        {
            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            Container.Register<Target>(() => target).AsSingleton();
        }

        public void RegisterSingleton<Target>() where Target : class
        {

            Container.Register<Target>().AsSingleton();
        }

        public void RegisterSingleton<Target>(Func<object> factory)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            Container.Register<Target>(factory).AsSingleton();
        }

        public void RegisterSingleton<TService, TImplementation>() where TImplementation : TService
        {
            Container.Register<TService, TImplementation>().AsSingleton();
        }

        public void RegisterSingleton(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            Container.Register(type).AsSingleton();
        }

        public void RegisterSingleton(Type serviceType, Type implementationType)
        {
            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationType is null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }
            Container.Register(new[] { serviceType }, implementationType).AsSingleton();
        }

        public IRegisteredType RegisterMany(Type[] serviceTypes, Type implementationType)
        {
            if (serviceTypes is null || serviceTypes.Length == 0)
            {
                throw new ArgumentNullException(nameof(serviceTypes));
            }
            if (implementationType is null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }
            return Container.Register(serviceTypes, implementationType);
        }

        public void RegisterManySingleton(Type[] serviceTypes, Type implementationType)
        {
            if (serviceTypes is null || serviceTypes.Length == 0)
            {
                throw new ArgumentNullException(nameof(serviceTypes));
            }
            if (implementationType is null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }
            Container.Register(serviceTypes, implementationType).AsSingleton();
        }
    }


    internal sealed class ContainerProvider : IContainerProvider
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal Container Container;

        internal ContainerProvider(ContainerRegistry containerRegistry)
        {
            Container = containerRegistry.Container;
        }


        public Target Resolve<Target>()
        {
            if (Resolve(typeof(Target)) is Target target)
            {
                return target;
            }

            return default;
        }

        public object Resolve(Type type)
        {
            object @object = Container.GetService(type);

            if (@object is not FrameworkElement frameworkElement || frameworkElement.DataContext is not null)
            {
                return @object;
            }

            if (ViewRegisterExtensions.viewTypeAwares.TryGetValue(type, out ViewModelLocator.ViewViewModelAware aware) == false)
            {
                return @object;
            }

            if (aware.ViewModelType is null && aware.AutoWareViewModel == true)
            {
                aware.ViewModelType = ViewModelLocator.defaultViewTypeToViewModelTypeResolver(aware.ViewType);
            }

            if (aware.ViewModelType is null)
            {
                if (aware.ThrowExceptionWhenViewModelNotExist == true)
                {
                    throw new Exception("no valid view model type matched");
                }
            }
            else
            {
                aware.AutoWareViewModel = false;

                if (ContainerLocator.Container is ContainerProvider container)
                {
                    if (container.Container.IsRegistered(aware.ViewModelType) == false)
                    {
                        ContainerLocator.Registry.Register(aware.ViewModelType).AsSingleton();
                    }
                }

                frameworkElement.DataContext = ContainerLocator.Container.Resolve(aware.ViewModelType);
            }

            return @object;
        }
    }
}
