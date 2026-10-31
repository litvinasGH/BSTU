using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Printinvest.Models;
using Printinvest.Models.Enums;
using Printinvest.Utils;
using Printinvest.Views;

namespace Printinvest.ViewModels
{
    public class AdminPanelViewModel : INotifyPropertyChanged
    {
        private const string Path = "Data/Equipment.json";

        public List<Brand> AvailableBrands { get; }
        public ICommand BrowseImageCommand { get; }
        public ICommand AddCommand { get; }

        public Brand Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string SupportedServicesText { get; set; }
        public string ImagePath { get; set; }

        public AdminPanelViewModel()
        {
            AvailableBrands = ((Brand[])Enum.GetValues(typeof(Brand))).ToList();
            Brand = AvailableBrands.FirstOrDefault();

            BrowseImageCommand = new RelayCommand(_ => {
                var dlg = new OpenFileDialog { Filter = "Image|*.png;*.jpg;*.jpeg" };
                if (dlg.ShowDialog() == true)
                {
                    // копируем файл в Assets/Images/
                    var dest = System.IO.Path.Combine("Assets/Images", System.IO.Path.GetFileName(dlg.FileName));
                    File.Copy(dlg.FileName, dest, true);
                    ImagePath = dest;
                    OnPropertyChanged(nameof(ImagePath));
                }
            });

            AddCommand = new RelayCommand(_ => {
                try
                {
                    // читаем
                    var list = File.Exists(Path)
                        ? JsonConvert.DeserializeObject<List<Equipment>>(File.ReadAllText(Path), new JsonSerializerSettings { Converters = { new StringEnumConverter() } })
                        : new List<Equipment>();

                    var services = (SupportedServicesText ?? "")
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => (ServiceType)Enum.Parse(typeof(ServiceType), s.Trim()))
                        .ToList();

                    list.Add(new Equipment { Brand = Brand, Model = Model, Price = Price, SupportedServices = services, ImagePath = ImagePath });

                    File.WriteAllText(Path, JsonConvert.SerializeObject(list, Formatting.Indented, new StringEnumConverter()));

                    // закрываем окно
                    var win = Application.Current.Windows
                              .OfType<AdminPanelWindow>()
                              .FirstOrDefault();
                    if (win != null)
                        win.DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string p = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }
}