using Easy.Toolkit.Dialogs.Defaults;

namespace Easy.Toolkit
{
    /// <summary>
    /// container locator
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
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

        /// <summary>
        /// replace default Container
        /// </summary>
        /// <typeparam name="TContainer"></typeparam>
        [DebuggerNonUserCode]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void SetContainer<TContainer>() where TContainer : IContainerRegistry, new()
        {
            TContainer registry1 = new TContainer();
            RegisterDefault(registry1);
        }


        [DebuggerNonUserCode]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private static void RegisterDefault(IContainerRegistry registry1)
        {
            registry1.Register<IMessenger, Messenger>().AsSingleton();
            registry1.Register<IPopupManager, PopupManager>().AsSingleton();
            registry1.Register<INavigationManager, NavigationManager>().AsSingleton();
            registry1.Register<IDialogManager, DialogManager>().AsSingleton();
            registry1.RegisterDialogWindow<DefaultDialogWindow>();
            ContainerLocator.registry = registry1;
            ContainerLocator.container = registry1.Buidler();
        }
    }
}
