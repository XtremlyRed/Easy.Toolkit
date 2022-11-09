using System.Collections.Generic;

namespace Easy.Toolkit
{
    /// <summary>
    /// navigation aware
    /// </summary>
    public interface INavigationViewAware 
    {
        /// <summary>
        /// navigetion to this view aware from old view 
        /// </summary>
        /// <param name="navigationParameters"></param>
        public void NavigateTo(NavigationParameters navigationParameters = null);

        /// <summary>
        /// navigetion from this view to new view
        /// </summary>
        public void NavigateFrom();

    }



    public class NavigationParameters
    {
        private readonly Dictionary<string, object> dictionary = new Dictionary<string, object>();


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


        public void SetValue(string parameterKey, object value)
        {
            if (string.IsNullOrEmpty(parameterKey))
            {
                throw new ArgumentException("key cannot be empty or null");
            }

            dictionary[parameterKey] = value;
        }
    }
}
