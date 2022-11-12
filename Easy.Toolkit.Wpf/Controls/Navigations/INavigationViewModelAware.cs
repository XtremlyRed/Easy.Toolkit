using System.Collections.Generic;

namespace Easy.Toolkit
{
    /// <summary>
    /// navigation aware
    /// </summary>
    public interface INavigationViewModelAware 
    {
        /// <summary>
        /// navigetion to this view aware from old view 
        /// </summary>
        /// <param name="navigationParameters"></param>
        public void NavigateTo(INavigationParameters navigationParameters = null);

        /// <summary>
        /// navigetion from this view to new view
        /// </summary>
        public void NavigateFrom();

    }

    /// <summary>
    /// navigation parameter collection
    /// </summary>
    public interface INavigationParameters
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
        INavigationParameters SetValue(string parameterKey, object value);
    }

    /// <summary>
    /// navigation parameters
    /// </summary>
   [DebuggerDisplay("{KVs}")] public class NavigationParameters: INavigationParameters
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
        public INavigationParameters SetValue(string parameterKey, object value)
        {
            if (string.IsNullOrEmpty(parameterKey))
            {
                throw new ArgumentException("key cannot be empty or null");
            }

            dictionary[parameterKey] = value;

            return this;
        }
    }
}

