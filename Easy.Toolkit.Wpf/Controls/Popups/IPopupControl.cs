using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace Easy.Toolkit
{
    public interface IPopupControl
    {
        string Identity { get; }

        Task ShowAsync(string showMessage);

        Task<bool> ConfirmAsync(string confirmMessage);

        Task<object> PopupAsync<TView>(Func<TView> viewCreator) where TView : IPopupControl;

    }


    [ContentProperty("Content")]
    [DefaultProperty("Content")]
    [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
    public sealed partial class PopupControl : ContentControl
    {

        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Grid popupPanel;
        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Grid showPanel;

        /// <summary>
        /// 
        /// </summary>
        public static DependencyProperty IdentityProperty = PropertyAssist.PropertyRegister<PopupControl, string>(i => i.Identity, null, (s, e) =>
        {
            if (s is not IPopupControl control)
            {
                throw new Exception($"{s.GetType()} must inherit interface:{typeof(IPopupControl)}");
            }

            s.identity = e.NewValue;
             
            if (PopupManager.popupAwares.TryGetValue(s, out PopupDeliver deliver) == false)
            {
                PopupManager.popupAwares[s] = deliver = new PopupDeliver();
            }
            deliver.Dispatcher = s.Dispatcher;
            deliver.Popup = s;
        });


        [Bindable(true)]
        [Category("Identity")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public string Identity
        {
            get => base.GetValue(IdentityProperty) as string;
            set => base.SetValue(IdentityProperty, value);
        }


        public static DependencyProperty MaskBrushProperty = PropertyAssist.PropertyRegister<PopupControl, Brush>(i => i.MaskBrush, Brushes.Transparent);


        [Bindable(true)]
        [Category("Brush")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public Brush MaskBrush
        {
            get => base.GetValue(MaskBrushProperty) as Brush;
            set => base.SetValue(MaskBrushProperty, value);
        }


        public static DependencyProperty CornerRadiusProperty = PropertyAssist.PropertyRegister<PopupControl, CornerRadius>(i => i.CornerRadius, new CornerRadius(0));

        [Bindable(true)]
        [Category("Brush")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)base.GetValue(CornerRadiusProperty);
            set => base.SetValue(CornerRadiusProperty, value);
        }


        public static DependencyProperty HorizontalPopupAlignmentProperty = PropertyAssist.PropertyRegister<PopupControl, HorizontalAlignment>(i => i.HorizontalPopupAlignment, HorizontalAlignment.Center);

        [Bindable(true)]
        [Category("PopupAlignment")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public HorizontalAlignment HorizontalPopupAlignment
        {
            get => (HorizontalAlignment)base.GetValue(HorizontalPopupAlignmentProperty);
            set => base.SetValue(HorizontalPopupAlignmentProperty, value);
        }

        public static DependencyProperty VerticalPopupAlignmentProperty = PropertyAssist.PropertyRegister<PopupControl, VerticalAlignment>(i => i.VerticalPopupAlignment, VerticalAlignment.Center);

        [Bindable(true)]
        [Category("PopupAlignment")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public VerticalAlignment VerticalPopupAlignment
        {
            get => (VerticalAlignment)base.GetValue(VerticalPopupAlignmentProperty);
            set => base.SetValue(VerticalPopupAlignmentProperty, value);
        }


        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal static DependencyProperty PopupContentProperty = PropertyAssist.PropertyRegister<PopupControl, IPopupView>(i => i.PopupContent, null);

        [Bindable(true)]
        [Category("PopupContent")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal IPopupView PopupContent
        {
            get => base.GetValue(PopupContentProperty) as IPopupView;
            set => base.SetValue(PopupContentProperty, value);
        }

        public static DependencyProperty MessagePopupViewProperty = PropertyAssist.PropertyRegister<PopupControl, IMessagePopupView>(i => i.MessagePopupView, null);

        [Bindable(true)]
        [Category("MessagePopupView")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public IMessagePopupView MessagePopupView
        {
            get => base.GetValue(MessagePopupViewProperty) as IMessagePopupView;
            set => base.SetValue(MessagePopupViewProperty, value);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            popupPanel = GetTemplateChild("PopupPanel") as Grid;
            showPanel = GetTemplateChild("ShowPanel") as Grid;
        }




    }
}
