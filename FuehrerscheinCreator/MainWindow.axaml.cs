using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Styling;
using FuehrerscheinCreator.ViewModels;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using System.Collections.Generic;

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

    private async void OnAddImageClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            AllowMultiple = false,
            Title = "Select an Image",
            Filters = new List<FileDialogFilter>
            {
                new FileDialogFilter { Name = "Image Files", Extensions = new List<string> { "png", "jpg", "jpeg", "bmp", "gif" } }
            }
        };


        var result = await openFileDialog.ShowAsync(this);
        if (result != null && result.Length > 0)
        {
            var preview = this.FindControl<Preview>("Preview");

            var imagePath = result[0];
            preview.Selected = new Bitmap(imagePath);

        }
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