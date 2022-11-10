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



        public async Task ShowAsync(string showMessage)
        {
            PopupAware popupAware = new PopupAware(PopupMode.Show, showMessage);

            PopupMessage(popupAware);

            await popupAware.DisplayAsync();

            popupAware.Dispose();

        }

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

        public async Task<object> PopupAsync<TView>(Func<TView> viewCreator) where TView : IPopupControl
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
                        IPopupView view = popupInfo.ViewCreator() as IPopupView;

                        if (view is null)
                        {
                            continue;
                        }

                        view.RequestClose += popupInfo.RequestClose;
                        popupInfo.OnClose = () => view.RequestClose -= popupInfo.RequestClose;

                        PopupContent = view;
                        await popupInfo.DisplayAsync();
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
                        messageView.RequestClose += popupInfo.RequestClose;

                        popupInfo.OnClose = () => messageView.RequestClose -= popupInfo.RequestClose;

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

            public Action OnClose { get; set; }

            public Func<object> ViewCreator { get; set; }

            public void RequestClose(object sender, PopupResultEventArgs e)
            {
                OnClose?.Invoke();
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
