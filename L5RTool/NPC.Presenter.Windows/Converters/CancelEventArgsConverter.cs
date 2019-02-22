using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace NPC.Presenter.Windows.Converters
{
    class CancelEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CancelEventArgs args)
            {
                return new Action(() => args.Cancel = true);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
