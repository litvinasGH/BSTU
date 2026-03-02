using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;


namespace Printinvest.other
{
    public class AuthViewModel : INotifyPropertyChanged
    {
        private User _currentUser = new User();
        private ObservableCollection<User> _users; // Загружается из файла
        private string _selectedLanguage = "ru-RU";
        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                _selectedLanguage = value;
                OnPropertyChanged(nameof(SelectedLanguage));
                SwitchLanguage();
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand SwitchLanguageCommand { get; }

        public AuthViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            RegisterCommand = new RelayCommand(Register);
            SwitchLanguageCommand = new RelayCommand(SwitchLanguage);
        }

        private void Login(object obj)
        {
            // Проверка пользователя в коллекции
            var user = _users.FirstOrDefault(u => u.Email == CurrentUser.Email && u.Password == CurrentUser.Password);
            if (user != null)
            {
                // Открытие главного окна в зависимости от роли
            }
            else
            {
                MessageBox.Show("Ошибка входа!");
            }
        }

        public void Register(object obj)
        {
            // Открытие окна регистрации
            //var registrationWindow = new RegistrationWindow();
            //registrationWindow.Show();
        }

        private void SwitchLanguage(object parameter = null)
        {
            var culture = new CultureInfo(_selectedLanguage);
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;

            // Обновление ресурсов всех окон
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary { Source = new Uri($"/Resources.{_selectedLanguage}.xaml", UriKind.Relative) }
            );
        }

        // Реализация INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
