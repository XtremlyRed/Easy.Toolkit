using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Easy.Toolkit
{
    /// <summary>
    /// <see cref="IRelayCommand{TParameter}"/>
    /// </summary>
    /// <typeparam name="TParameter"></typeparam>
    public interface IRelayCommand<TParameter> : ICommand
    {
        /// <summary>
        /// can execute command
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        bool CanExecute(TParameter parameter);

        /// <summary>
        /// execute command
        /// </summary>
        /// <param name="parameter"></param>
        void Execute(TParameter parameter);
    }

    /// <summary>
    /// <see cref="IRelayCommandAsync{TParameter}"/>
    /// </summary>
    /// <typeparam name="TParameter"></typeparam>
    public interface IRelayCommandAsync<TParameter> : ICommand
    {
        /// <summary>
        /// can execute command
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        bool CanExecute(TParameter parameter);

        /// <summary>
        /// execute command async
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Task ExecuteAsync(TParameter parameter);
    }

    /// <summary>
    /// RelayCommand
    /// </summary>
    /// <typeparam name="TParameter"></typeparam>
    public class RelayCommand<TParameter> : ICommand, IRelayCommand<TParameter>, IRelayCommandAsync<TParameter>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private readonly SynchronizationContext synchronizationContext = SynchronizationContext.Current;

        /// <summary>
        /// can execute changed event
        /// </summary>
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

        /// <summary>
        /// create a new command
        /// </summary>
        /// <param name="executeCallback"></param>
        /// <param name="canExecuteCallback"></param>
        public RelayCommand(Action<TParameter> executeCallback, Func<TParameter, bool> canExecuteCallback = null)
        {
            this.executeCallback = executeCallback;
            this.canExecuteCallback = canExecuteCallback;
        }

        /// <summary>
        /// create a new command
        /// </summary>
        /// <param name="executeFuncCallback"></param>
        /// <param name="canExecuteCallback"></param>
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

        /// <summary>
        /// can execute with <typeparamref name="TParameter"/> <paramref name="parameter"/>
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(TParameter parameter)
        {
            return canExecuteCallback?.Invoke(parameter) ?? true;
        }

        /// <summary>
        /// execute command with <typeparamref name="TParameter"/> <paramref name="parameter"/>
        /// </summary>
        /// <param name="parameter"></param>
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

        /// <summary>
        /// raise can execute changed
        /// </summary>
        public virtual void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// execute command async
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public Task ExecuteAsync(TParameter parameter)
        {
            if (executeFuncCallback is null)
            {
                return Task.FromResult(false);
            }
            isExecuting = true;
             
            return executeFuncCallback
                .Invoke(parameter)
                .ContinueWith(t =>
                {
                    isExecuting = false; 
                    t.Wait();
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandAction"></param>
        public static implicit operator RelayCommand<TParameter>(Action<TParameter> commandAction)
        {
            return new RelayCommand<TParameter>(commandAction);
        }
    }

}
