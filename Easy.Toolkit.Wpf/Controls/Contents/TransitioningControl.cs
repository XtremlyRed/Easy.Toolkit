using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Easy.Toolkit
{
    /// <summary>
    /// TransitionMode
    /// </summary>
    public enum TransitionMode : byte
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// RightToLeft
        /// </summary>
        RightToLeft = 1,
        /// <summary>
        /// LeftToRight
        /// </summary>
        LeftToRight = 2,
        /// <summary>
        /// BottomToTop
        /// </summary>
        BottomToTop = 3,
        /// <summary>
        /// TopToBottom
        /// </summary>
        TopToBottom = 4,
        /// <summary>
        /// RightToLeftWithFade
        /// </summary>
        RightToLeftWithFade = 5,
        /// <summary>
        /// LeftToRightWithFade
        /// </summary>
        LeftToRightWithFade = 6,
        /// <summary>
        /// BottomToTopWithFade
        /// </summary>
        BottomToTopWithFade = 7,
        /// <summary>
        /// TopToBottomWithFade
        /// </summary>
        TopToBottomWithFade = 8,
        /// <summary>
        /// Fade
        /// </summary>
        Fade = 9,
        /// <summary>
        /// Random
        /// </summary>
        Random = 254,
        /// <summary>
        /// Custom
        /// </summary>
        Custom = 255
    }

    /// <summary>
    /// TransitioningControl
    /// </summary>
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
        /// <summary>
        /// TransitioningControl
        /// </summary>
        public TransitioningControl()
        {
            Loaded += (s, e) => RunTransition();

        }

        /// <summary>
        /// TransitionModeProperty
        /// </summary>
        public static readonly DependencyProperty TransitionModeProperty =
            PropertyAssist.PropertyRegister<TransitioningControl, TransitionMode>(i => i.TransitionMode, TransitionMode.Random,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Inherits,
               (s, e) => s.RunTransition());


        /// <summary>
        /// TransitionMode
        /// </summary>
        public TransitionMode TransitionMode
        {
            get => (TransitionMode)GetValue(TransitionModeProperty);
            set => SetValue(TransitionModeProperty, value);
        }

        /// <summary>
        /// TransitionStoryboardProperty
        /// </summary>
        public static readonly DependencyProperty TransitionStoryboardProperty = DependencyProperty.Register(
            "TransitionStoryboard", typeof(Storyboard), typeof(TransitioningControl), new PropertyMetadata(default(Storyboard)));

        /// <summary>
        /// TransitionStoryboard
        /// </summary>
        public Storyboard TransitionStoryboard
        {
            get => (Storyboard)GetValue(TransitionStoryboardProperty);
            set => SetValue(TransitionStoryboardProperty, value);
        }

        /// <summary>
        /// CornerRadiusProperty
        /// </summary>
        public static DependencyProperty CornerRadiusProperty = PropertyAssist.PropertyRegister<TransitioningControl, CornerRadius>(i => i.CornerRadius, new CornerRadius(0));

        /// <summary>
        /// CornerRadius
        /// </summary>
        [Bindable(true)]
        [Category("Border")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)base.GetValue(CornerRadiusProperty);
            set => base.SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// Content
        /// </summary>
        [Bindable(true)]
        public new object Content
        {
            get => base.GetValue(ContentProperty);
            set
            {
                if (value is null)
                {
                    base.SetValue(ContentProperty, value);
                    return;
                } 
                base.SetValue(ContentProperty, value);
                RunTransition(); 
            }
        }

        private void RunTransition()
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
                mode = (TransitionMode)value;
            }
            if (mode == TransitionMode.None)
            {
                return;
            }

            if (storyboardMapper.TryGetValue(mode, out Storyboard storyboard) == false)
            {
                storyboardMapper[mode] = storyboard = ResourceAssist.GetResource<Storyboard>($"{mode}Transition");
            }

            storyboard?.Begin(contentPresenter);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        private readonly Dictionary<TransitionMode, Storyboard> storyboardMapper = new Dictionary<TransitionMode, Storyboard>();


        /// <summary>
        /// <seealso cref="OnApplyTemplate"/>
        /// </summary>
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
        /// IdentityProperty
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

        /// <summary>
        /// Identity
        /// </summary>
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

        async Task INavigationControl.NavigateToAsync(object view, INavigationParameters parameters = null)
        {

            await Dispatcher.InvokeAsync(() =>
            {
                if (Content == view)
                {

                    return;
                }

                NavigationControl.ExecuteLink(Content, null, false);
                NavigationControl.ExecuteLink(view, parameters, true);

                if (Content != null)
                {
                    activitedView.Push(Content);
                }

                Content = view;

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

                Content = newView;

            }, DispatcherPriority.Background);

        }
    }
}
