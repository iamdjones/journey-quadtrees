using Microsoft.CodeAnalysis.CSharp.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace quadtrees
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WriteableBitmap bitmap;
        private MainWindowViewModel viewModel = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();

            DataContext = viewModel;

            bitmap = new WriteableBitmap((int)image.Width, (int)image.Height, 96, 96, PixelFormats.Bgr32, null);
            image.Source = bitmap;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private async void image_Loaded(object sender, RoutedEventArgs e)
        {
            for (var x = 0; x < image.Width; x++)
            {
                for (var y = 0; y < image.Height; y++)
                {
                    var pixelRect = GetPixelRect(x, y);
                    var pixelColors = await GetPixelColor(x, y);
                    bitmap.WritePixels(pixelRect, pixelColors, 4, 0);
                }
            }
        }


        private Int32Rect GetPixelRect(int x, int y)
        {
            return new Int32Rect(x, y, 1, 1);
        }

        private async Task<byte[]> GetPixelColor(int x, int y)
        {
            var vars = new PixelColorVariables { x = x, y = y };
            return new byte[] {
                (byte)await viewModel.GreenScript(vars),
                (byte)await viewModel.BlueScript(vars),
                (byte)await viewModel.RedScript(vars),
                (byte)await viewModel.AlphaScript(vars)
            };
        }

        #region Sugar
        private Int32Rect GetPixelRect(double x, double y)
        {
            return GetPixelRect((int)x, (int)y);
        }

        private async Task<byte[]> GetPixelColor(double x, double y)
        {
            return await GetPixelColor((int)x, (int)y);
        }
        #endregion
    }

    public class PixelColorVariables
    {
        public int x;
        public int y;
    }
}
