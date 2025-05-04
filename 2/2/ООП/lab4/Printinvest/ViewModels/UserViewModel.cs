using System.Windows.Input;
using System.Windows;
using System;
using Printinvest.Models;

namespace MyApp.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            set { _currentUser = value; OnPropertyChanged(nameof(CurrentUser)); }
        }

        public ICommand SaveProfileCommand { get; }
        public ICommand ChangeLanguageCommand { get; }
        public ICommand ChangeThemeCommand { get; }

        public UserViewModel(User user)
        {
            CurrentUser = user;
            SaveProfileCommand = new RelayCommand(_ => SaveProfile());
            ChangeLanguageCommand = new RelayCommand(lang => ApplyLanguage(lang.ToString()));
            ChangeThemeCommand = new RelayCommand(theme => ApplyTheme(theme.ToString()));
        }

        private void SaveProfile()
        {
            // логика сохранения: запись в файл, БД и т.д.
            MessageBox.Show("");
        }
        private void ApplyLanguage(string lang)
        {
            var dict = new ResourceDictionary { Source = new Uri($"Resources/Strings.{lang}.xaml", UriKind.Relative) };
            Application.Current.Resources.MergedDictionaries[1] = dict;
        }
        private void ApplyTheme(string theme)
        {
            var dict = new ResourceDictionary { Source = new Uri($"Themes/{theme}Theme.xaml", UriKind.Relative) };
            Application.Current.Resources.MergedDictionaries[0] = dict;
        }
    }

}