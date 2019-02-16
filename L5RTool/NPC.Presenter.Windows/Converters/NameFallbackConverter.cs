using System;
using System.Globalization;
using System.Windows.Data;

namespace NPC.Presenter.Windows.Converters
{
    class NameFallbackConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (object obj in values)
            {
                string textValue = obj?.ToString();
                if (!string.IsNullOrEmpty(textValue))
                {
                    return textValue;
                }
            }

            return "";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
