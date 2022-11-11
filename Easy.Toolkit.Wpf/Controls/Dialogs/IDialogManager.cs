using System.Linq;
using System.Windows;

namespace Easy.Toolkit
{
    /// <summary>
    /// dialog manager interface
    /// </summary>
    public interface IDialogManager
    {
        /// <summary>
        /// <para> <c>explain:</c></para> 
        /// <para> use the method <see cref="ViewRegisterExtensions.RegisterView{TView}(IContainerRegistry, string, bool, bool)"/> to register view. </para>
        /// <para> the parameter <paramref name="viewName"/> is the viewName here</para> 
        /// </summary> 
        /// <param name="viewName"></param>
        /// <param name="parameters"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        void Show(string viewName, IDialogParameters parameters, Action<DialogResult> callback);


        /// <summary>
        /// <para> <c>explain:</c></para> 
        /// <para> use the method <see cref="ViewRegisterExtensions.RegisterView{TView}(IContainerRegistry, string, bool, bool)"/> to register view. </para>
        /// <para> the parameter <paramref name="viewName"/> is the viewName here</para> 
        /// </summary> 
        /// <param name="viewName"></param>
        /// <param name="parameters"></param>
        /// <param name="callback"></param> 
        /// <returns></returns>
        void ShowDialog(string viewName, IDialogParameters parameters, Action<DialogResult> callback);

    }

    /// <summary>
    /// dialog manager
    /// </summary>
    public class DialogManager : IDialogManager
    {
        /// <summary>
        /// get dialog host window style
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Style GetWindowStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(WindowStyleProperty);
        }

