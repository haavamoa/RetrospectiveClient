using System;
using System.Globalization;
using System.Windows;

namespace RetrospectiveClient.Resources.Converters.ValueConverters
{
    class VisibilityConverter : ConverterMarkupExtension<VisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Visibility))
            {
                throw new ArgumentException($"{nameof(VisibilityConverter)} : input is not of type {nameof(Visibility)}");
            }

            var visibilityValue = (Visibility)value;
            return visibilityValue == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
