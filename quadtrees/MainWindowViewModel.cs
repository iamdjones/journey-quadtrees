using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace quadtrees
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            GreenFormula = "x - y";
            BlueFormula = "y - x";
            RedFormula = "x * y";
            AlphaFormula = "0";

            GenerateImageCommand = new RelayCommand(GenerateImage, true);
        }

        public ICommand GenerateImageCommand { get; set; }
        public void GenerateImage()
        {

            OnPropertyChanged(nameof(Image));
        }

        private WriteableBitmap image;
        public ImageSource Image
        {
            get => image;
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
    }

}
