using System.Windows.Forms;

namespace Easy.Toolkit
{
    /// <summary>
    /// dialog view model aware
    /// </summary>
    public interface IDialogViewModelAware
    {
        /// <summary>
        /// if the dialog can be closed.
        /// </summary>
        /// <returns>If <c>true</c> the dialog can be closed. If <c>false</c> the dialog will not close.</returns>
        bool CanClose { get; }

        /// <summary>
        /// called when the dialog is closed.
        /// </summary>
        void OnDialogClosed();

        /// <summary>
        /// called when the dialog is opened.
        /// </summary>
        /// <param name="parameters">The parameters passed to the dialog.</param>
        void OnDialogOpened(IDialogParameters parameters);

        /// <summary>
        /// The title of the dialog that will show in the window title.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// the event <see cref="RequestClose"/> to close the dialog.
        /// </summary>
        event Action<DialogResult> RequestClose;
    }
}
