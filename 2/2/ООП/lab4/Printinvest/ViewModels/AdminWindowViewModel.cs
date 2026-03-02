using Printinvest.Data;
using Printinvest.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace Printinvest.ViewModels
{
    public class AdminWindowViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseManager _dbManager;
        private ObservableCollection<Equipment> _equipment;
        private ObservableCollection<Service> _services;
        private ObservableCollection<User> _users;
        private ObservableCollection<Comment> _comments;
        private string _selectedTable;

        public AdminWindowViewModel(User currentUser)
        {
            if (currentUser == null || !currentUser.IsAdmin)
                throw new UnauthorizedAccessException("Доступ только для администраторов.");

            _dbManager = new DatabaseManager();
            _equipment = new ObservableCollection<Equipment>(_dbManager.GetEquipment());
            _services = new ObservableCollection<Service>(_dbManager.GetServices());
            _users = new ObservableCollection<User>(_dbManager.GetUsers());
            _comments = new ObservableCollection<Comment>();
            _selectedTable = "Equipment";

            EquipmentView = CollectionViewSource.GetDefaultView(_equipment);
            ServicesView = CollectionViewSource.GetDefaultView(_services);
            UsersView = CollectionViewSource.GetDefaultView(_users);
            CommentsView = CollectionViewSource.GetDefaultView(_comments);

            SwitchTableCommand = new RelayCommand(SwitchTable);
            AddEquipmentCommand = new RelayCommand(AddEquipment);
            UpdateEquipmentCommand = new RelayCommand(UpdateEquipment);
            DeleteEquipmentCommand = new RelayCommand(DeleteEquipment);
            AddServiceCommand = new RelayCommand(AddService);
            UpdateServiceCommand = new RelayCommand(UpdateService);
            DeleteServiceCommand = new RelayCommand(DeleteService);
            AddUserCommand = new RelayCommand(AddUser);
            UpdateUserCommand = new RelayCommand(UpdateUser);
            DeleteUserCommand = new RelayCommand(DeleteUser);
        }

        public ObservableCollection<Equipment> Equipment
        {
            get => _equipment;
            set { _equipment = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Service> Services
        {
            get => _services;
            set { _services = value; OnPropertyChanged(); }
        }

        public ObservableCollection<User> Users
        {
            get => _users;
            set { _users = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Comment> Comments
        {
            get => _comments;
            set { _comments = value; OnPropertyChanged(); }
        }

        public string SelectedTable
        {
            get => _selectedTable;
            set
            {
                _selectedTable = value;
                UpdateCommentsIfNeeded();
                OnPropertyChanged();
            }
        }

        public ICollectionView EquipmentView { get; }
        public ICollectionView ServicesView { get; }
        public ICollectionView UsersView { get; }
        public ICollectionView CommentsView { get; }

        public ICommand SwitchTableCommand { get; }
        public ICommand AddEquipmentCommand { get; }
        public ICommand UpdateEquipmentCommand { get; }
        public ICommand DeleteEquipmentCommand { get; }
        public ICommand AddServiceCommand { get; }
        public ICommand UpdateServiceCommand { get; }
        public ICommand DeleteServiceCommand { get; }
        public ICommand AddUserCommand { get; }
        public ICommand UpdateUserCommand { get; }
        public ICommand DeleteUserCommand { get; }

        private void SwitchTable(object parameter)
        {
            SelectedTable = parameter.ToString();
        }

        private void AddEquipment(object parameter)
        {
            var newEquipment = new Equipment { Model = "Новое оборудование", Price = 0 };
            _dbManager.AddEquipment(newEquipment);
            Equipment.Add(newEquipment);
        }

        private void UpdateEquipment(object parameter)
        {
            if (parameter is Equipment equipment)
            {
                _dbManager.UpdateEquipment(equipment);
                RefreshEquipment();
            }
        }

        private void DeleteEquipment(object parameter)
        {
            if (parameter is Equipment equipment)
            {
                _dbManager.DeleteEquipment(equipment.Id);
                Equipment.Remove(equipment);
            }
        }

        private void AddService(object parameter)
        {
            var newService = new Service { Name = "Новая услуга", Price = 0 };
            _dbManager.AddService(newService);
            Services.Add(newService);
        }

        private void UpdateService(object parameter)
        {
            if (parameter is Service service)
            {
                _dbManager.UpdateService(service);
                RefreshServices();
            }
        }

        private void DeleteService(object parameter)
        {
            if (parameter is Service service)
            {
                _dbManager.DeleteService(service.Id);
                Services.Remove(service);
            }
        }

        private void AddUser(object parameter)
        {
            var newUser = new User { Login = "newuser", Password = "password", Name = "Новый пользователь", IsAdmin = false };
            _dbManager.AddUser(newUser);
            Users.Add(newUser);
        }

        private void UpdateUser(object parameter)
        {
            if (parameter is User user)
            {
                _dbManager.UpdateUser(user);
                RefreshUsers();
            }
        }

        private void DeleteUser(object parameter)
        {
            if (parameter is User user)
            {
                _dbManager.DeleteUser(user.Id);
                Users.Remove(user);
            }
        }

        private void RefreshEquipment()
        {
            Equipment.Clear();
            foreach (var eq in _dbManager.GetEquipment())
                Equipment.Add(eq);
        }

        private void RefreshServices()
        {
            Services.Clear();
            foreach (var service in _dbManager.GetServices())
                Services.Add(service);
        }

        private void RefreshUsers()
        {
            Users.Clear();
            foreach (var user in _dbManager.GetUsers())
                Users.Add(user);
        }

        private void UpdateCommentsIfNeeded()
        {
            if (SelectedTable == "Comments")
            {
                Comments.Clear();
                foreach (var equipment in Equipment)
                {
                    if (equipment.Comments != null)
                    {
                        foreach (var comment in equipment.Comments)
                            Comments.Add(comment);
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

  
}