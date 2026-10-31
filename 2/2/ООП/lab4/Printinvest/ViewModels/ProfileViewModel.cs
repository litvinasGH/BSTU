using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Printinvest.Models;

using System.Windows;
using System.Globalization;
using System.Linq;
using System;

namespace Printinvest.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private User _user;
        public User User
        {
            get => _user;
            set { _user = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }
        public ICommand SwitchLanguageCommand { get; }
        public ICommand ToggleThemeCommand { get; }

        public ProfileViewModel(User user)
        {
            User = user;
            SaveCommand = new RelayCommand(OnSave);
            SwitchLanguageCommand = new RelayCommand(OnSwitchLanguage);
            ToggleThemeCommand = new RelayCommand(OnToggleTheme);
        }

        private void OnSave(object obj)
        {
            MessageBox.Show("Changes saved!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnSwitchLanguage(object obj)
        {
            var culture = new CultureInfo(obj?.ToString());
            CultureInfo.CurrentUICulture = culture;
            // перезагрузить словари:
            Application.Current.Resources.MergedDictionaries
                .Remove(Application.Current.Resources.MergedDictionaries.FirstOrDefault(d => d.Source.OriginalString.StartsWith("Localization/Resources.")));
            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary { Source = new Uri($"Localization/Resources.{obj?.ToString()}.xaml", UriKind.Relative) });
        }

        private void OnToggleTheme(object obj)
        {
            var theme = obj?.ToString();
            // убрать старую тему
            var old = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source.OriginalString.StartsWith("Themes/"));
            if (old != null) Application.Current.Resources.MergedDictionaries.Remove(old);
            // добавить новую
            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary { Source = new Uri($"Themes/{theme}.xaml", UriKind.Relative) });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
