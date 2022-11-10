using System.Windows;

using Microsoft.Win32;

namespace Easy.Toolkit
{
    internal class ContainerRegistry : IContainerRegistry
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal Container Container = new();

        public IContainerProvider Buidler()
        {
            return new ContainerProvider(this); 
        }

        public IRegisteredType Register<Target>(Type type)
        {
            return Container.Register<Target>(type);
        }

        public IRegisteredType Register<TImplementation, TService>() where TImplementation : TService
        {
            return Container.Register<TImplementation, TService>();
        }

        public IRegisteredType Register<Target>(Func<object> factory)
        {
            return Container.Register<Target>(factory);
        }

        public IRegisteredType Register<Target>()
        {
            return Container.Register<Target>();
        }


        public IRegisteredType Register(Type implementationType, Type serviceType)
        {
            return Container.Register(implementationType, new[] { serviceType });
        }

        public IRegisteredType Register(Type type)
        {
            return Container.Register(type);
        }

        public void RegisterInstance<Target>(Target instace)
        {
            Container.Register<Target>(() => instace).AsSingleton();
        }

        public IRegisteredType RegisterMany(Type implementationType, Type[] serviceTypes)
        {
            return Container.Register(implementationType, serviceTypes);
        }
    }


    internal class ContainerProvider : IContainerProvider
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

            if (@object is FrameworkElement frameworkElement)
            {
                if (frameworkElement.DataContext is null)
                {
                    if (ViewRegisterExtensions.viewTypeAwares.TryGetValue(type, out ViewModelLocator.ViewViewModelAware aware))
                    {
                        if (aware.ViewModelType is null && aware.AutoWareViewModel == true)
                        {
                            aware.ViewModelType = ViewModelLocator.defaultViewTypeToViewModelTypeResolver(aware.ViewType);
                        }

                        if (aware.ViewModelType is null)
                        {
                            if (aware.NotExistViewModelThrowException == true)
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

                        
                    }
                }
            }


            return @object;
        }
    }
}
