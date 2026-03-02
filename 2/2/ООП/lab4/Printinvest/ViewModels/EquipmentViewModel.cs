using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Printinvest.Models;
using Printinvest.Models.Enums;

namespace Printinvest.ViewModels
{
    public class EquipmentViewModel : INotifyPropertyChanged
    {
        private readonly Equipment _equipment;
        private string _model;
        private decimal _price;
        private Brand _brand;

        public EquipmentViewModel(Equipment equipment)
        {
            _equipment = equipment;
            _model = equipment.Model;
            _price = equipment.Price;
            _brand = equipment.Brand;

            AvailableBrands = new List<Brand>((Brand[])Enum.GetValues(typeof(Brand)));
        }

        public string Model
        {
            get => _model;
            set
            {
                if (_model == value) return;
                _model = value;
                OnPropertyChanged();
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                if (_price == value) return;
                _price = value;
                OnPropertyChanged();
            }
        }

        public Brand Brand
        {
            get => _brand;
            set
            {
                if (_brand == value) return;
                _brand = value;
                OnPropertyChanged();
            }
        }

        public List<Brand> AvailableBrands { get; }

        /// <summary>
        /// Применяет изменения обратно к оригинальной модели
        /// </summary>
        public void ApplyChanges()
        {
            _equipment.Model = Model;
            _equipment.Price = Price;
            _equipment.Brand = Brand;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}