﻿using System.Collections.Generic;
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
        INavigationDepute this[string navigationIdentity] { get; }


        /// <summary>
        /// <para> <c>explain:</c>></para>
        /// <para> use thr property <see cref="NavigationControl.Identity"/> to register <paramref name="navigationIdentity"/></para>
        /// <para> use the method <see cref="ViewModelLocator.RegisterView{TView}(IContainerRegistry, string, bool, bool)"/> to register view. </para>
        /// <para> the parameter <paramref name="viewName"/> is the viewName here</para> 
        /// </summary>
        /// <param name="navigationIdentity"></param>
        /// <param name="viewName"></param>
        /// <param name="navigationParameters"></param>
        /// <returns></returns>
        Task NavigateToAsync(string navigationIdentity, string viewName, NavigationParameters navigationParameters = null);

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
        public static bool? GetIsNavigateHost(DependencyObject obj)
        {
            return (bool?)obj.GetValue(IsNavigateHostProperty);
        }

        public static void SetIsNavigateHost(DependencyObject obj, bool? value)
        {
            obj.SetValue(IsNavigateHostProperty, value);
        }

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
                    navigationAwares[control] = new NavigationDepute()
                    {
                        Navigation = control,
                        Dispatcher = s.Dispatcher,
                    };
                    return;
                }

                if (!navigationAwares.TryGetValue(control, out NavigationDepute value))
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
        internal static Dictionary<INavigationControl, NavigationDepute> navigationAwares = new Dictionary<INavigationControl, NavigationDepute>();

        public INavigationDepute this[string navigationIdentity]
        {
            get
            {
                if (navigationIdentity is null)
                {
                    throw new ArgumentNullException(nameof(navigationIdentity));
                }

                NavigationDepute proxy = navigationAwares.Values.FirstOrDefault(i => string.Compare(i.Navigation?.Identity, navigationIdentity) == 0);

                return proxy;
            }
        }

        public Task NavigateBackAsync(string navigationIdentity)
        {
            if (navigationIdentity is null)
            {
                throw new ArgumentNullException(nameof(navigationIdentity));
            }

            NavigationDepute proxy = navigationAwares.Values.FirstOrDefault(i => string.Compare(i.Navigation?.Identity, navigationIdentity) == 0);

            if (proxy is null)
            {
                return Task.FromResult(false);
            }

            return proxy.NavigateBackAsync();
        }

        public Task NavigateToAsync(string navigationIdentity, string viewName, NavigationParameters navigationParameters = null)
        {
            if (navigationIdentity is null)
            {
                throw new ArgumentNullException(nameof(navigationIdentity));
            }

            NavigationDepute proxy = navigationAwares.Values.FirstOrDefault(i => string.Compare(i.Navigation?.Identity, navigationIdentity) == 0);

            if (proxy is null)
            {
                return Task.FromResult(false);
            }

            return proxy.NavigateToAsync(viewName, navigationParameters);
        }
    }
}