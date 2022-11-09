using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Easy.Toolkit
{
    public class RelayCommand : ICommand, IRelayCommandAsync
    {

        public event EventHandler CanExecuteChanged;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private readonly Action executeCallback;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private readonly Func<bool> canExecuteCallback = null;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private readonly Func<Task> executeFuncCallback;


        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private readonly bool executeAsync;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool isExecuting;

        public RelayCommand(Func<Task> executeFuncCallback, Func<bool> canExecuteCallback = null)
        {
            executeAsync = true;
            this.executeFuncCallback = executeFuncCallback;
            this.canExecuteCallback = canExecuteCallback;
        }


        public RelayCommand(Action executeCallback, Func<bool> canExecuteCallback = null)
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
            if (executeAsync == false)
            {
                Execute();
                return;
            }

            await ExecuteAsync();

        }

        public bool CanExecute()
        {
            return canExecuteCallback?.Invoke() ?? true;
        }

        public void Execute()
        {
            if (executeCallback is null)
            {
                return;
            }
            try
            {
                isExecuting = true;

                RaiseCanExecuteChanged();
                executeCallback.Invoke();
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

        public Task ExecuteAsync()
        {
            if (executeFuncCallback is null)
            {
                return Task.FromResult(false);
            }
            isExecuting = true;

            RaiseCanExecuteChanged();

            return executeFuncCallback
                   .Invoke()
                   .ContinueWith(y =>
                   {
                       isExecuting = false;
                       RaiseCanExecuteChanged();
                       y.Wait();
                   });
        }


        public static implicit operator RelayCommand(Action commandAction)
        {
            return new RelayCommand(commandAction  );
        }
         
    }

}
