using System.Collections.Generic;

namespace Easy.Toolkit
{
    /// <summary>
    /// dialog parameters
    /// </summary>
    public interface IDialogParameters
    {
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
        /// <param name="dialogResult"></param>
        public DialogResultEventArgs(DialogResult dialogResult)
        {
            DialogResult = dialogResult;
        }

        /// <summary>
        /// dialog result
        /// </summary>
        public DialogResult DialogResult { get; }

        /// <summary>
        /// create a new dialog result event args from <paramref name="dialogResult"/>
        /// </summary>
        /// <param name="dialogResult"></param>
        public static implicit operator DialogResultEventArgs(DialogResult dialogResult)
        {
            return new DialogResultEventArgs(dialogResult);
        }

        /// <summary>
        /// get <see cref="DialogResult"/> result from <paramref name="dialogResultEventArgs"/>
        /// </summary>
        /// <param name="dialogResultEventArgs"></param>
        public static explicit operator DialogResult(DialogResultEventArgs dialogResultEventArgs)
        {
            return dialogResultEventArgs.DialogResult;
        }
    }
}
