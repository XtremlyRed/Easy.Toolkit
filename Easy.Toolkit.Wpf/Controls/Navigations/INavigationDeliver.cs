using System.Threading.Tasks;
using System.Windows.Threading;

namespace Easy.Toolkit
{
    /// <summary>
    ///  navigation deliver 
    /// </summary>
    public interface INavigationDeliver

    {
        /// <summary>
        /// navigation  identity
        /// </summary>
        string Identity { get; }

        /// <summary>
        /// <para> Use the method <see cref="ViewRegisterExtensions.RegisterView{TView}(IContainerRegistry, string, bool, bool)"/> to register views. </para>
        /// <para> The parameter <paramref name="viewName"/> is the viewName here</para>
        /// </summary>
        /// <param name="viewName">view name</param>
        /// <param name="navigationParameters"></param>
        /// <returns></returns>
        public Task NavigateToAsync(string viewName, NavigationParameters navigationParameters = null);

        /// <summary>
        /// navigation back
        /// </summary>
        /// <returns></returns>
        public Task NavigateBackAsync();
    }


    /// <summary>
    ///  navigation deliver 
    /// </summary>
    public sealed class NavigationDeliver : INavigationDeliver
    {

        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal INavigationControl Navigation;
        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal Dispatcher Dispatcher;

        /// <summary>
        /// navigation  identity
        /// </summary>
        public string Identity => Navigation?.Identity;


        /// <summary>
        /// navigation back
        /// </summary>
        /// <returns></returns>
        public Task NavigateBackAsync()
        {
            return Navigation.NavigateBackAsync();
        }


        /// <summary>
        /// <para> Use the method <see cref="ViewRegisterExtensions.RegisterView{TView}(IContainerRegistry, string, bool, bool)"/> to register views. </para>
        /// <para> The parameter <paramref name="viewName"/> is the viewName here</para>
        /// </summary>
        /// <param name="viewName">view name</param>
        /// <param name="navigationParameters"></param>
        /// <returns></returns>
        public async Task NavigateToAsync(string viewName, NavigationParameters navigationParameters = null)
        {
            if (viewName is null)
            {
                throw new ArgumentNullException(nameof(viewName));
            }

            if (ViewRegisterExtensions.viewNameAwares.TryGetValue(viewName, out ViewModelLocator.ViewViewModelAware viewViewModelAware) == false)
            {
                throw new Exception($"view:{viewName} not registered");
            }

            object view = await Dispatcher.InvokeAsync(() =>
            {
                return ContainerLocator.Container.Resolve(viewViewModelAware.ViewType);

            }, DispatcherPriority.Background);

            await Navigation.NavigateToAsync(view, navigationParameters);
        }
    }


}
