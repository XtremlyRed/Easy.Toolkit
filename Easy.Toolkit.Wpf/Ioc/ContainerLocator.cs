using Easy.Toolkit.Dialogs;
using Easy.Toolkit.Dialogs.Defaults;

namespace Easy.Toolkit
{
    /// <summary>
    /// container locator
    /// </summary>
    public static class ContainerLocator
    {
        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static IContainerProvider container;
        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static IContainerRegistry registry;

        /// <summary>
        /// container registry
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public static IContainerRegistry Registry => registry;

        /// <summary>
        /// container privider
        /// </summary>
        public static IContainerProvider Container => container;

        /// <summary>
        /// initialize default container locator
        /// </summary>
        [DebuggerNonUserCode]
        static ContainerLocator()
        {
            ContainerRegistry registry1 = new();
            RegisterDefault(registry1);
        }

        [DebuggerNonUserCode]
        public static void SetContainer<TContainer>()
            where TContainer : IContainerRegistry, new()
        {
            TContainer registry1 = new TContainer();
            RegisterDefault(registry1);
        }


        [DebuggerNonUserCode]
        private static void RegisterDefault(IContainerRegistry registry1)
        { 
            registry1.Register<Messenger, IMessenger>().AsSingleton();
            registry1.Register<PopupManager, IPopupManager>().AsSingleton();
            registry1.Register<NavigationManager, INavigationManager>().AsSingleton();
            registry1.Register<DialogManager, IDialogManager>().AsSingleton();
            registry1.Register<DefaultDialogWindow, IDialogWindow>();
            ContainerLocator.registry = registry1;
            ContainerLocator.container = registry1.Buidler();
        }
    }
}
