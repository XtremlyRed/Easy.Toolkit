using System.ComponentModel;
using System.Diagnostics;

namespace Easy.Toolkit
{
    /// <summary>
    /// container locator
    /// </summary>
    public static class ContainerLocator
    {
        /// <summary>
        /// container registry
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static IContainerRegistry Registry { get; private set; }

        /// <summary>
        /// container privider
        /// </summary>
        public static IContainerProvider Container { get; private set; }


        /// <summary>
        /// initialize default container locator
        /// </summary>
        static ContainerLocator()
        {
            ContainerRegistry registry = new();
            Container = new ContainerProvider(registry);

            registry.Register<Messenger, IMessenger>().AsSingleton();

            registry.Register<NavigationManager, INavigationManager>().AsSingleton();

            Registry = registry;
        }
    }
}
