using System;
using System.Globalization;

namespace Retrospective.Clients.WPF.Resources.Converters.ValueConverters
{
    public class NullToBoolConverter : ConverterMarkupExtension<NullToBoolConverter>
    {
        public bool Inverted { get; set; }
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!Inverted)
            {
                return value == null;
            }
            return value != null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
