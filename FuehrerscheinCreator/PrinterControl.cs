﻿using System.Collections.Generic;
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
    public static Bitmap RenderToBmp(UserControl target, string path)
    {
        var pixelSize = new PixelSize((int) target.Width , (int) target.Height );
        var size = new Size(target.Width, target.Height);
        var pos = target.Bounds.Position;
        Bitmap bpm;
        using (RenderTargetBitmap bitmap = new RenderTargetBitmap(pixelSize, new Vector(90, 90)))
        {
            target.Measure(size);
            target.Arrange(new Rect(size));
            bitmap.Render(target);
            bitmap.Save(path);
            target.Arrange(new Rect(pos, size));
            bpm = bitmap;
        }

        return bpm;
    }

    public static async Task Print(string path, string printerName)
    {
        using PrintDocument pd = new PrintDocument();

        pd.PrintPage += (sender, e) =>
        {
            using Image img = Image.FromFile(path);


            // Credit card size in pixels at 96 DPI

            // Position the image at the top left corner
            int x = 0;
            int y = 0;

            // Draw the image
            e.Graphics.DrawImageUnscaledAndClipped(img,new Rectangle(new Point(0,0), new System.Drawing.Size(img.Width,img.Height)));
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