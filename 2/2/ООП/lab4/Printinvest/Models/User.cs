using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Printinvest.Models { 
    public class User : INotifyPropertyChanged
    {
        private string _login;
        private string _password;
        private string _name;
        private bool _isAdmin;
        private Uri _photoUri;

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

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public bool IsAdmin
        {
            get => _isAdmin;
            set { _isAdmin = value; OnPropertyChanged(); }
        }

        public Uri PhotoUri
        {
            get => _photoUri;
            set { _photoUri = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}