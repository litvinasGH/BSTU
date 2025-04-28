using System.ComponentModel;
using System.Collections.Generic;
using Printinvest.Models.Enums;

namespace Printinvest.Models
{
    public class Equipment : INotifyPropertyChanged
    {
        private string _model;
        private decimal _price;
        private Brand _brand;
        private List<ServiceType> _supportedServices;
        public string ImagePath { get; set; }

        public string Model
        {
            get => _model;
            set { _model = value; OnPropertyChanged(nameof(Model)); }
        }

        public decimal Price
        {
            get => _price;
            set { _price = value; OnPropertyChanged(nameof(Price)); }
        }

        public Brand Brand
        {
            get => _brand;
            set { _brand = value; OnPropertyChanged(nameof(Brand)); }
        }

        public List<ServiceType> SupportedServices
        {
            get => _supportedServices;
            set { _supportedServices = value; OnPropertyChanged(nameof(SupportedServices)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}