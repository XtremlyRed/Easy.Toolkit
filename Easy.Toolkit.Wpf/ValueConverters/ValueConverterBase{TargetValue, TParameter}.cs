using System.Globalization;
using System.Windows.Data;

namespace Easy.Toolkit 
{
    /// <summary>
    /// converter base from <typeparamref name="TargetValue"/> and <typeparamref name="TParameter"/>
    /// </summary>
    /// <typeparam name="TargetValue"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    public abstract class ValueConverterBase<TargetValue, TParameter> : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not TargetValue targetValue || Invoker.TryCast<TParameter>(parameter,out var parameter1) == false)
            {
                return value;
            }

             
            return Convert(targetValue, targetType, parameter1, culture);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertBack(  value,   targetType,   parameter,   culture);
        }



        /// <summary>
        /// convert from <typeparamref name="TargetValue"/> <paramref name="value"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        protected abstract object Convert(TargetValue value, Type targetType, TParameter parameter, CultureInfo culture);


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