        /// <summary>
        /// set dialog host window style
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetWindowStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(WindowStyleProperty, value);
        }

        /// <summary>
        /// dialog host window style
        /// </summary>
        public static readonly DependencyProperty WindowStyleProperty =
            DependencyProperty.RegisterAttached("WindowStyle", typeof(Style),
            typeof(DialogManager),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (s, e) => { }));


        /// <summary>
        /// get dialog host window startup location
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static WindowStartupLocation GetWindowStartupLocation(DependencyObject obj)
        {
            return (WindowStartupLocation)obj.GetValue(WindowStartupLocationProperty);
        }

        /// <summary>
        /// set dialog host window startup location
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetWindowStartupLocation(DependencyObject obj, WindowStartupLocation value)
        {
            obj.SetValue(WindowStartupLocationProperty, value);
        }

        /// <summary>
        /// dialog host window startup location
        /// </summary>
        public static readonly DependencyProperty WindowStartupLocationProperty =
            DependencyProperty.RegisterAttached("WindowStartupLocation", typeof(WindowStartupLocation),
            typeof(DialogManager),
            new FrameworkPropertyMetadata(WindowStartupLocation.CenterOwner, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (s, e) => { }));

        /// <summary>
        /// get dialog host window size to content
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static SizeToContent GetSizeToContent(DependencyObject obj)
        {
            return (SizeToContent)obj.GetValue(SizeToContentProperty);
        }

        /// <summary>
        /// set dialog host window size to content
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetSizeToContent(DependencyObject obj, SizeToContent value)
        {
            obj.SetValue(SizeToContentProperty, value);
        }

        /// <summary>
        /// dialog host window size to content
        /// </summary>
        public static readonly DependencyProperty SizeToContentProperty =
            DependencyProperty.RegisterAttached("SizeToContent", typeof(SizeToContent),
            typeof(DialogManager),
            new FrameworkPropertyMetadata(SizeToContent.WidthAndHeight, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (s, e) => { }));


        /// <summary>
        /// <para> <c>explain:</c></para> 
        /// <para> use the method <see cref="ViewRegisterExtensions.RegisterView{TView}(IContainerRegistry, string, bool, bool)"/> to register view. </para>
        /// <para> the parameter <paramref name="viewName"/> is the viewName here</para> 
        /// </summary>
        /// <param name="navigationIdentity"></param>
        /// <param name="viewName"></param>
        /// <param name="navigationParameters"></param>
        /// <returns></returns>
        public void Show(string viewName, IDialogParameters parameters, Action<DialogResult> callback)
        {
            InnerDisplay(false, viewName, parameters, callback);
        }

        /// <summary>
        /// <para> <c>explain:</c></para> 
        /// <para> use the method <see cref="ViewRegisterExtensions.RegisterView{TView}(IContainerRegistry, string, bool, bool)"/> to register view. </para>
        /// <para> the parameter <paramref name="viewName"/> is the viewName here</para> 
        /// </summary>
        /// <param name="navigationIdentity"></param>
        /// <param name="viewName"></param>
        /// <param name="navigationParameters"></param>
        /// <returns></returns>
        public void ShowDialog(string viewName, IDialogParameters parameters, Action<DialogResult> callback)
        {
            InnerDisplay(true, viewName, parameters, callback);
        }


        private void InnerDisplay(bool isModal, string viewName, IDialogParameters parameters, Action<DialogResult> callback)
        {
            if (ViewRegisterExtensions.viewNameAwares.TryGetValue(viewName, out ViewModelLocator.ViewViewModelAware aware) == false)
            {
                throw new Exception($"view name : {viewName}  not registered");
            }

            parameters ??= new DialogParameters();

            IDialogWindow window = ContainerLocator.Container.Resolve<IDialogWindow>();

            object objContent = ContainerLocator.Container.Resolve(aware.ViewType);

            if (objContent is not FrameworkElement element)
            {
                throw new Exception($"dialog  content must be a FrameworkElement");
            }

            if (element.DataContext is not IDialogViewModelAware viewModelAware)
            {
                throw new Exception($"dialog data context must implement the {typeof(IDialogViewModelAware)}");
            }

            if (window.Style is null)
            {
                window.Style = GetWindowStyle(element);
            }
            window.SizeToContent = GetSizeToContent(element);
            window.WindowStartupLocation = GetWindowStartupLocation(element);
            window.Content = objContent;

            window.DataContext = viewModelAware; //share data context

            if (window.Owner == null)
            {
                window.Owner = Application.Current?.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
            }

            ConfigureEvents(window, callback, () => viewModelAware.OnDialogOpened(parameters));

            if (isModal)
            {
                window.ShowDialog();
                return;
            }

            window.Show();

        }


        /// <summary>
        ///  configure Events
        /// </summary>
        /// <param name="dialogWindow">The dialog window.</param>
        /// <param name="callback">The action invoked when the dialog is closed.</param>
        private void ConfigureEvents(IDialogWindow dialogWindow, Action<DialogResult> callback, Action openCallback)
        {
            Action<DialogResult> requestCloseHandler = null;
            requestCloseHandler = (re) =>
            {
                dialogWindow.Result = re;
                dialogWindow.Close();
            };

            RoutedEventHandler loadedHandler = null;
            loadedHandler = (s, e) =>
            {
                dialogWindow.Loaded -= loadedHandler;
                if (dialogWindow.DataContext is IDialogViewModelAware viewModelAware)
                {
                    viewModelAware.RequestClose += requestCloseHandler;
                }
                openCallback?.Invoke();
            };
            dialogWindow.Loaded += loadedHandler;

            CancelEventHandler closingHandler = null;
            closingHandler = (s, e) =>
            {
                if (dialogWindow.DataContext is IDialogViewModelAware viewModelAware && viewModelAware.CanClose == false)
                {
                    e.Cancel = true;
                }
            };
            dialogWindow.Closing += closingHandler;

            EventHandler closedHandler = null;
            closedHandler = (s, e) =>
            {
                dialogWindow.Closed -= closedHandler;
                dialogWindow.Closing -= closingHandler;

                if (dialogWindow.DataContext is IDialogViewModelAware viewModelAware)
                {
                    viewModelAware.RequestClose -= requestCloseHandler;
                    viewModelAware.OnDialogClosed();
                }

                dialogWindow.DataContext = null;
                dialogWindow.Content = null;

                callback?.Invoke(dialogWindow.Result);
            };
            dialogWindow.Closed += closedHandler;
        }



    }
}
