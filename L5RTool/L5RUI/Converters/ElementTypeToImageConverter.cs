using System;
using System.Globalization;
using System.Windows.Data;

namespace L5RUI.Converters
{
    class ElementTypeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "pack://application:,,,/L5RUI;component/Icons/Elements/" + value.ToString() + ".png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
