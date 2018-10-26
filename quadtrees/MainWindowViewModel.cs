using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace quadtrees
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;



        public MainWindowViewModel()
        {


            GreenFormula = "x - y";
            BlueFormula = "y - x";
            RedFormula = "x * y";
            AlphaFormula = "0";

            GenerateImageCommand = new RelayCommand(GenerateImage, true);
        }

        public ICommand GenerateImageCommand { get; set; }
        public async void GenerateImage()
        {
            image = new WriteableBitmap(Width, Height, 96, 96, PixelFormats.Bgr32, null);

            ImageStatus = "loading...";

            for (var x = 0; x < image.Width; x++)
            {
                for (var y = 0; y < image.Height; y++)
                {
                    var pixelRect = GetPixelRect(x, y);
                    var pixelColors = await GetPixelColor(x, y);
                    image.WritePixels(pixelRect, pixelColors, 4, 0);
                }
            }

            ImageStatus = string.Empty;
            OnPropertyChanged(nameof(Image));
        }


        private int width;
        public int Width
        {
            get => width;
            set
            {
                width = value;
                OnPropertyChanged();
            }
        }

        private int height;
        public int Height
        {
            get => height;
            set
            {
                height = value;
                OnPropertyChanged();
            }
        }

        private WriteableBitmap image;
        public ImageSource Image
        {
            get => image;
        }

        private string imageStatus;
        public string ImageStatus
        {
            get => imageStatus;
            set
            {
                imageStatus = value;
                OnPropertyChanged();
            }
        }

        private string greenFormula;
        public string GreenFormula
        {
            get => greenFormula;
            set {
                greenFormula = value;
                GreenScript = CSharpScript.Create<long>(greenFormula, globalsType: typeof(PixelColorVariables)).CreateDelegate();
                OnPropertyChanged();
            }
        }
        public ScriptRunner<long> GreenScript { get; set; }

        private string blueFormula;
        public string  BlueFormula
        {
            get => blueFormula;
            set
            {
                blueFormula = value;
                BlueScript = CSharpScript.Create<long>(blueFormula, globalsType: typeof(PixelColorVariables)).CreateDelegate();
                OnPropertyChanged();
            }
        }

        public ScriptRunner<long> BlueScript { get; set; }

        private string redFormula;
        public string RedFormula
        {
            get => redFormula;
            set
            {
                redFormula = value;
                RedScript = CSharpScript.Create<long>(redFormula, globalsType: typeof(PixelColorVariables)).CreateDelegate();
                OnPropertyChanged();
            }
        }

        public ScriptRunner<long> RedScript { get; set; }

        private string alphaFormula;
        public string AlphaFormula
        {
            get => alphaFormula;
            set
            {
                alphaFormula = value;
                AlphaScript = CSharpScript.Create<long>(alphaFormula, globalsType: typeof(PixelColorVariables)).CreateDelegate();
                OnPropertyChanged();
            }
        }

        public ScriptRunner<long> AlphaScript { get; set; }

        protected void OnPropertyChanged([CallerMemberName] string callingMember = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(callingMember));
        }

        private Int32Rect GetPixelRect(int x, int y)
        {
            return new Int32Rect(x, y, 1, 1);
        }

        private async Task<byte[]> GetPixelColor(int x, int y)
        {
            var vars = new PixelColorVariables { x = x, y = y };
            return new byte[] {
                (byte)await GreenScript(vars),
                (byte)await BlueScript(vars),
                (byte)await RedScript(vars),
                (byte)await AlphaScript(vars)
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

}
