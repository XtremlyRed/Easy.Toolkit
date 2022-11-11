global using System;
global using System.ComponentModel;
global using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

[assembly: XmlnsDefinition("http://easy.toolkit.org", "Easy.Toolkit")]

namespace Easy.Toolkit
{

    /// <summary>
    /// Encapsulates an easy toolkit application.
    /// </summary>
    public abstract class EasyApplication : System.Windows.Application
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        private Window window;

        /// <summary>
        /// UI thread synchronization context
        /// </summary>
        public static SynchronizationContext SynchronizationContext;


        /// <summary>
        /// Gets or sets a UI that is automatically shown when an application starts.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Uri StartupUri { get; set; }

        /// <summary>
        ///  Gets the System.Windows.Application object for the current System.AppDomain.
        /// </summary>
        public static new EasyApplication Current { get; private set; }

        /// <summary>
        ///    Gets the System.Windows.Threading.Dispatcher this System.Windows.Threading.DispatcherObject is associated with.
        /// </summary>
        public static new System.Windows.Threading.Dispatcher Dispatcher { get; private set; }

        /// <summary>
        ///  Raises the System.Windows.Application.Startup event.
        /// </summary>
        /// <param name="e"></param>
        protected sealed override void OnStartup(StartupEventArgs e)
        {
            ThemesInitialize.Initialize(this);
            Initialize();
            base.OnStartup(e);
        }

        /// <summary>
        /// create easy application instance
        /// </summary>
        protected EasyApplication()
        {
            SynchronizationContext = SynchronizationContext.Current;
            Current = this;
            Dispatcher = base.Dispatcher;
            TypeContainerRegistry(ContainerLocator.Registry);
        }



        /// <summary>
        /// Initialize
        /// </summary>
        protected virtual void Initialize()
        {
            base.MainWindow = window = CreateMainWindow(ContainerLocator.Container);
            OnInitialized();
        }

        /// <summary>
        /// IocContainerRegistry
        /// </summary>
        /// <param name="registry"></param>
        protected abstract void TypeContainerRegistry(IContainerRegistry registry);

        /// <summary>
        /// CreateMainWindow
        /// </summary>
        /// <returns></returns>
        protected abstract System.Windows.Window CreateMainWindow(IContainerProvider container);

        /// <summary> 
        /// </summary>
        protected virtual void OnInitialized()
        {
            if (window is null)
            {
                Environment.Exit(0);
                return;
            }
            window.Show();
        }
    }
}
