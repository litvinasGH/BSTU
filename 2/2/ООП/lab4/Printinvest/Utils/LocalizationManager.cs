using System;
using System.Globalization;
using System.Windows;

namespace Printinvest.Utils
{
    public static class LocalizationManager
    {
        public static void SetCulture(CultureInfo culture)
        {
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary
                {
                    Source = new Uri($"/Localization/Resources.{culture.Name}.xaml", UriKind.Relative)
                });
        }
    }
}