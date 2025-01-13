using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using FuehrerscheinCreator.ViewModels;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace FuehrerscheinCreator;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private async void PrintBtnClick(object? sender, RoutedEventArgs e)
    {
        var preview = this.FindControl<Preview>("Preview");
        if (preview == null)
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Caption", "Preview not found",
                    ButtonEnum.YesNo);

            var result = await box.ShowAsync();
            return;
        }
        PrinterControl.RenderToBmp(preview, "Fuehrerschein.bmp");
        PrinterControl.Print("Fuehrerschein.bmp", (DataContext as MainViewModel)?.SelectedPrinter);

    }
}