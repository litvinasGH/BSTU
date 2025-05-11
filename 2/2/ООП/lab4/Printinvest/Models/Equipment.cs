using System.ComponentModel;
using System.Collections.Generic;
using Printinvest.Models.Enums;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Printinvest.Models
{
    public class Equipment : INotifyPropertyChanged
    {
        private string _model;
        private decimal _price;
        private Brand _brand;
        private ObservableCollection<Comment> _comments;
        private List<ServiceType> _supportedServices;
        public string ImagePath { get; set; }

        public Equipment()
        {
            // Инициализируем и подписываемся на изменение
            _comments = new ObservableCollection<Comment>();
            _comments.CollectionChanged += Comments_CollectionChanged;
        }

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

        // Свойство Comments
        public ObservableCollection<Comment> Comments
        {
            get => _comments;
            set
            {
                if (_comments != null)
                    _comments.CollectionChanged -= Comments_CollectionChanged;
                _comments = value;
                if (_comments != null)
                    _comments.CollectionChanged += Comments_CollectionChanged;
                OnPropertyChanged(nameof(Comments));
            }
        }

        // Обработчик изменения коллекции
        private void Comments_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnCommentsChanged(e);
        }

        /// <summary>
        /// Вызывается при любом изменении коллекции Comments
        /// </summary>
        protected virtual void OnCommentsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

        }
    }
}