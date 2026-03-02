using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace lab4
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ResourceDictionary _currentLanguageDict;

        public static void SwitchLanguage(string cultureCode)
        {
            // Создаем URI для нужного словаря
            var dictUri = new Uri($"/Resources/{cultureCode}/dictionary.xaml", UriKind.Relative);

            // Загружаем словарь
            var newDict = new ResourceDictionary() { Source = dictUri };

            // Удаляем старый словарь
            if (_currentLanguageDict != null)
            {
                Current.Resources.MergedDictionaries.Remove(_currentLanguageDict);
            }

            // Добавляем новый и сохраняем ссылку
            Current.Resources.MergedDictionaries.Add(newDict);
            _currentLanguageDict = newDict;

            // Меняем культуру приложения
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);
        }

        // Загрузка языка по умолчанию
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SwitchLanguage("en-US"); // или "ru-RU"
        }
    }
}
