using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Easy.Toolkit
{

    /// <summary>
    /// header panel
    /// </summary>
    public class HeaderControl : ContentControl
    {

        static HeaderControl()
        {
            PropertyAssist.DefaultStyle<HeaderControl>(DefaultStyleKeyProperty);
        }

        /// <summary>
        /// create a new instance of  <see cref="HeaderControl"/>
        /// </summary>
        public HeaderControl()
        {

        }

        /// <summary>
        /// Header
        /// </summary>
        public static DependencyProperty HeaderProperty = PropertyAssist.PropertyRegister<HeaderControl, object>(i => i.Header, null);

        /// <summary>
        /// Header
        /// </summary>
        [Bindable(true)]
        [Category("Header")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public object Header
        {
            get => base.GetValue(HeaderProperty);
            set => base.SetValue(HeaderProperty, value);
        }


        /// <summary>
        /// HorizontalHeaderAlignmentProperty
        /// </summary>
        public static DependencyProperty HorizontalHeaderAlignmentProperty = PropertyAssist.PropertyRegister<HeaderControl, HorizontalAlignment>(i => i.HorizontalHeaderAlignment, HorizontalAlignment.Stretch);

        /// <summary>
        /// HorizontalHeaderAlignment
        /// </summary>
        [Bindable(true)]
        [Category("Header")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public HorizontalAlignment HorizontalHeaderAlignment
        {
            get => (HorizontalAlignment)base.GetValue(HorizontalHeaderAlignmentProperty);
            set => base.SetValue(HorizontalHeaderAlignmentProperty, value);
        }

        /// <summary>
        /// VerticalHeaderAlignmentProperty
        /// </summary>
        public static DependencyProperty VerticalHeaderAlignmentProperty = PropertyAssist.PropertyRegister<HeaderControl, VerticalAlignment>(i => i.VerticalHeaderAlignment, VerticalAlignment.Center);

        /// <summary>
        /// VerticalHeaderAlignment
        /// </summary>
        [Bindable(true)]
        [Category("Header")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public VerticalAlignment VerticalHeaderAlignment
        {
            get => (VerticalAlignment)base.GetValue(VerticalHeaderAlignmentProperty);
            set => base.SetValue(VerticalHeaderAlignmentProperty, value);
        }

        /// <summary>
        /// HeaderMarginProperty
        /// </summary>
        public static DependencyProperty HeaderMarginProperty = PropertyAssist.PropertyRegister<HeaderControl, Thickness>(i => i.HeaderMargin, new Thickness(5, 0, 5, 0));

        /// <summary>
        /// HeaderMargin
        /// </summary>
        [Bindable(true)]
        [Category("Header")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public Thickness HeaderMargin
        {
            get => (Thickness)base.GetValue(HeaderMarginProperty);
            set => base.SetValue(HeaderMarginProperty, value);
        }


        /// <summary>
        /// HeaderWidthProperty
        /// </summary>
        public static DependencyProperty HeaderWidthProperty = PropertyAssist.PropertyRegister<HeaderControl, double>(i => i.HeaderWidth, double.NaN);


        /// <summary>
        /// HeaderWidth
        /// </summary>
        [Bindable(true)]
        [Category("Header")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        [TypeConverter(typeof(LengthConverter))]
        public double HeaderWidth
        {
            get => (double)base.GetValue(HeaderWidthProperty);
            set => base.SetValue(HeaderWidthProperty, value);
        }

        /// <summary>
        /// HeaderHeightProperty
        /// </summary>
        public static DependencyProperty HeaderHeightProperty = PropertyAssist.PropertyRegister<HeaderControl, double>(i => i.HeaderHeight, double.NaN);

        /// <summary>
        /// HeaderHeight
        /// </summary>
        [Bindable(true)]
        [Category("Header")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        [TypeConverter(typeof(LengthConverter))]
        public double HeaderHeight
        {
            get => (double)base.GetValue(HeaderHeightProperty);
            set => base.SetValue(HeaderHeightProperty, value);
        }

        /// <summary>
        /// BackgroundOpacityProperty
        /// </summary>
        public static DependencyProperty BackgroundOpacityProperty = PropertyAssist.PropertyRegister<HeaderControl, double>(i => i.BackgroundOpacity, 1d);

        /// <summary>
        /// BackgroundOpacity
        /// </summary>
        [Bindable(true)]
        [Category("Background")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public double BackgroundOpacity
        {
            get => (double)base.GetValue(BackgroundOpacityProperty);
            set => base.SetValue(BackgroundOpacityProperty, value);
        }


        /// <summary>
        /// ContentMarginProperty
        /// </summary>
        public static DependencyProperty ContentMarginProperty = PropertyAssist.PropertyRegister<HeaderControl, Thickness>(i => i.ContentMargin, new Thickness(0));

        /// <summary>
        /// ContentMargin
        /// </summary>
        [Bindable(true)]
        [Category("Content")]
        [Localizability(LocalizationCategory.None, Readability = Readability.Unreadable)]
        public Thickness ContentMargin
        {
            get => (Thickness)base.GetValue(ContentMarginProperty);
            set => base.SetValue(ContentMarginProperty, value);
        }


        /// <summary>
        /// CornerRadiusProperty
        /// </summary>
        public static DependencyProperty CornerRadiusProperty = PropertyAssist.PropertyRegister<HeaderControl, CornerRadius>(i => i.CornerRadius, new CornerRadius(0));

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
        /// OnApplyTemplate
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //if (GetTemplateChild("Path_Click_Container") is Button clickBtn)
            //{
            //    clickBtn.Click += (s, e) =>
            //    {
            //        RoutedEventArgs routedEventArgs = new RoutedEventArgs(ClickEvent, this);
            //        RaiseEvent(routedEventArgs);
            //    };
            //}
        }
    }
}
