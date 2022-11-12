using System.Windows;

namespace Easy.Toolkit
{
    /// <summary>
    /// <see cref="IPopupView"/>
    /// </summary>
    public interface IPopupView
    {
        /// <summary>
        /// data context
        /// </summary>
        object DataContext { get; }

        /// <summary>
        /// called when view request close
        /// </summary>
        event EventHandler<PopupResultEventArgs> RequestClosePopup;

        /// <summary>
        /// called when view loaded
        /// </summary>
        event RoutedEventHandler Loaded;
    }

    /// <summary>
    /// message view 
    /// </summary>
    public interface IMessagePopupView : IPopupView
    {

        /// <summary>
        /// display message
        /// </summary>
        string Message { set; }

        /// <summary>
        /// hide cancel button
        /// </summary>
        bool HideCancel { set; }
    }

    /// <summary>
    /// popup result
    /// </summary>
    public class PopupResultEventArgs : EventArgs
    {
        /// <summary>
        /// create a new popup result event args
        /// </summary>
        /// <param name="popupResult"></param>
        public PopupResultEventArgs(bool popupResult)
        {
            PopupResult = popupResult;
        }

        /// <summary>
        /// popup result
        /// </summary>
        public bool PopupResult { get; }

        /// <summary>
        /// create a new popup result event args from <paramref name="popupResult"/>
        /// </summary>
        /// <param name="popupResult"></param>
        public static implicit operator PopupResultEventArgs(bool popupResult)
        {
            return new PopupResultEventArgs(popupResult);
        }

        /// <summary>
        /// get <see cref="bool"/> result from <paramref name="popupResultEventArgs"/>
        /// </summary>
        /// <param name="popupResultEventArgs"></param>
        public static explicit operator bool(PopupResultEventArgs popupResultEventArgs)
        {
            return popupResultEventArgs.PopupResult;
        }
    }
}
