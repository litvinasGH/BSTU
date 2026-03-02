using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System;

namespace Printinvest.Utils
{
    public class IsAtMaxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double val && parameter is Slider slider)
                return val >= slider.Maximum;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}