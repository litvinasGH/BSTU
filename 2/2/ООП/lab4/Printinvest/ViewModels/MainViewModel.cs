using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Printinvest.Models;
using Printinvest.Models.Enums;
using Printinvest.Utils;
using Printinvest.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
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
        private string _searchQuery;
        private ServiceType _selectedServiceType;
        private Brand _selectedBrand;
        private decimal _maxPrice;

        public ObservableCollection<Service> Services { get; } = new ObservableCollection<Service>();
        public ObservableCollection<Equipment> Equipment { get; } = new ObservableCollection<Equipment>();

        public ICollectionView CurrentItems => _isEquipmentMode
            ? CollectionViewSource.GetDefaultView(Equipment)
            : CollectionViewSource.GetDefaultView(Services);

        public bool IsEquipmentMode
        {
            get => _isEquipmentMode;
            set
            {
                _isEquipmentMode = value;
                OnPropertyChanged(nameof(IsEquipmentMode));
                CurrentItems.Refresh();
            }
        }

        public ICommand SwitchToServicesCommand { get; }
        public ICommand SwitchToEquipmentCommand { get; }
        public ICommand OpenAdminPanelCommand { get; }



        public MainViewModel()
        {
            LoadData();

            SwitchToServicesCommand = new RelayCommand(_ => IsEquipmentMode = false);
            SwitchToEquipmentCommand = new RelayCommand(_ => IsEquipmentMode = true);
            OpenAdminPanelCommand = new RelayCommand(_ => OpenAdminPanel());

            CurrentItems.Filter = FilterItems;
        }

        private bool FilterItems(object item)
        {
            if (item is Service service)
                return service.Price <= _maxPrice &&
                       (_selectedServiceType == ServiceType.None || service.Type == _selectedServiceType) &&
                       (string.IsNullOrEmpty(_searchQuery) || service.Name.Contains(_searchQuery));

            if (item is Equipment equipment)
                return equipment.Price <= _maxPrice &&
                       (_selectedBrand == Brand.None || equipment.Brand == _selectedBrand) &&
                       (string.IsNullOrEmpty(_searchQuery) || equipment.Model.Contains(_searchQuery));

            return false;
        }

        private void LoadData()
        {
            if (File.Exists(_servicesPath))
            {
                var settings = new JsonSerializerSettings
                {
                    Converters = { new StringEnumConverter() } // Добавляем конвертер enum
                };

                var services = JsonConvert.DeserializeObject<List<Service>>(
                    File.ReadAllText(_servicesPath),
                    settings
                );
                Services.AddRange(services);
            }

            if (File.Exists(_equipmentPath))
            {
                var settings = new JsonSerializerSettings
                {
                    Converters = { new StringEnumConverter() } // Аналогично для оборудования
                };

                var equipment = JsonConvert.DeserializeObject<List<Equipment>>(
                    File.ReadAllText(_equipmentPath),
                    settings
                );
                Equipment.AddRange(equipment);
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                CurrentItems.Refresh();
            });
        }

        private void OpenAdminPanel()
        {
            var adminWindow = new AdminWindow();
            adminWindow.ShowDialog();
            LoadData(); // Перезагружаем данные после закрытия
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            !(bool)(value ?? false);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            !(bool)(value ?? false);
    }
}