using System;
using System.Globalization;

namespace RetrospectiveClient.Resources.Converters.ValueConverters
{
    public class StringIsNullOrEmptyBoolConverter : ConverterMarkupExtension<StringIsNullOrEmptyBoolConverter>
    {
        public bool Inverted { get; set; }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return !Inverted;
            }

            if (!(value is string))
            {
                throw new ArgumentException(
                                            $"{nameof(StringIsNullOrEmptyToVisibilityConverter)} is provided with wrong argument type, expected is string");
            }

            var stringValue = value as string;
            return string.IsNullOrEmpty(stringValue) && !Inverted;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}