using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Easy.Toolkit
{
    public interface IRelayCommandAsync : ICommand
    {
        bool CanExecute();

        Task ExecuteAsync();
    }


    public class RelayCommandAsync : ICommand, IRelayCommandAsync
    {
        public event EventHandler CanExecuteChanged;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private readonly Func<Task> executeCallback;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private readonly Func<bool> canExecuteCallback = null;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool isExecuting;

        public RelayCommandAsync(Func<Task> executeCallback, Func<bool> canExecuteCallback = null)
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

            return CanExecute();
        }

        async void ICommand.Execute(object parameter)
        {
            await ExecuteAsync();
        }

        public bool CanExecute()
        {
            return canExecuteCallback?.Invoke() ?? true;
        }

        public Task ExecuteAsync()
        {
            if (executeCallback is null)
            {
                return Task.FromResult(false);
            }

            isExecuting = true;

            RaiseCanExecuteChanged();

            return executeCallback
                   .Invoke()
                   .ContinueWith(y =>
                   {
                       isExecuting = false;
                       RaiseCanExecuteChanged();
                       y.Wait();
                   });

        }

        public virtual void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
         
        public static implicit operator RelayCommandAsync(Func<Task> commandAction)
        {
            return new RelayCommandAsync(commandAction);
        }

    }

}
