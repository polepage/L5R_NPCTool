using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace NPC.Presenter.Windows.Converters
{

    [ContentProperty("Converters")]
    [ContentWrapper(typeof(List<IValueConverter>))]
    class StackConverter : IValueConverter
    {
        public List<IValueConverter> Converters { get; } = new List<IValueConverter>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result = value;
            foreach (IValueConverter converter in Converters)
            {
                result = converter.Convert(result, targetType, parameter, culture);
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result = value;
            foreach (IValueConverter converter in Converters)
            {
                result = converter.ConvertBack(result, targetType, parameter, culture);
            }

            return result;
        }
    }
}
