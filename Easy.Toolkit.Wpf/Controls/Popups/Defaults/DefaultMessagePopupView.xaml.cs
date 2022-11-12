using System.Windows;
using System.Windows.Controls;

namespace Easy.Toolkit.Popups
{
    /// <summary>
    /// DefaultMessagePopupView.xaml 
    /// </summary>
    public partial class DefaultMessagePopupView : UserControl, IMessagePopupView
    {
        /// <summary>
        /// OkButtonContent
        /// </summary>
        public static string OkButtonContent = "OK";
        /// <summary>
        /// NoButtonContent
        /// </summary>
        public static string NoButtonContent = "No";
        /// <summary>
        /// TitleContent
        /// </summary>
        public static string TitleContent = "Tips";

        /// <summary>
        /// DisplaySize
        /// </summary>
        public static Size DisplaySize = new Size(550, 340);

        /// <summary>
        /// DefaultMessagePopupView
        /// </summary>
        public DefaultMessagePopupView()
        {
            InitializeComponent();

            NoBtn.MouseLeftButtonUp += (s, e) => RequestClosePopup?.Invoke(this, false);
            YesBtn.MouseLeftButtonUp += (s, e) => RequestClosePopup.Invoke(this, true);

            TitleText.Text = TitleContent;
            NoBtnText.Text = NoButtonContent;
            YesBtnText.Text = OkButtonContent;
            Width = DisplaySize.Width;
            Height = DisplaySize.Height;
        }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { set => MessageContainer.Text = value; }

        /// <summary>
        /// HideCancel
        /// </summary>
        public bool HideCancel
        {
            set => NoBtn.Visibility = value ? Visibility.Collapsed : Visibility.Visible;
        }

        /// <summary>
        /// RequestClosePopup
        /// </summary>
        public event EventHandler<PopupResultEventArgs> RequestClosePopup;

         
    }
}
