using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using Easy.Toolkit.Popups;

namespace Easy.Toolkit
{
    public partial class PopupControl : IPopupControl
    {
        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string identity;

        string IPopupControl.Identity
        {
            get
            {
                if (identity is null)
                {
                    throw new Exception($"{GetType()} property:{nameof(INavigationControl.Identity)} value must be unique and cannot be empty or null");
                }

                return identity;
            }
        }



        /// <summary>
        /// popup message
        /// </summary>
        /// <param name="showMessage"></param>
        /// <returns></returns>
        public async Task ShowAsync(string showMessage)
        {
            PopupAware popupAware = new PopupAware(PopupMode.Show, showMessage);

            PopupMessage(popupAware);

            await popupAware.DisplayAsync();

            popupAware.Dispose();

        }

        /// <summary>
        /// popup confirm message
        /// </summary>
        /// <param name="confirmMessage"></param>
        /// <returns></returns>
        public async Task<bool> ConfirmAsync(string confirmMessage)
        {
            PopupAware popupAware = new PopupAware(PopupMode.Confirm, confirmMessage);

            PopupMessage(popupAware);

            object result = await popupAware.DisplayAsync();

            popupAware.Dispose();

            if (result is bool @bool)
            {
                return @bool;
            }
            return false;
        }

        /// <summary>
        /// popup popupView control
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <param name="viewCreator"></param>
        /// <returns></returns>
        public async Task<object> PopupAsync<TView>(Func<TView> viewCreator) where TView : IPopupView
        {
            PopupAware popupAware = new PopupAware(PopupMode.Popup, "")
            {
                ViewCreator = () => viewCreator()
            };

            PopupView(popupAware);

            object result = await popupAware.DisplayAsync();

            popupAware.Dispose();

            return result;
        }

        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ConcurrentQueue<PopupAware> popupViewAwares = new();
        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool viewPopuped;

        private void PopupView(PopupAware popupAware)
        {
            lock (this)
            {
                popupViewAwares.Enqueue(popupAware);

                if (viewPopuped)
                {
                    return;
                }

                viewPopuped = true;
            }


            Dispatcher.InvokeAsync(async () =>
            {
                popupPanel.Visibility = Visibility.Visible;

                try
                {
                    while (popupViewAwares.TryDequeue(out PopupAware popupInfo))
                    {
                        IPopupView popupView = popupInfo.ViewCreator() as IPopupView;

                        if (popupView is not FrameworkElement element)
                        {
                            throw new Exception($"popup content must be a FrameworkElement");
                        }

                        if (element.DataContext is not IPopupViewModelAware viewModelAware)
                        {
                            throw new Exception($"popup data context must implement the {typeof(IPopupViewModelAware)}");
                        }

                        viewModelAware.PopupRequestClose += popupInfo.RequestClose;
                        popupView.RequestClosePopup += popupInfo.RequestClose;
                        popupInfo.OnClose = (s, e) =>
                        {
                            popupView.RequestClosePopup -= popupInfo.RequestClose;
                            viewModelAware.PopupRequestClose -= popupInfo.RequestClose;
                            viewModelAware.OnPopupClosed(e);
                        };

                        PopupContent = popupView;
                        await popupInfo.DisplayAsync();
                        PopupContent = null;
                    }

                }
                finally
                {
                    popupPanel.Visibility = Visibility.Collapsed;
                    viewPopuped = false;
                }
            });
        }



        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly ConcurrentQueue<PopupAware> popupMessageAwares = new();
        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool messagePopuped;
        private void PopupMessage(PopupAware popupAware)
        {
            lock (this)
            {
                popupMessageAwares.Enqueue(popupAware);

                if (messagePopuped)
                {
                    return;
                }

                messagePopuped = true;
            }


            Dispatcher.InvokeAsync(async () =>
            {
                showPanel.Visibility = Visibility.Visible;

                try
                {
                    while (popupMessageAwares.TryDequeue(out PopupAware popupInfo))
                    {
                        IMessagePopupView messageView = MessagePopupView ??= new DefaultMessagePopupView();
                        messageView.Message = popupInfo.Message;
                        messageView.HideCancel = popupInfo.Mode == PopupMode.Show;
                        messageView.RequestClosePopup += popupInfo.RequestClose;
                        var viewModelAware = messageView.DataContext as IPopupViewModelAware;
                        if(viewModelAware != null)
                        {
                            viewModelAware.PopupRequestClose += popupInfo.RequestClose;
                        }
                        popupInfo.OnClose = (s, e) => 
                        {
                            messageView.RequestClosePopup -= popupInfo.RequestClose;
                            if (viewModelAware != null)
                            {
                                viewModelAware.PopupRequestClose -= popupInfo.RequestClose;
                                viewModelAware.OnPopupClosed(e);
                            }
                        };

                        await popupInfo.DisplayAsync();
                    }
                }
                finally
                {
                    showPanel.Visibility = Visibility.Collapsed;
                    messagePopuped = false;
                }

            });
        }


        private enum PopupMode { Show, Confirm, Popup }
        private class PopupAware : IDisposable
        {
            public PopupAware(PopupMode Mode, string Message)
            {
                this.Mode = Mode;
                this.Message = Message;
            }


            private SemaphoreSlim semaphoreSlim = new(0);
            private int semaphoreCounter;
            private object popupResult;
            public PopupMode Mode { get; set; }

            public string Message { get; set; }

            public EventHandler<PopupResultEventArgs> OnClose { get; set; }

            public Func<object> ViewCreator { get; set; }

            public void RequestClose(object sender, PopupResultEventArgs e)
            {
                OnClose?.Invoke(sender, e);
                popupResult = e.PopupResult;
                semaphoreSlim.Release(semaphoreCounter);
                semaphoreCounter = 0;
            }

            public async Task<object> DisplayAsync()
            {
                Interlocked.Increment(ref semaphoreCounter);
                await semaphoreSlim.WaitAsync();
                return popupResult;
            }

            public void Dispose()
            {
                semaphoreSlim?.Dispose();
                semaphoreSlim = null;
                OnClose = null;
                ViewCreator = null;
            }
        }
    }
}
