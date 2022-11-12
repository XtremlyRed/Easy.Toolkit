using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace Easy.Toolkit
{
    /// <summary>
    /// interface of <see cref="IPopupControl"/>
    /// </summary>
    public interface IPopupControl
    {
        /// <summary>
        /// will mark a popup host control as a unique key
        /// </summary>
        string Identity { get; }

        /// <summary>
        /// show message
        /// </summary>
        /// <param name="showMessage"></param>
        /// <returns></returns>
        Task ShowAsync(string showMessage);

        /// <summary>
        /// show confirm message
        /// </summary>
        /// <param name="confirmMessage"></param>
        /// <returns></returns>
        Task<bool> ConfirmAsync(string confirmMessage);

        /// <summary>
        /// popup view control
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <param name="viewCreator">view creator</param>
        /// <returns></returns>
        Task<object> PopupAsync<TView>(Func<TView> viewCreator) where TView : IPopupView;

    }

    /// <summary>
    /// popup control
    /// </summary>
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
        /// will mark a popup host control as a unique key
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


        /// <summary>
        /// will mark a popup host control as a unique key
        /// </summary>
        [Bindable(true)]
        [Category("Identity")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]

        public string Identity
        {
            get => base.GetValue(IdentityProperty) as string;
            set => base.SetValue(IdentityProperty, value);
        }

        /// <summary>
        /// MaskBrushProperty
        /// </summary>
        public static DependencyProperty MaskBrushProperty = PropertyAssist.PropertyRegister<PopupControl, Brush>(i => i.MaskBrush, Brushes.Transparent);


        /// <summary>
        /// MaskBrush
        /// </summary>
        [Bindable(true)]
        [Category("Brush")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public Brush MaskBrush
        {
            get => base.GetValue(MaskBrushProperty) as Brush;
            set => base.SetValue(MaskBrushProperty, value);
        }

        /// <summary>
        /// CornerRadiusProperty
        /// </summary>
        public static DependencyProperty CornerRadiusProperty = PropertyAssist.PropertyRegister<PopupControl, CornerRadius>(i => i.CornerRadius, new CornerRadius(0));

        /// <summary>
        /// CornerRadius
        /// </summary>
        [Bindable(true)]
        [Category("Brush")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)base.GetValue(CornerRadiusProperty);
            set => base.SetValue(CornerRadiusProperty, value);
        }

        /// <summary>
        /// HorizontalPopupAlignmentProperty
        /// </summary>
        public static DependencyProperty HorizontalPopupAlignmentProperty = PropertyAssist.PropertyRegister<PopupControl, HorizontalAlignment>(i => i.HorizontalPopupAlignment, HorizontalAlignment.Center);

        /// <summary>
        /// HorizontalPopupAlignment
        /// </summary>
        [Bindable(true)]
        [Category("PopupAlignment")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public HorizontalAlignment HorizontalPopupAlignment
        {
            get => (HorizontalAlignment)base.GetValue(HorizontalPopupAlignmentProperty);
            set => base.SetValue(HorizontalPopupAlignmentProperty, value);
        }

        /// <summary>
        /// VerticalPopupAlignmentProperty
        /// </summary>
        public static DependencyProperty VerticalPopupAlignmentProperty = PropertyAssist.PropertyRegister<PopupControl, VerticalAlignment>(i => i.VerticalPopupAlignment, VerticalAlignment.Center);


        /// <summary>
        /// VerticalPopupAlignment
        /// </summary>
        [Bindable(true)]
        [Category("PopupAlignment")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public VerticalAlignment VerticalPopupAlignment
        {
            get => (VerticalAlignment)base.GetValue(VerticalPopupAlignmentProperty);
            set => base.SetValue(VerticalPopupAlignmentProperty, value);
        }


        /// <summary>
        /// PopupContentProperty
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal static DependencyProperty PopupContentProperty = PropertyAssist.PropertyRegister<PopupControl, IPopupView>(i => i.PopupContent, null);


        /// <summary>
        /// PopupContent
        /// </summary>
        [Bindable(true)]
        [Category("PopupContent")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal IPopupView PopupContent
        {
            get => base.GetValue(PopupContentProperty) as IPopupView;
            set => base.SetValue(PopupContentProperty, value);
        }

        /// <summary>
        /// MessagePopupViewProperty
        /// </summary>
        public static DependencyProperty MessagePopupViewProperty = PropertyAssist.PropertyRegister<PopupControl, IMessagePopupView>(i => i.MessagePopupView, null);

        /// <summary>
        /// MessagePopupView
        /// </summary>
        [Bindable(true)]
        [Category("MessagePopupView")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public IMessagePopupView MessagePopupView
        {
            get => base.GetValue(MessagePopupViewProperty) as IMessagePopupView;
            set => base.SetValue(MessagePopupViewProperty, value);
        }

        /// <summary>
        /// <see cref="OnApplyTemplate"/>
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            popupPanel = GetTemplateChild("PopupPanel") as Grid;
            showPanel = GetTemplateChild("ShowPanel") as Grid;
        }




    }
}
