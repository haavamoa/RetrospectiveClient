using System;
using System.Windows;

namespace Retrospective.Clients.WPF.Resources.Converters.ValueConverters
{
    public class CountToVisibilityConverter : ConverterMarkupExtension<CountToVisibilityConverter>
    {
        public bool Inverted { get; set; }

        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int count = (int)value;

            Visibility returnValue = count > 0 ? Visibility.Visible : Visibility.Collapsed;

            if (Inverted)
            {
                returnValue = returnValue == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            }

            return returnValue;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
