using System;
using System.Globalization;
using System.Windows;

namespace Retrospective.Clients.WPF.Resources.Converters.ValueConverters
{
    public class StringIsNullOrEmptyToVisibilityConverter : ConverterMarkupExtension<StringIsNullOrEmptyToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolean = (bool)new StringIsNullOrEmptyBoolConverter().Convert(value, targetType, parameter, culture);
            return boolean ? (Inverted ? Visibility.Collapsed : Visibility.Visible) : Visibility.Collapsed;
        }


        public bool Inverted { get; set; }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}