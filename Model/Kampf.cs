using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Model
{
    public class Kampf : INotifyPropertyChanged
    {
        ObservableCollection<Geschoepf> alleGegner = null;
        ObservableCollection<Geschoepf> alleSpieler = null;
        ObservableCollection<Geschoepf> gegnerImKampf = null;
        ObservableCollection<Geschoepf> spielerImKampf = null;

        List<Geschoepf> alleImKampf = null;

        Geschoepf aktuellerAngreifer;
        Int32 index = 0;
        Int32 maxAnzahl = 0;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        public Geschoepf AktuellerAngreifer
        {
            get
            {
                return aktuellerAngreifer;
            }

            set
            {
                aktuellerAngreifer = value;
                OnPropertyChanged(new PropertyChangedEventArgs("aktAngr"));
            }
        }

        public void FuegeGegnerHinzu(Geschoepf aGesch)
        {
            foreach(Geschoepf g in alleGegner)
            {
                if(g.Name.Equals(aGesch.Name))
                {
                    gegnerImKampf.Add(aGesch);
                    alleImKampf.Add(aGesch);
                }
            }

            maxAnzahl = alleImKampf.Count();
            alleImKampf.Sort((n1, n2) => n1.Initiative.CompareTo(n2.Initiative));
            

            PruefeAktuell();
        }


        public void PruefeAktuell()
        {
            AktuellerAngreifer = alleImKampf.ElementAt(index);
            Mark();
        }

        public void Mark()
        {

        }

    }
}
