using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.IO;
using System.Windows;
using VersFx.Formats.Text.Epub;
using VersFx.Formats.Text.Epub.Entities;
using iTextSharp.text.html.simpleparser;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Globalization;

namespace epubToPdf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            FormattedText text = new FormattedText(
      "Hello World",
      CultureInfo.InvariantCulture,
      FlowDirection.LeftToRight,
      new Typeface("Segeo UI"),
      50,
      Brushes.Red);

            FormattedText text1 = new FormattedText(
     "By Axe Julius",
     CultureInfo.InvariantCulture,
     FlowDirection.LeftToRight,
     new Typeface("Segeo UI"),
     30,
     Brushes.Green);

            WriteTextToImage(textBox.Text,
                @"C:\Users\garvj\Desktop\lol.jpeg", text,text1,
    new Rect(0, 0, 850, 550),
    HorizontalAlignment.Center, VerticalAlignment.Center);
        }


      
        public static void WriteTextToImage(string inputFile, string outputFile, FormattedText text, FormattedText text1,
    Rect textRect, HorizontalAlignment hAlign, VerticalAlignment vAlign)
        {
            BitmapImage bitmap = new BitmapImage(new Uri(inputFile));
            DrawingVisual visual = new DrawingVisual();
            Point position = textRect.Location;

            switch (hAlign)
            {
                case HorizontalAlignment.Center:
                    position.X += (textRect.Width - text.Width) / 2;
                    break;
                case HorizontalAlignment.Right:
                    position.X += textRect.Width - text.Width;
                    break;
            }

            switch (vAlign)
            {
                case VerticalAlignment.Center:
                    position.Y += (textRect.Height - text.Height) / 2;
                    break;
                case VerticalAlignment.Bottom:
                    position.Y += textRect.Height - text.Height;
                    break;
            }

            using (DrawingContext dc = visual.RenderOpen())
            {
                dc.DrawImage(bitmap, new Rect(0, 0, bitmap.PixelWidth, bitmap.PixelHeight));
                dc.DrawText(text, position);
                position = textRect.Location;
                position.X += textRect.Width - text1.Width;
                position.Y += textRect.Height - text1.Height;
                dc.DrawText(text1, position);

            }

            RenderTargetBitmap target = new RenderTargetBitmap(bitmap.PixelWidth, bitmap.PixelHeight,
                                                               bitmap.DpiX, bitmap.DpiY, PixelFormats.Default);
            target.Render(visual);

            BitmapEncoder encoder = null;

            switch (Path.GetExtension(outputFile))
            {
                case ".png":
                    encoder = new PngBitmapEncoder();
                    break;
                case ".jpeg":
                    encoder = new JpegBitmapEncoder();
                    break;
            }

            if (encoder != null)
            {
                encoder.Frames.Add(BitmapFrame.Create(target));

                using (FileStream outputStream = new FileStream(outputFile, FileMode.Create))
                {
                    encoder.Save(outputStream);
                }
            }
        }
    }
}

