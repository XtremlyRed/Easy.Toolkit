using System.Globalization;
using System.Windows.Data;

namespace Easy.Toolkit
{
    /// <summary>
    /// converter base from <typeparamref name="TargetValue"/>
    /// </summary>
    /// <typeparam name="TargetValue"></typeparam>
    public abstract class ValueConverterBase<TargetValue> : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TargetValue target)
            {
                Convert(target, targetType, parameter, culture);
            }

            return value;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertBack(value, targetType, parameter, culture);
        }

        /// <summary>
        /// convert from <typeparamref name="TargetValue"/> <paramref name="value"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        protected abstract object Convert(TargetValue value, Type targetType, object parameter, CultureInfo culture);


        /// <summary>
        /// convert from <see cref="object"/> <paramref name="value"/> to <typeparamref name="TargetValue"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected virtual TargetValue ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
