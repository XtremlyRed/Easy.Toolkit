using System.Collections.Generic;
using System.Linq;

namespace Easy.Toolkit
{
    /// <summary>
    /// popup parameter collection
    /// </summary>
    public interface IPopupParameters
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
        IPopupParameters SetValue(string parameterKey, object value);
    }




    /// <summary>
    /// popup parameters
    /// </summary>
    [DebuggerDisplay("{KVs}")]
    public class PopupParameters : IPopupParameters
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
        /// <exception cref="ArgumentException"></exception>
        public IPopupParameters SetValue(string parameterKey, object value)
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
    /// popup view model aware
    /// </summary>
    public interface IPopupViewModelAware
    {
        /// <summary>
        /// called when view request close
        /// </summary>
        event EventHandler<PopupResultEventArgs> PopupRequestClose;


        /// <summary>
        /// called when the popup is closed.
        /// </summary>
        void OnPopupClosed(PopupResultEventArgs popupResult);

        /// <summary>
        /// called when the popup is opened.
        /// </summary>
        /// <param name="parameters">The parameters passed to the popup.</param>
        void OnPopupOpened(IPopupParameters parameters);
    }
}
