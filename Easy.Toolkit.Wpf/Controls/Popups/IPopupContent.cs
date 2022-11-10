namespace Easy.Toolkit
{
    public interface IPopupView
    {
        event EventHandler<PopupResultEventArgs> RequestClose;
    }

    public interface IMessagePopupView : IPopupView
    { 
        string Message { set; }

        bool HideCancel {   set; } 
    }

    public class PopupResultEventArgs : EventArgs
    {
        public PopupResultEventArgs(bool popupResult)
        {
            PopupResult = popupResult;
        }

        public bool PopupResult { get; }


        public static implicit operator PopupResultEventArgs(bool popupResult)
        {
            return new PopupResultEventArgs(popupResult);
        }

        public static explicit operator bool(PopupResultEventArgs popupResultEventArgs)
        {
            return popupResultEventArgs.PopupResult;
        }
    };
}
