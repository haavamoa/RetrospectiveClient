using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace RetrospectiveClient.Resources.Converters.MultiValueConverters
{
    public abstract class MultiConverterMarkupExtension<T> : MarkupExtension, IMultiValueConverter where T : class, new()
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
        public abstract object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);

        public abstract object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture);
    }
}