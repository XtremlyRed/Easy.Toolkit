using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Easy.Toolkit
{

    public class RelayCommandAsync<TParameter> : ICommand, IRelayCommandAsync<TParameter>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private readonly SynchronizationContext synchronizationContext = SynchronizationContext.Current;
        public event EventHandler CanExecuteChanged;


        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private readonly Func<TParameter, Task> executeCallback;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private readonly Func<TParameter, bool> canExecuteCallback = null;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool isExecuting;

        public RelayCommandAsync(Func<TParameter, Task> executeCallback, Func<TParameter, bool> canExecuteCallback = null)
        {
            this.executeCallback = executeCallback;
            this.canExecuteCallback = canExecuteCallback;
        }

        bool ICommand.CanExecute(object parameter)
        {
            if (isExecuting)
            {
                return false;
            }

            if (parameter is TParameter parameter1)
            {
                return CanExecute(parameter1);
            }

            return true;
        }

        async void ICommand.Execute(object parameter)
        {
            TParameter p = default;

            if (parameter is TParameter parameter1)
            {
                p = parameter1;
            }

            await ExecuteAsync(p);
        }

        public bool CanExecute(TParameter parameter)
        {
            return canExecuteCallback?.Invoke(parameter) ?? true;
        }

        public Task ExecuteAsync(TParameter parameter)
        {
            if (executeCallback is null)
            {
                return Task.FromResult(false);
            }

            isExecuting = true;
             
            return executeCallback
                .Invoke(parameter)
                .ContinueWith(t =>
                {
                    isExecuting = false; 
                    t.Wait();
                });

        }

        public virtual void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }


        public static implicit operator RelayCommandAsync<TParameter>(Func<TParameter, Task> commandAction)
        {
            return new RelayCommandAsync<TParameter>(commandAction);
        }
    }
}
