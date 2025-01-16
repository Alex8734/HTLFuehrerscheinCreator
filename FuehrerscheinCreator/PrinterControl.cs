using System.Collections.Generic;
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
using Image = System.Drawing.Image;
using Size = Avalonia.Size;
using System;

namespace FuehrerscheinCreator;

public static class PrinterControl
{
    public static void RenderToBmp(UserControl target, string path)
    {
        // Get the DPI settings of the target device
        using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
        {
            var dpiX = g.DpiX;
            var dpiY = g.DpiY;

            // Adjust the pixel size based on the DPI settings
            var pixelSize = new PixelSize((int)(target.Width * 2 * dpiX / 96), (int)(target.Height * 2 * dpiY / 96));
            var size = new Size(target.Width, target.Height);
            var pos = target.Bounds.Position;

            using (RenderTargetBitmap bitmap = new RenderTargetBitmap(pixelSize, new Vector(dpiX, dpiY)))
            {
                target.Measure(size);
                target.Arrange(new Rect(size));
                bitmap.Render(target);
                bitmap.Save(path);
                target.Arrange(new Rect(pos, size));
            }
        }
    }

    public static async Task Print(string path, string printerName)
    {
        using PrintDocument pd = new PrintDocument();

        pd.PrintPage += (sender, e) =>
        {
            using Image img = Image.FromFile(path);

            // Credit card size in pixels at 96 DPI
            int cardWidth = (int)(85.60 * 2 * 3.78);
            int cardHeight = (int)(53.98 * 2* 3.78);

            // Position the image at the top left corner
            int x = 0;
            int y = 0;

            // Draw the image
            e.Graphics.DrawImage(img, x, y, cardWidth, cardHeight);
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