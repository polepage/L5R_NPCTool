using L5RTool.Elements;
using System;
using System.Globalization;
using System.Windows.Data;

namespace L5RTool.Converters
{
    class ElementTypeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "\\Icons\\" + value.ToString() + ".png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
