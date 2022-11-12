using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Easy.Toolkit
{
    /// <summary>
    /// popup manager
    /// </summary>
    public interface IPopupManager
    {
        /// <summary>
        /// get <see cref="IPopupDeliver"/> from <paramref name="popupIdentity"/>
        /// </summary>
        /// <param name="popupIdentity"></param>
        /// <returns></returns>
        IPopupDeliver this[string popupIdentity] { get; }

        /// <summary>
        /// popup message in popup control with identity equas <paramref name="popupIdentity"/>
        /// </summary>
        /// <param name="popupIdentity"></param>
        /// <param name="showMessage"></param>
        /// <returns></returns>
        Task ShowAsync(string popupIdentity, string showMessage);

        /// <summary>
        /// popup confirm message in popup control with identity equas <paramref name="popupIdentity"/>
        /// </summary>
        /// <param name="popupIdentity"></param>
        /// <param name="confirmMessage"></param>
        /// <returns></returns>
        Task<bool> ConfirmAsync(string popupIdentity, string confirmMessage);


        /// <summary>
        /// <para> <c>explain:</c>></para> 
        /// <para> use the method <see cref="ViewRegisterExtensions.RegisterView{TView}(IContainerRegistry, string, bool, bool)"/> to register view. </para>
        /// <para> the parameter <paramref name="viewName"/> is the viewName here</para> 
        /// <para> the view must implement the <see cref="IPopupView"/></para>
        /// <para> the view's data content must implement the <see cref="IPopupViewModelAware"/></para>
        /// </summary> 
        /// <param name="popupIdentity"></param>
        /// <param name="viewName"></param>
        /// <param name="popupParameters"></param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<object> PopupAsync(string popupIdentity, string viewName, IPopupParameters popupParameters=null);
    }



    /// <summary>
    /// <see cref="PopupManager"/>
    /// </summary>
    public class PopupManager : IPopupManager
    {
        /// <summary>
        /// get whether an element ( must be implementation <see cref="IPopupControl"/> ) is set to popup host
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool? GetIsPopupHost(DependencyObject obj)
        {
            return (bool?)obj.GetValue(IsPopupHostProperty);
        }

        /// <summary>
        /// set ui element as popup host ,but the ui element must be implementation <see cref="IPopupControl"/>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetIsPopupHost(DependencyObject obj, bool? value)
        {
            obj.SetValue(IsPopupHostProperty, value);
        }

        /// <summary>
        /// is popup host
        /// </summary>
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
        internal static IDictionary<IPopupControl, PopupDeliver> popupAwares = new Dictionary<IPopupControl, PopupDeliver>();

        /// <summary>
        /// get <see cref="IPopupDeliver"/> from <paramref name="popupIdentity"/>
        /// </summary>
        /// <param name="popupIdentity"></param>
        /// <returns></returns>
        public IPopupDeliver this[string popupIdentity]
        {
            get
            {
                if (popupIdentity is null)
                {
                    throw new ArgumentNullException(nameof(popupIdentity));
                }

                var proxy = popupAwares.Values.FirstOrDefault(i => string.Compare(i.Popup.Identity, popupIdentity) == 0);

                return proxy;
            }
        }
        /// <summary>
        /// popup confirm message in popup control with identity equas <paramref name="popupIdentity"/>
        /// </summary>
        /// <param name="popupIdentity"></param>
        /// <param name="confirmMessage"></param>
        /// <returns></returns>
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

        /// <summary>
        /// <para> <c>explain:</c>></para> 
        /// <para> use the method <see cref="ViewRegisterExtensions.RegisterView{TView}(IContainerRegistry, string, bool, bool)"/> to register view. </para>
        /// <para> the parameter <paramref name="viewName"/> is the viewName here</para> 
        /// <para> the view must implement the <see cref="IPopupView"/></para>
        /// <para> the view's data content must implement the <see cref="IPopupViewModelAware"/></para>
        /// </summary> 
        /// <param name="popupIdentity"></param>
        /// <param name="viewName"></param>
        /// <param name="popupParameters"></param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public Task<object> PopupAsync (string popupIdentity, string viewName, IPopupParameters popupParameters = null) 
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

            return proxy.PopupAsync(viewName, popupParameters);
        }


        /// <summary>
        /// popup message in popup control with identity equas <paramref name="popupIdentity"/>
        /// </summary>
        /// <param name="popupIdentity"></param>
        /// <param name="showMessage"></param>
        /// <returns></returns>
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
