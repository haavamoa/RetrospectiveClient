﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace RetrospectiveClient.Resources.Converters.ValueConverters
{
    public abstract class ConverterMarkupExtension<T> : MarkupExtension, IValueConverter where T : class, new()
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}