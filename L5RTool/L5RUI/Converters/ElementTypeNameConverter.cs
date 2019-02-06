using NPC.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace L5RUI.Converters
{
    class ElementTypeNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (ElementType)value;
            if (type == ElementType.Trait)
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
