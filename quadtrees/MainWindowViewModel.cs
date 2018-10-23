using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string greenFormula;
        public string GreenFormula
        {
            get => greenFormula;
            set {
                greenFormula = value;
                OnPropertyChanged();    
            }
        }

        private string blueFormula;
        public string  BlueFormula
        {
            get => blueFormula;
            set
            {
                blueFormula = value;
                OnPropertyChanged();
            }
        }

        private string redFormula;
        public string RedFormula
        {
            get => redFormula;
            set
            {
                redFormula = value;
                OnPropertyChanged();
            }
        }

        private string alphaFormula;
        public string AlphaFormula
        {
            get => alphaFormula;
            set
            {
                alphaFormula = value;
                OnPropertyChanged();
            }
        }


        protected void OnPropertyChanged([CallerMemberName] string callingMember = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(callingMember));
        }
    }
}
