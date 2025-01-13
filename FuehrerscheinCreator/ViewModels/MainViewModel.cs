using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Linq;

namespace FuehrerscheinCreator.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private string _lastName;
    private string _firstName;
    private string _birthday;
    private string _selectedPrinter;
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