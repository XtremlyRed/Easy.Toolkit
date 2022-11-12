using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace Easy.Toolkit
{
    /// <summary>
    /// TargetTypeConverter
    /// </summary>
    /// <typeparam name="TargetType"></typeparam>
    /// <typeparam name="ParamType"></typeparam>
    public class TargetTypeConverter<TargetType, ParamType> : TypeConverter
    {
        private static readonly char[] ConnectChar = new char[] { '‘', '’', '。', '（', '）', '(', ')', '<', '>', '《', '》', '{', '}', '[', ']', ',', '，', '`', '~', '*', '^', '`', '·', ';', '；', ':', '：', ' ' };

        /// <summary>
        /// CanConvertFrom
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// CanConvertTo
        /// </summary>
        /// <param name="context"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// ConvertFrom
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is not string stringValue)
            {
                return base.ConvertFrom(context, culture, value);
            }

            string[] strArray = stringValue.Split(ConnectChar, StringSplitOptions.RemoveEmptyEntries);

            if (strArray is null || strArray.Length == 0)
            {
                return default;
            }

            ParamType[] numArray = new ParamType[strArray.Length];

            TypeConverter converter = TypeDescriptor.GetConverter(typeof(ParamType));
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = (ParamType)converter.ConvertFromString(context, culture, strArray[i]);
            }

            TargetType tartet = (TargetType)Activator.CreateInstance(typeof(TargetType), numArray);

            return tartet;
        }
    }

    /// <summary>
    /// TargetTypeArrayConverter
    /// </summary>
    /// <typeparam name="TargetType"></typeparam>
    /// <typeparam name="ParamType"></typeparam>
    public class TargetTypeArrayConverter<TargetType, ParamType> : TypeConverter
    {
        private static readonly char[] ConnectChar = new char[] { '‘', '’', '。', '（', '）', '(', ')', '<', '>', '《', '》', '{', '}', '[', ']', ',', '，', '`', '~', '*', '^', '`', '·', ';', '；', ':', '：', ' ' };

        /// <summary>
        /// CanConvertFrom
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// CanConvertTo
        /// </summary>
        /// <param name="context"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// ConvertFrom
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is not string stringValue)
            {
                return base.ConvertFrom(context, culture, value);
            }

            stringValue = stringValue.Replace("\r", "").Replace("\n", "");

            string[] strArray = stringValue.Split(ConnectChar, StringSplitOptions.RemoveEmptyEntries);

            if (strArray is null || strArray.Length == 0)
            {
                return default;
            }

            ParamType[] numArray = new ParamType[strArray.Length];

            TypeConverter converter = TypeDescriptor.GetConverter(typeof(ParamType));
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = (ParamType)converter.ConvertFromString(context, culture, strArray[i]);
            }

            ParamType[][] paramTypes = new ParamType[1][];

            paramTypes[0] = numArray;

            TargetType tartet = (TargetType)Activator.CreateInstance(typeof(TargetType), paramTypes);

            return tartet;
        }
    }
}
