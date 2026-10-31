using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System;

public class LocalizationManager : INotifyPropertyChanged
{
    private ResourceDictionary _currentDictionary;

    public static LocalizationManager Instance { get; } = new LocalizationManager();

    public event PropertyChangedEventHandler PropertyChanged;

    public string this[string key] =>
        _currentDictionary?[key] as string ?? key;

    public void SetCulture(CultureInfo culture)
    {
        var dict = new ResourceDictionary
        {
            Source = new Uri($"/Resources/{culture.Name}/Strings.xaml", UriKind.Relative)
        };

        Application.Current.Resources.MergedDictionaries.Remove(_currentDictionary);
        Application.Current.Resources.MergedDictionaries.Add(dict);
        _currentDictionary = dict;

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
    }
}