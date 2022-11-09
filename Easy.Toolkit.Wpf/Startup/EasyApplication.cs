global using System;
global using System.ComponentModel;
global using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

[assembly: XmlnsDefinition("http://easy.toolkit.org", "Easy.Toolkit")]

namespace Easy.Toolkit
{

    public abstract class EasyApplication : System.Windows.Application
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        private Window window;


        public new Uri StartupUri { get; set; }
        public static new EasyApplication Current { get; private set; }
        public static new System.Windows.Threading.Dispatcher Dispatcher { get; private set; }
        protected sealed override void OnStartup(StartupEventArgs e)
        {
            ThemesInitialize.Initialize(this);
            Initialize();
            base.OnStartup(e);
        }

        protected EasyApplication()
        {
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
