using System.Windows;
using System.Windows.Input;

namespace Easy.Toolkit.Dialogs.Defaults
{
    /// <summary>
    /// DefaultDialogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DefaultDialogWindow : Window, IDialogWindow
    {
        public DefaultDialogWindow()
        {
            InitializeComponent();
            ExitWindow.MouseDown += (s, e) => Close();

            HeadContainer.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    DragMove();
                }
            };
        }

        public DialogResult Result { get; set; }

        object IDialogWindow.Content
        {
            get => ContentContainer.Content;
            set => ContentContainer.Content = value;
        }
    }
}
