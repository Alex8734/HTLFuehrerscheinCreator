using System.Collections.Generic;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using System.Drawing.Printing;
using System.Drawing;
using System.Threading.Tasks;
using MsBox.Avalonia;
using MsBox.Avalonia.Dto;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia.Models;
using Bitmap = Avalonia.Media.Imaging.Bitmap;
using Image = System.Drawing.Image;
using Point = System.Drawing.Point;
using Size = Avalonia.Size;

namespace FuehrerscheinCreator;

public static class PrinterControl
{
    public static Bitmap RenderToBmp(UserControl target, string path, double dpi)
    {
        // Convert width and height to pixels based on DPI
        var pixelWidth = (int)(target.Width * dpi / 96.0);
        var pixelHeight = (int)(target.Height * dpi / 96.0);

        var pixelSize = new PixelSize(pixelWidth, pixelHeight);
        var size = new Size(target.Width, target.Height);
        var originalBounds = target.Bounds;

        Bitmap resultBitmap;

        using (var bitmap = new RenderTargetBitmap(pixelSize, new Vector(dpi, dpi)))
        {
            // Prepare the target for rendering
            target.Measure(size);
            target.Arrange(new Rect(size));

            // Render to the bitmap
            bitmap.Render(target);
            bitmap.Save(path); // Save to the specified path
            resultBitmap = bitmap;
            // Create a copy to return
        }

        // Restore original bounds
        target.Arrange(originalBounds);

        return resultBitmap;
    }

    public static async Task Print(string path, string printerName)
    {
        using PrintDocument pd = new PrintDocument();


        pd.PrintPage += (sender, e) =>
        {
            using Image img = Image.FromFile(path);

            // Draw the image
            e.Graphics.DrawImage(img,0,0);

        };
        var printers = PrinterSettings.InstalledPrinters;
        if (printers.Count == 0)
        {
            var box = MessageBoxManager
                .GetMessageBoxStandard("Caption", "No printer found",
                    ButtonEnum.YesNo);

            var result = await box.ShowAsync();
            return;
        }

        var msgPrams = new MessageBoxCustomParams
        {
            ButtonDefinitions = new List<ButtonDefinition>
            {
                new() {Name = "Print", IsDefault = true },
                new() {Name = "Cancel", IsCancel = true}
            },
            ContentTitle = "Print",
            ContentMessage = "You want to print?",
        };

        var printBox = MessageBoxManager
            .GetMessageBoxCustom(msgPrams);

        var printResult = await printBox.ShowAsync();
        if (printResult == "Cancel")
        {
            return;
        }
        pd.PrinterSettings = new PrinterSettings
        {
            PrinterName = printerName,
        };
        pd.Print();
    }
}