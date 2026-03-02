using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Printinvest.Models;
using Printinvest.Models.Enums;
using Printinvest.Utils;
using Printinvest.Views;                        // Для окна деталей
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;         // Для CallerMemberName
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Printinvest.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly string _servicesPath = "Data/Services.json";
        private readonly string _equipmentPath = "Data/Equipment.json";

        private bool _isEquipmentMode;
        private string _searchQuery = string.Empty;
        private ServiceType _selectedServiceType = ServiceType.None;
        private Brand _selectedBrand = Brand.None;
        private decimal _maxPrice = 100000;

        private bool _isAdminUnlocked;
        public bool IsAdminUnlocked
        {
            get => _isAdminUnlocked;
            set { _isAdminUnlocked = value; OnPropertyChanged(); }
        }

        public ICommand UnlockAdminCommand { get; }
        public ICommand EditEquipmentCommand { get; }
        public ICommand DeleteEquipmentCommand { get; }

        public ObservableCollection<Service> Services { get; } = new ObservableCollection<Service>();
        public ObservableCollection<Equipment> Equipment { get; } = new ObservableCollection<Equipment>();
        public ICollectionView CurrentItems { get; private set; }

        public bool IsEquipmentMode
        {
            get => _isEquipmentMode;
            set
            {
                if (_isEquipmentMode == value) return;
                _isEquipmentMode = value;
                OnPropertyChanged();
                SwitchCollectionView();
            }
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (_searchQuery == value) return;
                _searchQuery = value;
                OnPropertyChanged();
                CurrentItems.Refresh();
            }
        }

        public ServiceType SelectedServiceType
        {
            get => _selectedServiceType;
            set
            {
                if (_selectedServiceType == value) return;
                _selectedServiceType = value;
                OnPropertyChanged();
                CurrentItems.Refresh();
            }
        }

        public Brand SelectedBrand
        {
            get => _selectedBrand;
            set
            {
                if (_selectedBrand == value) return;
                _selectedBrand = value;
                OnPropertyChanged();
                CurrentItems.Refresh();
            }
        }

        public decimal MaxPrice
        {
            get => _maxPrice;
            set
            {
                if (_maxPrice == value) return;
                _maxPrice = value;
                OnPropertyChanged();
                CurrentItems.Refresh();
            }
        }

        public ICommand SwitchToServicesCommand { get; }
        public ICommand SwitchToEquipmentCommand { get; }
        public ICommand OpenAdminPanelCommand { get; }
        public ICommand ShowEquipmentDetailsCommand { get; }
        public ICommand SwitchToEnglishCommand { get; }
        public ICommand SwitchToRussianCommand { get; }



        public MainViewModel()
        {
            // Инициализация коллекции по умолчанию
            _isEquipmentMode = true;
            CurrentItems = CollectionViewSource.GetDefaultView(Equipment);
            CurrentItems.Filter = FilterItems;

            

            SwitchToServicesCommand = new RelayCommand(_ => IsEquipmentMode = false);
            SwitchToEquipmentCommand = new RelayCommand(_ => IsEquipmentMode = true);
            OpenAdminPanelCommand = new RelayCommand(_ => OpenAdminPanel());
            ShowEquipmentDetailsCommand = new RelayCommand(obj => {
                if (obj is Equipment eq)
                {
                    var wnd = new EquipmentDetailsWindow { DataContext = new EquipmentDetailsWindowVM(eq, CurrentUser), Owner = Application.Current.MainWindow };
                    wnd.ShowDialog();
                    SaveEquipmentToJson();
                }
            });
            SwitchToEnglishCommand = new RelayCommand(_ => ChangeLanguage("en-US"));
            SwitchToRussianCommand = new RelayCommand(_ => ChangeLanguage("ru-RU"));



            UnlockAdminCommand = new RelayCommand(_ => ShowPasswordDialog());
            EditEquipmentCommand = new RelayCommand(obj => EditEquipment(obj as Equipment), obj => IsAdminUnlocked);
            DeleteEquipmentCommand = new RelayCommand(obj => DeleteEquipment(obj as Equipment), obj => IsAdminUnlocked);

            LoadData();
            
            SwitchCollectionView();

        }

        private void ChangeLanguage(string cultureCode)
        {
            // строим pack URI к словарю
            var dict = new ResourceDictionary
            {
                Source = new Uri(
                    $"pack://application:,,,/Printinvest;component/Localization/Resources.{cultureCode}.xaml",
                    UriKind.Absolute)
            };
            // удаляем старые словари локализации
            var merged = Application.Current.Resources.MergedDictionaries;
            // допустим, у вас в App.xaml уже лежит какой‑то общий словарь — его лучше не убирать.
            // поэтому удаляем только те, чьё имя начинается на "Resources."
            for (int i = merged.Count - 1; i >= 0; i--)
            {
                var md = merged[i];
                if (md.Source != null &&
                    md.Source.OriginalString.Contains("/Localization/Resources."))
                {
                    merged.RemoveAt(i);
                }
            }
            // и добавляем новый
            merged.Add(dict);
        }

        private User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            set { _currentUser = value; OnPropertyChanged(); }
        }


        private void ShowPasswordDialog()
        {
            if (CurrentUser != null) // уже авторизован
            {
                var profile = new ProfileWindow();
                profile.DataContext = new ProfileViewModel(CurrentUser);
                profile.ShowDialog();
            }
            else
            { // не авторизован
                var loginWindow = new LoginWindow();
                if (loginWindow.ShowDialog() == true)
                {
                    var vm = loginWindow.DataContext as LoginViewModel;
                    CurrentUser = vm.AuthenticatedUser;
                    if (CurrentUser.IsAdmin)
                        IsAdminUnlocked = true;
                }
            }
        }

        private void EditEquipment(Equipment eq)
        {
            if (eq == null) return;
            var vm = new EquipmentViewModel(eq);
            var editWnd = new EquipmentEditWindow
            {
                DataContext = vm,
                Owner = Application.Current.MainWindow
            };
            if (editWnd.ShowDialog() == true)
            {
                SaveState();
                vm.ApplyChanges();           // <-- переносим изменения в модель
                SaveEquipmentToJson();
                CurrentItems.Refresh();
            }
        }

        private void DeleteEquipment(Equipment eq)
        {
            if (eq == null) return;
            if (MessageBox.Show($"Удалить оборудование {eq.Model}?", "Подтвердите удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                SaveState();
                Equipment.Remove(eq);
                Data.SERDB.database.DeleteEquipment(eq.Id);
                SaveEquipmentToJson();
                CurrentItems.Refresh();
            }
        }

        private void SaveEquipmentToJson()
        {
            try
            {
                // Сохранение оборудования в базу данных
                Data.SERDB.database.SaveAllEquipment(Equipment.ToList());
            }
            catch (Exception ex)
            {
                // Обработка ошибок при сохранении данных
                MessageBox.Show($"Ошибка при сохранении оборудования: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SwitchCollectionView()
        {
            CurrentItems = CollectionViewSource.GetDefaultView(_isEquipmentMode ? (IEnumerable<object>)Equipment : Services);
            CurrentItems.Filter = FilterItems;
            CurrentItems.Refresh();
            OnPropertyChanged(nameof(CurrentItems));
        }

        private bool FilterItems(object item)
        {
            if (_isEquipmentMode && item is Equipment e)
                return e.Price <= _maxPrice
                       && (_selectedBrand == Brand.None || e.Brand == _selectedBrand)
                       && (_selectedServiceType == ServiceType.None || e.SupportedServices.Contains(_selectedServiceType))
                       && (string.IsNullOrEmpty(_searchQuery)
                           || (e.Model != null &&
                               e.Model.IndexOf(_searchQuery, StringComparison.OrdinalIgnoreCase) >= 0));

            if (!_isEquipmentMode && item is Service s)
                return s.Price <= _maxPrice
                       && (_selectedServiceType == ServiceType.None || s.Type == _selectedServiceType)
                       && (string.IsNullOrEmpty(_searchQuery)
                           || (s.Name != null &&
                               s.Name.IndexOf(_searchQuery, StringComparison.OrdinalIgnoreCase) >= 0));

            return false;
        }

        private void LoadData()
        {
            try
            {
                // Загрузка услуг из базы данных
                var services = Data.SERDB.database.GetServices();
                Services.Clear();
                foreach (var service in services)
                {
                    Services.Add(service);
                }

                // Загрузка оборудования из базы данных
                var equipment = Data.SERDB.database.GetEquipment();
                Equipment.Clear();
                foreach (var eq in equipment)
                {
                    Equipment.Add(eq);
                }

                // Обновление представления данных
                Application.Current.Dispatcher.Invoke(() => CurrentItems.Refresh());
            }
            catch (Exception ex)
            {
                // Обработка ошибок при загрузке данных
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenAdminPanel()
        {
            var win = new AdminWindow(CurrentUser) { Owner = Application.Current.MainWindow };
            if (win.ShowDialog() == true)
            {
                SaveState();
                // Перезагрузить список
                LoadData();
                CurrentItems.Refresh();
            }
        }



        Stack<ObservableCollection<Equipment>> _undoStack = new Stack<ObservableCollection<Equipment>>();
        Stack<ObservableCollection<Equipment>> _redoStack = new Stack<ObservableCollection<Equipment>>();
        public void Undo()
        {
            if (_undoStack.Count > 0)
            {
                var lastState = _undoStack.Pop();
                _redoStack.Push(new ObservableCollection<Equipment>(Equipment));
                Equipment.Clear();
                foreach (var item in lastState)
                    Equipment.Add(item);
                CurrentItems.Refresh();
            }
        }
        public void Redo()
        {
            if (_redoStack.Count > 0)
            {
                var lastState = _redoStack.Pop();
                _undoStack.Push(new ObservableCollection<Equipment>(Equipment));
                Equipment.Clear();
                foreach (var item in lastState)
                    Equipment.Add(item);
                CurrentItems.Refresh();
            }
        }

        public ICommand UndoCommand => new RelayCommand(_ => Undo(), _ => _undoStack.Count > 0);
        public ICommand RedoCommand => new RelayCommand(_ => Redo(), _ => _redoStack.Count > 0);
        public void SaveState()
        {
            _undoStack.Push(new ObservableCollection<Equipment>(Equipment));
            _redoStack.Clear();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
