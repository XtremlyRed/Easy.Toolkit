using System.Windows;

namespace Easy.Toolkit
{
    /// <summary>
    /// dialog window interface
    /// </summary>
    public interface IDialogWindow
    {
        /// <summary>
        /// The window's size to content.
        /// </summary>    
        SizeToContent SizeToContent { get; set; }
        /// <summary>
        /// The window's startup location.
        /// </summary>    
        WindowStartupLocation WindowStartupLocation { get; set; }
        /// <summary>
        /// Dialog content.
        /// </summary>        
        object Content { get; set; }
        /// <summary>
        /// The window's owner.
        /// </summary>        
        Window Owner { get; set; }
        /// <summary>
        /// The data context must implement Prism.Services.Dialogs.IDialogAware.
        /// </summary>        
        object DataContext { get; set; }
        /// <summary>
        /// The result of the dialog.
        /// </summary>        
        DialogResult Result { get; set; }
        /// <summary>
        /// The window style.
        /// </summary>        
        Style Style { get; set; }
        /// <summary>
        /// Called when the window is loaded.
        /// </summary>        
        event RoutedEventHandler Loaded;
        /// <summary>
        /// Called when the window is closed.
        /// </summary>        
        event EventHandler Closed;
        /// <summary>
        /// Called when the window is closing.
        /// </summary>        
        event CancelEventHandler Closing;

        /// <summary>
        /// Close the window.
        /// </summary>        
        void Close();
        /// <summary>
        /// Show a non-modal dialog.
        /// </summary>
        void Show();
        /// <summary>
        /// Show a modal dialog.
        /// </summary>
        /// <returns></returns>
        bool? ShowDialog();
    }
}
