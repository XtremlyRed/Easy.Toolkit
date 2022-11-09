using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
namespace Easy.Toolkit
{
    public interface INavigationControl
    {
        string Identity { get; }
        Task NavigateBackAsync();
        Task NavigateToAsync(object view, NavigationParameters parameters = null);
    }


    public class NavigationControl : ContentControl, INavigationControl
    {
        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Stack<object> activitedView = new Stack<object>();

        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private object currentView;

        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string identity;
        string INavigationControl.Identity => identity;
        /// <summary>
        /// 
        /// </summary>
        public static DependencyProperty IdentityProperty = PropertyAssist.PropertyRegister<NavigationControl, string>(i => i.Identity, null, (s, e) =>
        {
            s.identity = e.NewValue;

            if (NavigationManager.navigationAwares.TryGetValue(s, out NavigationDepute proxy) == false)
            {
                NavigationManager.navigationAwares[s] = proxy = new NavigationDepute();
            }
            proxy.Dispatcher = s.Dispatcher;
            proxy.Navigation = s;
        });

        [Bindable(true)]
        [Category("Identity")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public string Identity
        {
            get => base.GetValue(IdentityProperty) as string;
            set => base.SetValue(IdentityProperty, value);
        }



        public async Task NavigateToAsync(object view, NavigationParameters parameters = null)
        {
            if (currentView == view)
            {
                return;
            }

            await Dispatcher.InvokeAsync(() =>
              {
                  ExecuteLink(currentView, null, false);

                  ExecuteLink(view, parameters, true);

                  if (currentView != null)
                  {
                      activitedView.Push(currentView);
                  }

                  base.Content = currentView = view;

              }, DispatcherPriority.Background, CancellationToken.None);

        }


        public async Task NavigateBackAsync()
        {
            if (activitedView.Count <= 0)
            {
                return;
            }

            await Dispatcher.InvokeAsync(() =>
             {
                 object newView = activitedView.Pop();

                 ExecuteLink(currentView, null, false);

                 ExecuteLink(newView, null, false);

                 base.Content = currentView = newView;

             }, DispatcherPriority.Background, CancellationToken.None);

        }


        private void ExecuteLink(object view, NavigationParameters parameters = null, bool toNew = true)
        {
            if (view == null)
            {
                return;
            }

            if (view is INavigationViewAware viewLink)
            {
                if (toNew)
                {
                    viewLink.NavigateTo(parameters);
                }
                else
                {
                    viewLink.NavigateFrom();
                }
            }

            if (view is not FrameworkElement framework1)
            {
                return;
            }

            if (framework1.DataContext is INavigationViewAware dataContextLink)
            {
                if (toNew)
                {
                    dataContextLink.NavigateTo(parameters);
                }
                else
                {
                    dataContextLink.NavigateFrom();
                }

            }
        }

    }

}
