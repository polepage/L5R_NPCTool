﻿using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace NPC.Presenter.Windows.Converters
{
    class NameToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = parameter as string;
            var uri = new StringBuilder("pack://application:,,,/NPC.Presenter.Windows;component/Icons/");

            if (!string.IsNullOrEmpty(path))
            {
                uri.Append(path);
                uri.Append("/");
            }

            uri.Append(value.ToString());
            uri.Append(".png");

            return uri.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
