using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Easy.Toolkit
{
    /// <summary>
    /// navigation manager
    /// </summary>
    public interface INavigationManager

    {
        /// <summary>
        /// get navigation proxy  by <paramref name="navigationIdentity"/>
        /// <para> <c>explain:</c>></para>
        /// <para> use thr property <see cref="NavigationControl.Identity"/> to register <paramref name="navigationIdentity"/></para> 
        /// </summary>
        /// <param name="navigationIdentity"></param>
        /// <returns></returns>
        INavigationDeliver this[string navigationIdentity] { get; }


        /// <summary>
        /// <para> <c>explain:</c>></para>
        /// <para> use thr property <see cref="NavigationControl.Identity"/> to register <paramref name="navigationIdentity"/></para>
        /// <para> use the method <see cref="ViewRegisterExtensions.RegisterView{TView}(IContainerRegistry, string, bool, bool)"/> to register view. </para>
        /// <para> the parameter <paramref name="viewName"/> is the viewName here</para> 
        /// </summary>
        /// <param name="navigationIdentity"></param>
        /// <param name="viewName"></param>
        /// <param name="navigationParameters"></param>
        /// <returns></returns>
        Task NavigateToAsync(string navigationIdentity, string viewName, INavigationParameters navigationParameters = null);

        /// <summary>
        /// navigation back in navigation from <paramref name="navigationIdentity"/>
        /// <para> <c>explain:</c>></para>
        /// <para> use thr property <see cref="NavigationControl.Identity"/> to register <paramref name="navigationIdentity"/></para> 
        /// </summary>
        /// <param name="navigationIdentity"></param>
        /// <returns></returns>
        Task NavigateBackAsync(string navigationIdentity);
    }

    public sealed class NavigationManager : INavigationManager
    {
        /// <summary>
        /// get whether an element ( must be implementation <see cref="INavigationControl"/> ) is set to navigation host
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool? GetIsNavigateHost(DependencyObject obj)
        {
            return (bool?)obj.GetValue(IsNavigateHostProperty);
        }

        /// <summary>
        /// set ui element as navigate host ,but the ui element must be implementation <see cref="INavigationControl"/>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetIsNavigateHost(DependencyObject obj, bool? value)
        {
            obj.SetValue(IsNavigateHostProperty, value);
        }

        /// <summary>
        /// is navigation host
        /// </summary>
        public static readonly DependencyProperty IsNavigateHostProperty =
            DependencyProperty.RegisterAttached("IsNavigateHost", typeof(bool?),
            typeof(NavigationManager),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (s, e) =>
            {
                if (s is not INavigationControl control)
                {
                    throw new Exception($"{s.GetType()} must inherit interface:{typeof(INavigationControl)}");
                }
                  

                if (e.NewValue is true)
                {
                    if (navigationAwares.ContainsKey(control))
                    {
                        return;
                    }
                    navigationAwares[control] = new NavigationDeliver()
                    {
                        Navigation = control,
                        Dispatcher = s.Dispatcher,
                    };
                    return;
                }

                if (!navigationAwares.TryGetValue(control, out NavigationDeliver value))
                {
                    return;
                }

                navigationAwares.Remove(control);

                if (value == null)
                {
                    return;
                }

                value.Navigation = null;
                value.Dispatcher = null;
                value = null;

            }));






        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal static Dictionary<INavigationControl, NavigationDeliver> navigationAwares = new Dictionary<INavigationControl, NavigationDeliver>();


        /// <summary>
        /// get navigation proxy  by <paramref name="navigationIdentity"/>
        /// <para> <c>explain:</c>></para>
        /// <para> use thr property <see cref="NavigationControl.Identity"/> to register <paramref name="navigationIdentity"/></para> 
        /// </summary>
        /// <param name="navigationIdentity"></param>
        /// <returns></returns>
        public INavigationDeliver this[string navigationIdentity]
        {
            get
            {
                if (navigationIdentity is null)
                {
                    throw new ArgumentNullException(nameof(navigationIdentity));
                }

                NavigationDeliver proxy = navigationAwares.Values.FirstOrDefault(i => string.Compare(i.Navigation?.Identity, navigationIdentity) == 0);

                return proxy;
            }
        }

        /// <summary>
        /// navigation back in navigation from <paramref name="navigationIdentity"/>
        /// <para> <c>explain:</c>></para>
        /// <para> use thr property <see cref="NavigationControl.Identity"/> to register <paramref name="navigationIdentity"/></para> 
        /// </summary>
        /// <param name="navigationIdentity"></param>
        /// <returns></returns>
        public Task NavigateBackAsync(string navigationIdentity)
        {
            if (navigationIdentity is null)
            {
                throw new ArgumentNullException(nameof(navigationIdentity));
            }

            NavigationDeliver proxy = navigationAwares.Values.FirstOrDefault(i => string.Compare(i.Navigation?.Identity, navigationIdentity) == 0);

            if (proxy is null)
            {
                return Task.FromResult(false);
            }

            return proxy.NavigateBackAsync();
        }
        /// <summary>
        /// <para> <c>explain:</c>></para>
        /// <para> use thr property <see cref="NavigationControl.Identity"/> to register <paramref name="navigationIdentity"/></para>
        /// <para> use the method <see cref="ViewRegisterExtensions.RegisterView{TView}(IContainerRegistry, string, bool, bool)"/> to register view. </para>
        /// <para> the parameter <paramref name="viewName"/> is the viewName here</para> 
        /// </summary>
        /// <param name="navigationIdentity"></param>
        /// <param name="viewName"></param>
        /// <param name="navigationParameters"></param>
        /// <returns></returns>
        public Task NavigateToAsync(string navigationIdentity, string viewName, INavigationParameters navigationParameters = null)
        {
            if (navigationIdentity is null)
            {
                throw new ArgumentNullException(nameof(navigationIdentity));
            }

            NavigationDeliver proxy = navigationAwares.Values.FirstOrDefault(i => string.Compare(i.Navigation?.Identity, navigationIdentity) == 0);

            if (proxy is null)
            {
                return Task.FromResult(false);
            }

            return proxy.NavigateToAsync(viewName, navigationParameters ?? new NavigationParameters());
        }
    }
}
