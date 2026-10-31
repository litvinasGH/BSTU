using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Printinvest.Utils
{
    public class ImageCoverPathConverter : IValueConverter
    {
        // Путь к default_cover.png внутри сборки:
        private static readonly Uri _defaultCoverUri =
            new Uri("pack://application:,,,/Printinvest;component/Assets/default_cover.png",
                    UriKind.Absolute);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Если в модели путь задан
            if (value is string relativePath && !string.IsNullOrWhiteSpace(relativePath))
            {
                // Пробуем посмотреть на файл по абсолютному пути: exe_directory + относительный путь из JSON
                var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
                if (File.Exists(fullPath))
                {
                    try
                    {
                        var bmp = new BitmapImage();
                        bmp.BeginInit();
                        bmp.UriSource = new Uri(fullPath, UriKind.Absolute);
                        bmp.CacheOption = BitmapCacheOption.OnLoad;
                        bmp.EndInit();
                        return bmp;
                    }
                    catch
                    {
                        // если чтение упало — идём к дефолту
                    }
                }
            }

            // fallback: показываем default_cover.png из ресурсов сборки
            return new BitmapImage(_defaultCoverUri);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
