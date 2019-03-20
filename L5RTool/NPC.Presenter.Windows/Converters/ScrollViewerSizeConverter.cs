using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NPC.Presenter.Windows.Converters
{
    class ScrollViewerSizeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2)
            {
                throw new ArgumentException("ScrollViewerWidthConverter: Bad number of parameters.");
            }

            double requestSize = (double)values[0];
            var scrollbarVisibility = (Visibility)values[1];
            
            if (scrollbarVisibility == Visibility.Visible)
            {
                double scrollbarSize = (double)parameter;
                return requestSize - scrollbarSize;
            }

            return requestSize;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
