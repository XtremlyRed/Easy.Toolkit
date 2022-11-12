using System.Windows;
using System.Windows.Input;

namespace Easy.Toolkit.Dialogs.Defaults
{
    /// <summary>
    /// DefaultDialogWindow.xaml  
    /// </summary>
    public partial class DefaultDialogWindow : Window, IDialogWindow
    {
        /// <summary>
        /// DefaultDialogWindow
        /// </summary>
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

        /// <summary>
        /// dialog result
        /// </summary>
        public DialogResult Result { get; set; }

        object IDialogWindow.Content
        {
            get => ContentContainer.Content;
            set => ContentContainer.Content = value;
        }
    }
}
