using System.ComponentModel;
namespace MyApp.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}