using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Easy.Toolkit
{
    public enum TransitionMode : byte
    {
        None = 0,
        RightToLeft = 1,
        LeftToRight = 2,
        BottomToTop = 3,
        TopToBottom = 4,
        RightToLeftWithFade = 5,
        LeftToRightWithFade = 6,
        BottomToTopWithFade = 7,
        TopToBottomWithFade = 8,
        Fade = 9,
        Random = 254,
        Custom = 255
    }

    [ContentProperty("Content")]
    [DefaultProperty("Content")]
    [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
    public class TransitioningControl : ContentControl, INavigationControl
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FrameworkElement contentPresenter;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly Random Random = new(Guid.NewGuid().GetHashCode());
        static TransitioningControl()
        {
            PropertyAssist.DefaultStyle<TransitioningControl>(DefaultStyleKeyProperty);
        }
        public TransitioningControl()
        {
            //IsVisibleChanged += (s, e) =>
            //{
            //    if (e.NewValue is bool r && r)
            //    {
            //        RunTransition();
            //    }
            //};
            Loaded += (s, e) => RunTransition();

        }


        public static readonly DependencyProperty TransitionModeProperty =
            PropertyAssist.PropertyRegister<TransitioningControl, TransitionMode>(i => i.TransitionMode, TransitionMode.Random,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Inherits,
               (s, e) => s.RunTransition());



        public TransitionMode TransitionMode
        {
            get => (TransitionMode)GetValue(TransitionModeProperty);
            set => SetValue(TransitionModeProperty, value);
        }

        public static readonly DependencyProperty TransitionStoryboardProperty = DependencyProperty.Register(
            "TransitionStoryboard", typeof(Storyboard), typeof(TransitioningControl), new PropertyMetadata(default(Storyboard)));

        public Storyboard TransitionStoryboard
        {
            get => (Storyboard)GetValue(TransitionStoryboardProperty);
            set => SetValue(TransitionStoryboardProperty, value);
        }

        public static DependencyProperty CornerRadiusProperty = PropertyAssist.PropertyRegister<TransitioningControl, CornerRadius>(i => i.CornerRadius, new CornerRadius(0));

        [Bindable(true)]
        [Category("Border")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)base.GetValue(CornerRadiusProperty);
            set => base.SetValue(CornerRadiusProperty, value);
        }

        [Bindable(true)]
        public new object Content
        {
            get => base.GetValue(ContentProperty);
            set
            {
                try
                {
                    if (value != null)
                    {
                        Visibility = Visibility.Collapsed;
                    }
                    base.SetValue(ContentProperty, value);
                }
                finally
                {
                    RunTransition();
                    Visibility = Visibility.Visible;
                }
            }
        }

        public void RunTransition()
        {
            if (!IsArrangeValid || contentPresenter == null)
            {
                return;
            }

            TransitionMode mode = TransitionMode;

            if (mode == TransitionMode.None)
            {
                return;
            }
            if (mode == TransitionMode.Custom)
            {
                TransitionStoryboard?.Begin(contentPresenter);
                return;
            }

            if (mode == TransitionMode.Random)
            {
                byte value = (byte)Random.Next(1, 10);
                Invoker.TryCast<TransitionMode>(value, out mode);
            }
            if (mode == TransitionMode.None)
            {
                return;
            }
            Storyboard storyboard = ResourceAssist.GetResource<Storyboard>($"{mode}Transition");
            storyboard?.Begin(contentPresenter);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            contentPresenter = GetTemplateChild("PATH_Container") as FrameworkElement;
        }


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
        public static DependencyProperty IdentityProperty = PropertyAssist.PropertyRegister<TransitioningControl, string>(i => i.Identity, null, (s, e) =>
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



        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Stack<object> activitedView = new Stack<object>();

        async Task INavigationControl.NavigateToAsync(object view, NavigationParameters parameters = null)
        {

            await Dispatcher.InvokeAsync(() =>
            {
                if (Content == view)
                {
                    return;
                }

                NavigationControl.ExecuteLink(Content, null, false);
                NavigationControl.ExecuteLink(view, parameters, true);


                Visibility = Visibility.Collapsed;

                if (Content != null)
                {
                    activitedView.Push(Content);
                }

                base.Content = view;


            }, DispatcherPriority.Background);

        }


        async Task INavigationControl.NavigateBackAsync()
        {
            if (activitedView.Count <= 0)
            {
                return;
            }

            await Dispatcher.InvokeAsync(() =>
            {

                object newView = activitedView.Pop();

                NavigationControl.ExecuteLink(Content, null, false);
                NavigationControl.ExecuteLink(newView, null, false);

                Visibility = Visibility.Collapsed;
                base.Content = newView;

            }, DispatcherPriority.Background);

        }
    }
}
