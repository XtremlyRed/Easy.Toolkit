using System.Collections.Generic;

namespace Easy.Toolkit
{
    /// <summary>
    /// dialog parameters
    /// </summary>
    public interface IDialogParameters
    {


        /// <summary>
        ///   get parameter value by <paramref name="parameterKey"/>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="parameterKey"></param> 
        /// <returns></returns>
        TValue GetValue<TValue>(string parameterKey);

        /// <summary>
        /// try get parameter value by <paramref name="parameterKey"/>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="parameterKey"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool TryGetValue<TValue>(string parameterKey, out TValue value);

        /// <summary>
        /// set parameter value with <paramref name="parameterKey"/>
        /// </summary>
        /// <param name="parameterKey"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IDialogParameters SetValue(string parameterKey, object value);

        /// <summary>
        /// set value by <paramref name="parameterKey"/>
        /// </summary>
        /// <param name="parameterKey"></param>
        /// <returns></returns>
        object this[string parameterKey] { set; }
    }

    /// <summary>
    /// dialog parameters
    /// </summary>
    [DebuggerDisplay("{KVs}")]
    public class DialogParameters : IDialogParameters
    {
        [EditorBrowsable(EditorBrowsableState.Never), DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Dictionary<string, object> dictionary = new Dictionary<string, object>();
        
        /// <summary>
        /// key value map
        /// </summary>
        public IReadOnlyDictionary<string, object> KVs => dictionary.ToReadOnlyDictionary();



        /// <summary>
        /// try get parameter value by <paramref name="parameterKey"/>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="parameterKey"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public TValue GetValue<TValue>(string parameterKey)
        {
            if (string.IsNullOrEmpty(parameterKey))
            {
                throw new ArgumentException("key cannot be empty or null");
            }

            if (dictionary.TryGetValue(parameterKey, out object value1))
            {
                if (value1 is TValue tv)
                {
                    return tv;
                }
            }
            return default;
        }
        /// <summary>
        /// try get parameter value by <paramref name="parameterKey"/>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="parameterKey"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public bool TryGetValue<TValue>(string parameterKey, out TValue value)
        {
            if (string.IsNullOrEmpty(parameterKey))
            {
                throw new ArgumentException("key cannot be empty or null");
            }

            if (dictionary.TryGetValue(parameterKey, out object value1))
            {
                if (value1 is TValue tv)
                {
                    value = tv;
                    return true;
                }
            }

            value = default;
            return false;
        }

        /// <summary>
        /// set parameter value with <paramref name="parameterKey"/>
        /// </summary>
        /// <param name="parameterKey"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IDialogParameters SetValue(string parameterKey, object value)
        {
            if (string.IsNullOrEmpty(parameterKey))
            {
                throw new ArgumentException("key cannot be empty or null");
            }

            dictionary[parameterKey] = value;

            return this;
        }

        /// <summary>
        ///  set value by <paramref name="parameterKey"/>
        /// </summary>
        /// <param name="parameterKey"></param>
        /// <exception cref="ArgumentNullException"><paramref name="parameterKey"/> is null</exception>
        /// <returns></returns>
        public object this[string parameterKey]
        {
            set
            {
                if (parameterKey is null)
                {
                    throw new ArgumentNullException(nameof(parameterKey));
                }

                dictionary[parameterKey] = value;
            }
        }
    }


    /// <summary>
    /// dialog result
    /// </summary>
    public sealed class DialogOperateResult
    {
        /// <summary>
        /// result mode
        /// </summary>
        public DialogResult DialogResult { get; set; }

        /// <summary>
        /// dialog parameters
        /// </summary>
        public IDialogParameters Parameters { get; set; }

        /// <summary>
        /// create a new dialog result event args from <paramref name="dialogResult"/>
        /// </summary>
        /// <param name="dialogResult"></param>
        public static implicit operator DialogOperateResult(DialogResult dialogResult)
        {
            return new DialogOperateResult() { DialogResult = dialogResult };
        }
    }

    /// <summary>
    /// dialog result
    /// </summary>
    public enum DialogResult
    {
        /// <summary>
        /// none
        /// </summary>
        None = 0,
        /// <summary>
        /// ok
        /// </summary>
        OK = 1,
        /// <summary>
        /// cancel
        /// </summary>
        Cancel = 2,
        /// <summary>
        /// abort
        /// </summary>
        Abort = 3,
        /// <summary>
        /// rery
        /// </summary>
        Retry = 4,
        /// <summary>
        /// ignore
        /// </summary>
        Ignore = 5,
        /// <summary>
        /// yes
        /// </summary>
        Yes = 6,
        /// <summary>
        /// no
        /// </summary>
        No = 7,
        /// <summary>
        /// try again
        /// </summary>
        TryAgain = 10,
        /// <summary>
        /// continue
        /// </summary>
        Continue = 11
    }



    /// <summary>
    /// dialog result
    /// </summary>
    public class DialogResultEventArgs : EventArgs
    {
        /// <summary>
        /// create a new dialog result event args
        /// </summary>
        /// <param name="dialogResultMode"></param>
        public DialogResultEventArgs(DialogResult dialogResultMode)
        {
            DialogResultMode = dialogResultMode;
        }

        /// <summary>
        /// create a new dialog result event args
        /// </summary>
        /// <param name="dialogResultMode"></param>
        /// <param name="dialogParameters"></param>
        public DialogResultEventArgs(DialogResult dialogResultMode, IDialogParameters dialogParameters)
        {
            DialogResultMode = dialogResultMode;
            DialogParameters = dialogParameters;
        }

        /// <summary>
        /// dialog parameters
        /// </summary>
        public IDialogParameters DialogParameters { get; }

        /// <summary>
        /// dialog result
        /// </summary>
        public DialogResult DialogResultMode { get; }

        /// <summary>
        /// create a new dialog result event args from <paramref name="dialogResultMode"/>
        /// </summary>
        /// <param name="dialogResultMode"></param>
        public static implicit operator DialogResultEventArgs(DialogResult dialogResultMode)
        {
            return new DialogResultEventArgs(dialogResultMode);
        }

        /// <summary>
        /// get <see cref="DialogResultMode"/> result from <paramref name="dialogResultEventArgs"/>
        /// </summary>
        /// <param name="dialogResultEventArgs"></param>
        public static explicit operator DialogResult(DialogResultEventArgs dialogResultEventArgs)
        {
            return dialogResultEventArgs.DialogResultMode;
        }
    }
}
