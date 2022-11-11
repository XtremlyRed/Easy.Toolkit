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
        Task NavigateToAsync(object view, INavigationParameters parameters = null);
    }


    public class NavigationControl : ContentControl, INavigationControl
    {
        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Stack<object> activitedView = new Stack<object>();

        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private object currentView;

        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string identity;

        string INavigationControl.Identity
        {
            get
            {
                if (identity is null)
                {
                    throw new Exception($"{GetType()} property:{nameof(INavigationControl.Identity)} value must be unique and cannot be empty or null");
                }

                return identity;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static DependencyProperty IdentityProperty = PropertyAssist.PropertyRegister<NavigationControl, string>(i => i.Identity, null, (s, e) =>
        {
            if (s is not INavigationControl control)
            {
                throw new Exception($"{s.GetType()} must inherit interface:{typeof(INavigationControl)}");
            }

            s.identity = e.NewValue;

            if (NavigationManager.navigationAwares.TryGetValue(s, out NavigationDeliver deliver) == false)
            {
                NavigationManager.navigationAwares[s] = deliver = new NavigationDeliver();
            }
            deliver.Dispatcher = s.Dispatcher;
            deliver.Navigation = s;
        });

        [Bindable(true)]
        [Category("Identity")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public string Identity
        {
            get => base.GetValue(IdentityProperty) as string;
            set => base.SetValue(IdentityProperty, value);
        }



        public async Task NavigateToAsync(object view, INavigationParameters parameters = null)
        {
            if (currentView == view)
            {
                return;
            }

            await Dispatcher.InvokeAsync(() =>
              {
                  ExecuteLink(currentView, null, false);

                  ExecuteLink(view, parameters ?? new NavigationParameters(), true);

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


        internal static void ExecuteLink(object view, INavigationParameters parameters = null, bool toNew = true)
        {
            if (view == null)
            {
                return;
            }

            if (view is not FrameworkElement framework1)
            {
                return;
            }

            if (framework1.DataContext is null)
            {
                return;
            }

            if (framework1.DataContext is INavigationViewModelAware dataContextLink)
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
