using System;
using System.Globalization;

namespace Retrospective.Clients.WPF.Resources.Converters.ValueConverters
{
    public class InvertedBoolConverter : ConverterMarkupExtension<InvertedBoolConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool booleanvalue && !booleanvalue;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}