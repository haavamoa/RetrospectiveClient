using System;
using System.Globalization;
using System.Windows;

namespace RetrospectiveClient.Resources.Converters.ValueConverters
{
    public class NullToVisibilityConverter : ConverterMarkupExtension<NullToVisibilityConverter>
    {
        public bool Inverted { get; set; }

        #region IValueConverter Members

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(Visibility))
            {
                Visibility returnValue;

                var stringValue = value as string;
                if (stringValue != null)
                {
                    returnValue = String.IsNullOrEmpty(stringValue) ? Visibility.Collapsed : Visibility.Visible;
                }
                else
                {
                    returnValue = value == null ? Visibility.Collapsed : Visibility.Visible;
                }

                if (Inverted)
                {
                    if (returnValue == Visibility.Visible)
                    {
                        returnValue = Visibility.Collapsed;
                    }
                    else
                    {
                        returnValue = Visibility.Visible;
                    }
                }

                return returnValue;
            }

            return value;
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