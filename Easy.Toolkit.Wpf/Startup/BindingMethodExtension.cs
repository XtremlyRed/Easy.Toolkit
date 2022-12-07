using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

using BF = System.Reflection.BindingFlags;

namespace Easy.Toolkit
{
    /// <summary>
    /// binding view model 's method name
    /// </summary>
    [MarkupExtensionReturnType(typeof(ICommand))]
    [Localizability(LocalizationCategory.NeverLocalize)]
    public class BindingMethodExtension : MarkupExtension, ICommand
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        private bool syncExecute;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        private bool asyncExecute;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        private Action<object> execute;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        private Func<object, Task> executeAsync;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        private Func<object, bool> canExecute;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        private FrameworkElement frameworkElement;

        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        private string executeMethodName;
        [DebuggerBrowsable(DebuggerBrowsableState.Never), EditorBrowsable(EditorBrowsableState.Never)]
        private string canExecuteMethodName;

        /// <summary>
        /// method name
        /// </summary>
        public string ExecuteMethodName
        {
            get => executeMethodName;
            set
            {
                executeMethodName = value;
                execute = null;
                executeAsync = null;
            }
        }

        /// <summary>
        /// can execute method name
        /// </summary>
        public string CanExecuteMethodName
        {
            get => canExecuteMethodName;
            set
            {
                canExecuteMethodName = value;
                canExecute = null;
            }
        }

        /// <summary>
        /// create new binding 
        /// </summary>
        public BindingMethodExtension()
        {
        }

        /// <summary>
        /// create new binding
        /// </summary>
        /// <param name="executeName"></param>
        public BindingMethodExtension(string executeName)
        {
            ExecuteMethodName = executeName;
        }

        /// <summary>
        /// create new binding
        /// </summary>
        public BindingMethodExtension(string executeName, string canExecuteName)
            : this(executeName)
        {
            CanExecuteMethodName = canExecuteName;
        }

        /// <summary>
        /// <see cref="MarkupExtension.ProvideValue(IServiceProvider)"/>
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public sealed override object ProvideValue(IServiceProvider serviceProvider)
        {
            object service = serviceProvider.GetService(typeof(IProvideValueTarget));
            if (service is not IProvideValueTarget provideValueTarget)
            {
                throw new ArgumentException($"The {nameof(serviceProvider)} must implement {nameof(IProvideValueTarget)} interface.");
            }

            object targetObject = provideValueTarget.TargetObject;
            if (targetObject.GetType().FullName == "System.Windows.SharedDp")
            {
                return this;
            }

            if (targetObject is not FrameworkElement frameworkElement)
            {
                throw new ArgumentException($"The bound element must be derived from the {typeof(FrameworkElement).FullName}.");
            }

            this.frameworkElement = frameworkElement;
            this.frameworkElement.DataContextChanged += (s, e) =>
            {
                execute = null;
                canExecute = null;
                executeAsync = null;
            };
            return this;
        }

        #region Implements ICommand

        /// <summary>
        /// can execute changed
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// can execute 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
            {
                object context = frameworkElement?.DataContext;
                if (context == null)
                {
                    return true;
                }

                string methodName = CanExecuteMethodName;
                if (methodName == null)
                {
                    return true;
                }

                System.Reflection.MethodInfo method = context.GetType().GetMethod(methodName);

                if (method == null)
                {
                    Type contextType = context.GetType();
                    throw new NullReferenceException($"Not found the method named \"{methodName}\" in {contextType} type.");
                }

                System.Reflection.ParameterInfo[] parameters = method.GetParameters();
                if (parameters.Length > 1)
                {
                    throw new InvalidOperationException($"The method named \"{methodName}\" must only have 0 or 1 parameters.");
                }

                bool hasParameter = parameters.Length > 0;

                if (method.ReturnType != typeof(bool))
                {
                    throw new InvalidOperationException($"The method named \"{methodName}\" must return bool type.");
                }

                canExecute = (args) => (bool)method.Invoke(context, hasParameter ? new[] { args } : null);
            }


            return canExecute(parameter);
        }

        /// <summary>
        /// execute command
        /// </summary>
        /// <param name="parameter"></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async void Execute(object parameter)
        {
            if (syncExecute == false && asyncExecute == false)
            {
                object context = frameworkElement?.DataContext;

                if (context == null)
                {
                    return;
                }

                string methodName = ExecuteMethodName;
                System.Reflection.MethodInfo method = context.GetType().GetMethod(methodName, BF.Instance | BF.Public | BF.NonPublic);

                if (method == null)
                {
                    Type contextType = context.GetType();
                    throw new NullReferenceException($"Not found the method named \"{methodName}\" in {contextType} type.");
                }

                System.Reflection.ParameterInfo[] parameters = method.GetParameters();
                if (parameters.Length > 1)
                {
                    throw new InvalidOperationException($"The method named \"{methodName}\" must only have 0 or 1 parameters.");
                }

                bool hasParameter = parameters.Length > 0;
                bool isTask = typeof(Task).IsAssignableFrom(method.ReturnType);

                syncExecute = !isTask;
                asyncExecute = isTask;

                if (syncExecute)
                {
                    execute = (arg) => method.Invoke(context, hasParameter ? new[] { arg } : null);
                }

                if (asyncExecute)
                {
                    executeAsync = (arg) => (Task)method.Invoke(context, hasParameter ? new[] { arg } : null);
                }
            }

            if (syncExecute)
            {
                execute(parameter);
            }

            if (asyncExecute)
            {
                await executeAsync(parameter);
            }
        }


        /// <summary>
        /// raise can execute changed
        /// </summary>
        public virtual void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
