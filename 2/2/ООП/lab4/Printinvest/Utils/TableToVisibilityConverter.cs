﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Printinvest.Converters
{
    public class TableToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string selectedTable && parameter is string expectedTable)
            {
                return selectedTable == expectedTable ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}