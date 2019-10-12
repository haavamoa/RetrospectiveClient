using System;
using System.Globalization;
using System.Windows;

namespace RetrospectiveClient.Resources.Converters.ValueConverters
{
    /// <summary>
    /// This BooleanToVisibility converter allows us to override the converted value when
    /// the bound value is false.
    /// 
    /// The built-in converter in WPF restricts us to always use Collapsed when the bound 
    /// value is false.
    /// </summary>
    public class BooleanToVisibilityConverter : ConverterMarkupExtension<BooleanToVisibilityConverter>
    {
        public bool Inverted { get; set; }

        #region IValueConverter Members

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (bool) value;
            if (Inverted)
            {
                boolValue = !boolValue;
            }

            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}