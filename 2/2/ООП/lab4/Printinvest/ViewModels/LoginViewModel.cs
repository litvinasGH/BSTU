using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Printinvest.Models;


namespace Printinvest.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _login;
        private string _password;
        private User _authenticatedUser;

        public string Login
        {
            get => _login;
            set { _login = value; OnPropertyChanged(); }
        }
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        public User AuthenticatedUser
        {
            get => _authenticatedUser;
            private set { _authenticatedUser = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }
        public ICommand CancelCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string prop = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(OnLogin);
            CancelCommand = new RelayCommand(OnCancel);
        }

        private void OnLogin(object obj)
        {
            // Demo auth: admin/admin; user/user
            if (Login == "admin" && Password == "admin")
            {
                AuthenticatedUser = new User { Login = "admin", Name = "Administrator", IsAdmin = true };
                CloseDialog(obj as Window, true);
            }
            else if (Login == "user" && Password == "user")
            {
                AuthenticatedUser = new User { Login = "user", Name = "Demo User", IsAdmin = false };
                CloseDialog(obj as Window, true);
            }
            else
            {
                MessageBox.Show("Invalid credentials", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void OnCancel(object obj)
        {
            CloseDialog(obj as Window, false);
        }

        private void CloseDialog(Window window, bool result)
        {
            if (window != null)
                window.DialogResult = result;
        }
    }
}