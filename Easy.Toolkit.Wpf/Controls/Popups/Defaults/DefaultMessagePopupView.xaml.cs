using System.Windows;
using System.Windows.Controls;

namespace Easy.Toolkit.Popups
{
    /// <summary>
    /// DefaultMessagePopupView.xaml 的交互逻辑
    /// </summary>
    public partial class DefaultMessagePopupView : UserControl, IMessagePopupView
    {
        public static string OkButtonContent = "OK";
        public static string NoButtonContent = "No";
        public static string TitleContent = "Tips";
        public static Size DisplaySize = new Size(550, 340);
        public DefaultMessagePopupView()
        {
            InitializeComponent();

            NoBtn.MouseLeftButtonUp += (s, e) => RequestClose?.Invoke(this, false);
            YesBtn.MouseLeftButtonUp += (s, e) => RequestClose.Invoke(this, true);

            TitleText.Text = TitleContent;
            NoBtnText.Text = NoButtonContent;
            YesBtnText.Text = OkButtonContent;
            Width = DisplaySize.Width;
            Height = DisplaySize.Height;
        }

        public string Message { set => MessageContainer.Text = value; }
        public bool HideCancel
        {
            set => NoBtn.Visibility = value ? Visibility.Collapsed : Visibility.Visible;
        }

        public event EventHandler<PopupResultEventArgs> RequestClose;

         
    }
}
