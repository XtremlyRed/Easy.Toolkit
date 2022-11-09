using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Easy.Toolkit
{
    public interface IRelayCommand<TParameter> : ICommand
    {
        bool CanExecute(TParameter parameter);

        void Execute(TParameter parameter);
    }

    public interface IRelayCommandAsync<TParameter> : ICommand
    {
        bool CanExecute(TParameter parameter); 

        Task ExecuteAsync(TParameter parameter);
    }


    public class RelayCommand<TParameter> : ICommand, IRelayCommand<TParameter>, IRelayCommandAsync<TParameter>
    {
        public event EventHandler CanExecuteChanged;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private readonly Action<TParameter> executeCallback;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private readonly Func<TParameter, bool> canExecuteCallback = null;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private readonly Func<TParameter, Task> executeFuncCallback;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private readonly bool executeAsync;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool isExecuting;

        public RelayCommand(Action<TParameter> executeCallback, Func<TParameter, bool> canExecuteCallback = null)
        {
            this.executeCallback = executeCallback;
            this.canExecuteCallback = canExecuteCallback;
        }

        public RelayCommand(Func<TParameter, Task> executeFuncCallback, Func<TParameter, bool> canExecuteCallback = null)
        {
            executeAsync = true;
            this.executeFuncCallback = executeFuncCallback;
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

            if (parameter is TParameter parameter2)
            {
                p = parameter2;
            }

            if (executeAsync == false)
            {
                Execute(p);

                return;
            }

            await ExecuteAsync(p);

        }

        public bool CanExecute(TParameter parameter)
        {
            return canExecuteCallback?.Invoke(parameter) ?? true;
        }

        public void Execute(TParameter parameter)
        {
            if (executeCallback is null)
            {
                return;
            }

            try
            {
                isExecuting = true;
                RaiseCanExecuteChanged();
                executeCallback.Invoke(parameter);
            }
            finally
            {
                isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        public virtual void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public Task ExecuteAsync(TParameter parameter)
        {
            if (executeFuncCallback is null)
            {
                return Task.FromResult(false);
            }
            isExecuting = true;

            RaiseCanExecuteChanged();

            return executeFuncCallback
                .Invoke(parameter)
                .ContinueWith(t =>
                {
                    isExecuting = false;
                    RaiseCanExecuteChanged();
                    t.Wait();
                });
        }


        public static implicit operator RelayCommand<TParameter>(Action<TParameter> commandAction)
        {
            return new RelayCommand<TParameter>(commandAction);
        }
    }

}
