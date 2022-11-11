using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Windows;

namespace Easy.Toolkit
{
    public interface IPopupManager
    {
        IPopupDeliver this[string identity] { get; }

        Task<bool> ConfirmAsync(string popupIdentity, string confirmMessage);

        Task<object> PopupAsync<TView>(string popupIdentity, Func<TView> viewCreator) where TView : IPopupView;
        Task ShowAsync(string popupIdentity, string showMessage);
    }


    public class PopupManager : IPopupManager
    {

        public static bool? GetIsPopupHost(DependencyObject obj)
        {
            return (bool?)obj.GetValue(IsPopupHostProperty);
        }

        public static void SetIsPopupHost(DependencyObject obj, bool? value)
        {
            obj.SetValue(IsPopupHostProperty, value);
        }

        public static readonly DependencyProperty IsPopupHostProperty =
            DependencyProperty.RegisterAttached("IsPopupHost", typeof(bool?),
            typeof(PopupManager),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (s, e) =>
            {
                if (s is not IPopupControl control)
                {
                    throw new Exception($"{s.GetType()} must inherit interface:{typeof(IPopupControl)}");
                }
                 
                if (e.NewValue is true)
                {
                    if (popupAwares.ContainsKey(control))
                    {
                        return;
                    }
                    popupAwares[control] = new PopupDeliver()
                    {
                        Popup = control,
                        Dispatcher = s.Dispatcher,
                    };
                    return;
                }

                if (!popupAwares.TryGetValue(control, out PopupDeliver value))
                {
                    return;
                }

                popupAwares.Remove(control);

                if (value == null)
                {
                    return;
                }

                value.Popup = null;
                value.Dispatcher = null;
                value = null;

            }));




        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)] 
        internal static IDictionary<IPopupControl, PopupDeliver> popupAwares = new  Dictionary<IPopupControl, PopupDeliver>();

        public IPopupDeliver this[string identity]
        {
            get
            {
                if (identity is null)
                {
                    throw new ArgumentNullException(nameof(identity));
                }

                var proxy = popupAwares.Values.FirstOrDefault(i => string.Compare(i.Popup.Identity, identity) == 0);

                return proxy;
            }
        }

        public Task<bool> ConfirmAsync(string popupIdentity, string confirmMessage)
        {
            if (popupIdentity is null)
            {
                throw new ArgumentNullException(nameof(popupIdentity));
            }

            var proxy = popupAwares.Values.FirstOrDefault(i => string.Compare(i.Popup?.Identity, popupIdentity) == 0);

            if (proxy is null)
            {
                return Task.FromResult(false);
            }

            return proxy.ConfirmAsync(confirmMessage);
        }

        public Task<object> PopupAsync<TView>(string popupIdentity, Func<TView> viewCreator) where TView : IPopupView
        {
            if (popupIdentity is null)
            {
                throw new ArgumentNullException(nameof(popupIdentity));
            }

            var proxy = popupAwares.Values.FirstOrDefault(i => string.Compare(i.Popup?.Identity, popupIdentity) == 0);

            if (proxy is null)
            {
                return Task.FromResult((object)false);
            }

            return proxy.PopupAsync(viewCreator);
        }

        public Task ShowAsync(string popupIdentity, string showMessage)
        {
            if (popupIdentity is null)
            {
                throw new ArgumentNullException(nameof(popupIdentity));
            }

            var proxy = popupAwares.Values.FirstOrDefault(i => string.Compare(i.Popup?.Identity, popupIdentity) == 0);

            if (proxy is null)
            {
                return Task.FromResult(false);
            }


            return proxy.ShowAsync(showMessage);
        }
    }
}
