using System;
using System.Windows.Data;
using System.Windows;

namespace Printinvest.Utils
{
    public class EnumToLocalizedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return string.Empty;

            var resourceKey = value.ToString();
            return Application.Current.TryFindResource(resourceKey) ?? resourceKey;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}