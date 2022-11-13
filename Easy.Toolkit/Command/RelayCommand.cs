using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Easy.Toolkit
{
    /// <summary>
    /// RelayCommand
    /// </summary>
    public class RelayCommand : ICommand, IRelayCommandAsync
    {

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private readonly SynchronizationContext synchronizationContext = SynchronizationContext.Current;

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

        /// <summary>
        /// create a new command  
        /// </summary>
        /// <param name="executeFuncCallback"></param>
        /// <param name="canExecuteCallback"></param>
        public RelayCommand(Func<Task> executeFuncCallback, Func<bool> canExecuteCallback = null)
        {
            executeAsync = true;
            this.executeFuncCallback = executeFuncCallback;
            this.canExecuteCallback = canExecuteCallback;
        }

        /// <summary>
        /// create a new command  
        /// </summary>
        /// <param name="executeCallback"></param>
        /// <param name="canExecuteCallback"></param>
        public RelayCommand(Action executeCallback, Func<bool> canExecuteCallback = null)
        {
            this.executeCallback = executeCallback;
            this.canExecuteCallback = canExecuteCallback;
        }

        /// <summary>
        /// can execute changed event
        /// </summary>
        public event EventHandler CanExecuteChanged;

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

        /// <summary>
        ///  can cexcute command
        /// </summary>
        /// <returns></returns>
        public bool CanExecute()
        {
            return canExecuteCallback?.Invoke() ?? true;
        }
        /// <summary>
        /// execute sync command
        /// </summary>
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

        /// <summary>
        /// raise event
        /// </summary>
        public virtual void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// execute an async command
        /// </summary>
        /// <returns></returns>
        public Task ExecuteAsync()
        {
            if (executeFuncCallback is null)
            {
                return Task.FromResult(false);
            }
            isExecuting = true;

            return executeFuncCallback
                   .Invoke()
                   .ContinueWith(y =>
                   {
                       isExecuting = false;
                       y.Wait();
                   });
        }

        /// <summary>
        /// create relaycommand from  <see cref="Action"/> <paramref name="commandAction"/>
        /// </summary>
        /// <param name="commandAction"></param>
        public static implicit operator RelayCommand(Action commandAction)
        {
            return new RelayCommand(commandAction);
        }


        /// <summary>
        ///  binding command
        /// </summary>
        /// <param name="commandAction"></param>
        /// <param name="canExecuteAction"></param>
        /// <returns></returns>
        public static RelayCommand Bind(Action commandAction, Func<bool> canExecuteAction = null)
        {
            return new RelayCommand(commandAction, canExecuteAction);
        }



        /// <summary>
        /// binding command
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="commandAction"></param>
        /// <param name="canExecuteAction"></param>
        /// <returns></returns>
        public static RelayCommand<Target> Bind<Target>(Action<Target> commandAction, Func<Target, bool> canExecuteAction = null)
        {
            return new RelayCommand<Target>(commandAction, canExecuteAction);
        }



        /// <summary>
        /// binding command
        /// </summary> 
        /// <param name="commandAction"></param>
        /// <param name="canExecuteAction"></param>
        /// <returns></returns>
        public static RelayCommandAsync Bind(Func<Task> commandAction, Func<bool> canExecuteAction = null)
        {
            return new RelayCommandAsync(commandAction, canExecuteAction);
        }



        /// <summary>
        /// binding command
        /// </summary>
        /// <typeparam name="Target"></typeparam>
        /// <param name="commandAction"></param>
        /// <param name="canExecuteAction"></param>
        /// <returns></returns>
        public static RelayCommandAsync<Target> Bind<Target>(Func<Target, Task> commandAction, Func<Target, bool> canExecuteAction = null)
        {
            return new RelayCommandAsync<Target>(commandAction, canExecuteAction);
        }



    }

}
