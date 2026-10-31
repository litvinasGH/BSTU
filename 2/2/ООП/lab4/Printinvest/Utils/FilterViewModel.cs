using Printinvest.Models.Enums;
using System;
using System.ComponentModel;

namespace Printinvest.ViewModels
{
    public class FilterViewModel : INotifyPropertyChanged
    {
        private ServiceType? _selectedServiceType;
        private Brand? _selectedBrand;
        private double _maxPrice = 100000;

        public event EventHandler FiltersChanged;

        public ServiceType? SelectedServiceType
        {
            get => _selectedServiceType;
            set
            {
                _selectedServiceType = value;
                OnPropertyChanged(nameof(SelectedServiceType));
                RaiseFiltersChanged();
            }
        }

        public Brand? SelectedBrand
        {
            get => _selectedBrand;
            set
            {
                _selectedBrand = value;
                OnPropertyChanged(nameof(SelectedBrand));
                RaiseFiltersChanged();
            }
        }

        public double MaxPrice
        {
            get => _maxPrice;
            set
            {
                _maxPrice = value;
                OnPropertyChanged(nameof(MaxPrice));
                RaiseFiltersChanged();
            }
        }

        private void RaiseFiltersChanged() => FiltersChanged?.Invoke(this, EventArgs.Empty);

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}