using System.Threading.Tasks;
using System.Windows.Threading;

namespace Easy.Toolkit 
{
    public interface IPopupDeliver
    {
        string Identity { get; }

        Task ShowAsync(string showMessage);

        Task<bool> ConfirmAsync(string confirmMessage);

        Task<object> PopupAsync<TView>(Func<TView> viewCreator) where TView : IPopupView;

    }

    public sealed class PopupDeliver : IPopupDeliver
    {
        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal IPopupControl Popup;
        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal Dispatcher Dispatcher;

        public string Identity => Popup.Identity;

        public Task<bool> ConfirmAsync(string confirmMessage)
        {
            return Popup.ConfirmAsync(confirmMessage);
        }

        public Task<object> PopupAsync<TView>(Func<TView> viewCreator) where TView : IPopupView
        {
            return Popup.PopupAsync(viewCreator);
        }

        public Task ShowAsync(string showMessage)
        {
            return Popup.ShowAsync(showMessage);
        }
    }

}
