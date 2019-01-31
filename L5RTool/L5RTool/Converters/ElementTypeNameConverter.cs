using L5RTool.Elements;
using System;
using System.Globalization;
using System.Windows.Data;

namespace L5RTool.Converters
{
    class ElementTypeNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (ElementType)value;
            if (type == ElementType.Advantage_Disadvantage)
            {
                return "Advantage / Disadvantage";
            }
            else
            {
                return value.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
