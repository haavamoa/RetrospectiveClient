using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace Retrospective.Clients.WPF.Resources.Converters.MultiValueConverters
{
    /// <summary>
    /// A converter to run a logical gate on multiple boolean values <see cref="LogicalGate"/>.
    /// Return value can be set by using <see cref="ReturnType"/>
    /// </summary>
    public class LogicalExpressionConverter : MultiConverterMarkupExtension<LogicalExpressionConverter>
    {
        public LogicalGate LogicalGate { get; set; }

        public ReturnType ReturnType { get; set; }

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
            {
                switch (ReturnType)
                {
                    case ReturnType.Visibility:
                        return Visibility.Collapsed;
                    case ReturnType.Boolean:
                        return false;
                    case ReturnType.Undefined:
                        return Visibility.Collapsed;
                }
            }

            foreach (var value in values)
            {
                if(value == DependencyProperty.UnsetValue || value == null)
                {
                    switch (ReturnType)
                    {
                        case ReturnType.Visibility:
                            return Visibility.Collapsed;
                        case ReturnType.Boolean:
                            return false;
                        case ReturnType.Undefined:
                            return Visibility.Collapsed;
                    }
                }
            }

            try
            {
                List<bool> bools = values.Cast<bool>().ToList();

                bool logcalExpression = false;
                switch (LogicalGate)
                {
                    case LogicalGate.Undefined:
                        throw new ArgumentException($"LogicalConverter undefined property: {nameof(LogicalGate)}");
                    case LogicalGate.And:
                        logcalExpression = bools.All(b => b);
                        break;
                    case LogicalGate.Nand:
                        logcalExpression = bools.All(b => !b);
                        break;
                    case LogicalGate.Or:
                        logcalExpression = bools.Any(b => b);
                        break;
                    case LogicalGate.Nor:
                        logcalExpression = bools.Any(b => !b);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }


                switch (ReturnType)
                {
                    case ReturnType.Boolean:
                        return logcalExpression;
                    case ReturnType.Visibility:
                        return logcalExpression ? Visibility.Visible : Visibility.Collapsed;
                    case ReturnType.Undefined:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception e)
            {
                throw new Exception("LogicalExpressionConverter : Something went wrong while converting:", e);
            }
            return null;

        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}