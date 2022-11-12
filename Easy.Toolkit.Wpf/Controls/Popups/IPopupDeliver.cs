using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Easy.Toolkit
{
    /// <summary>
    /// popup deliver
    /// </summary>
    public interface IPopupDeliver
    {
        /// <summary>
        /// popup message in popup control 
        /// </summary> 
        /// <param name="showMessage"></param>
        /// <returns></returns>
        Task ShowAsync(string showMessage);
        /// <summary>
        /// popup confirm message in popup control  
        /// </summary> 
        /// <param name="confirmMessage"></param>
        /// <returns></returns>
        Task<bool> ConfirmAsync(string confirmMessage);


        /// <summary>
        /// <para> <c>explain:</c>></para> 
        /// <para> use the method <see cref="ViewRegisterExtensions.RegisterView{TView}(IContainerRegistry, string, bool, bool)"/> to register view. </para>
        /// <para> the parameter <paramref name="viewName"/> is the viewName here</para> 
        /// <para> the view must implement the <see cref="IPopupView"/></para>
        /// <para> the view's data content must implement the <see cref="IPopupViewModelAware"/></para>
        /// </summary> 
        /// <param name="viewName"></param>
        /// <param name="popupParameters"></param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<object> PopupAsync(string viewName, IPopupParameters popupParameters = null);

    }


    /// <summary>
    /// popup deliver
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class PopupDeliver : IPopupDeliver
    {
        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal IPopupControl Popup;
        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal Dispatcher Dispatcher;


        /// <summary>
        /// popup confirm message in popup control 
        /// </summary> 
        /// <param name="confirmMessage"></param>
        /// <returns></returns>
        public Task<bool> ConfirmAsync(string confirmMessage)
        {
            return Popup.ConfirmAsync(confirmMessage);
        }


        /// <summary>
        /// popup message in popup control 
        /// </summary> 
        /// <param name="showMessage"></param>
        /// <returns></returns>
        public Task ShowAsync(string showMessage)
        {
            return Popup.ShowAsync(showMessage);
        }


        /// <summary>
        /// <para> <c>explain:</c>></para> 
        /// <para> use the method <see cref="ViewRegisterExtensions.RegisterView{TView}(IContainerRegistry, string, bool, bool)"/> to register view. </para>
        /// <para> the parameter <paramref name="viewName"/> is the viewName here</para> 
        /// <para> the view must implement the <see cref="IPopupView"/></para>
        /// <para> the view's data content must implement the <see cref="IPopupViewModelAware"/></para>
        /// </summary> 
        /// <param name="viewName"></param>
        /// <param name="popupParameters"></param>
        /// <returns></returns>
        public Task<object> PopupAsync(string viewName, IPopupParameters popupParameters = null)
        {
            if (ViewRegisterExtensions.viewNameAwares.TryGetValue(viewName, out ViewModelLocator.ViewViewModelAware aware) == false)
            {
                throw new Exception($"view name : {viewName}  not registered");
            }

            var view = ContainerLocator.Container.Resolve(aware.ViewType);

            if (view is not FrameworkElement frameworkElement)
            {
                throw new Exception($"the view must be a {typeof(FrameworkElement)} control");
            }

            if (view is not IPopupView popupView)
            {
                throw new Exception($"the view must implement the {typeof(IPopupView)}");
            }

            if (frameworkElement.DataContext is not IPopupViewModelAware viewModelAware)
            {
                throw new Exception($"the view's data content must implement the {typeof(IPopupViewModelAware)}");
            }

            ConfigureEvents(popupView, viewModelAware, popupParameters ?? new PopupParameters());

            return Popup.PopupAsync(() => popupView);
        }



        /// <summary>
        ///  configure Events
        /// </summary> 
        private void ConfigureEvents(IPopupView popupView, IPopupViewModelAware viewModelAware, IPopupParameters popupParameters)
        {
            EventHandler<PopupResultEventArgs> requestCloseHandler = null;
            RoutedEventHandler loadedHandler = null;

            loadedHandler = (s, e) =>
            {
                popupView.Loaded -= loadedHandler;
                viewModelAware.OnPopupOpened(popupParameters);
            };

            requestCloseHandler = (sender, result) =>
            {
                popupView.RequestClosePopup -= requestCloseHandler;
                popupView.Loaded -= loadedHandler;
                viewModelAware.OnPopupClosed(result);
            };

            popupView.RequestClosePopup += requestCloseHandler;
            popupView.Loaded += loadedHandler;

        }

    }

   
}
