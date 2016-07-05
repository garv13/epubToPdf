using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.IO;
using VersFx.Formats.Text.Epub;
using VersFx.Formats.Text.Epub.Entities;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Globalization;
using System.Drawing;
using System.Windows;
using System.Drawing.Imaging;

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
            string text = "book title";
            string text1 = "author name";
            wrap_text(text, text1);        
        }
        public void wrap_text(string text, string text1)
        {
            Bitmap bmp = new Bitmap(600,900);
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                Rectangle ImageSize = new Rectangle(0, 0, 600, 900);
                graph.FillRectangle(System.Drawing.Brushes.White, ImageSize);
            }
           
            using (Graphics g = Graphics.FromImage(bmp))
            {
                RectangleF textRect = new RectangleF(50, 250, 500, 500);
                RectangleF textRect1 = new RectangleF(300, 700, 275, 200);
                Font d = new Font("Segeo UI", 50);
                Font d1 = new Font("Segeo UI", 30);
                g.DrawString(text, d, System.Drawing.Brushes.Red,textRect);
                g.DrawString(text1, d1, System.Drawing.Brushes.Green, textRect1);
                bmp.Save(@"C:\Users\garvj\Desktop\lol.jpeg", ImageFormat.Jpeg);             
            }
        }
    }

}

