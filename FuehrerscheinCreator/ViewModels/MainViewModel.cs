using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace FuehrerscheinCreator.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private string _lastName;
    private string _firstName;
    private string _birthday;
    private string _selectedPrinter;
    private string _selectedSource = "https://www.shareicon.net/data/2015/05/07/34779_man_400x400.png";

    public string SelectedSource
    {
        get => string.IsNullOrWhiteSpace(_selectedSource)
            ? "https://www.shareicon.net/data/2015/05/07/34779_man_400x400.png"
            : _selectedSource;
        set
        {
            _selectedSource = value;
            OnPropertyChanged(nameof(SelectedSource));
        }
    }



    public string SelectedPrinter
    {
        get => string.IsNullOrWhiteSpace(_selectedPrinter) ? new PrinterSettings().PrinterName : _selectedPrinter;
        set
        {
            _selectedPrinter = value;
            OnPropertyChanged(nameof(SelectedPrinter));
        }
    }

    public List<string> Printers => PrinterSettings.InstalledPrinters.Select(p => p.ToString()).ToList();

    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            OnPropertyChanged(nameof(LastName));
        }
    }

    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }

    public string Birthday
    {
        get => _birthday;
        set
        {
            _birthday = value;
            OnPropertyChanged(nameof(Birthday));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}