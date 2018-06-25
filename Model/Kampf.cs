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
        int[] werte = new int[3];

        public Kampf()
        {
            alleSpieler = new ObservableCollection<Geschoepf>();
            alleGegner = new ObservableCollection<Geschoepf>();
            new DBread(alleSpieler, alleGegner);
        }

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

            AktualisiereAlle();
        }

        public void FuegeCharakterHinzu(Geschoepf aGesch)
        {
            foreach(Geschoepf g in alleSpieler)
            {
                if(g.Name.Equals(aGesch.Name))
                {
                    spielerImKampf.Add(aGesch);
                    alleImKampf.Add(aGesch);
                }
            }

            AktualisiereAlle();
        }

        private void AktualisiereAlle()
        {
            maxAnzahl = alleImKampf.Count();
            alleImKampf.Sort((n1, n2) => n1.Initiative.CompareTo(n2.Initiative));
            PruefeAktuell();
        }

        public void PruefeAktuell()
        {
        
            AktuellerAngreifer = alleImKampf.ElementAt(index);
        }

        public void Wuerfeln()
        {
            int ergebnis = 0;
            int trefferzone = 0;

            Random rdm = new Random();
            int wert1 = rdm.Next(0, 10);
            int wert0 = rdm.Next(0, 10);

            ergebnis = wert1 * 10 + wert0;
            if (ergebnis == 0)
            {
                ergebnis = 100;
            }

            trefferzone = wert0 * 10 + wert1;
            if(trefferzone == 0)
            {
                trefferzone = 100;
            }

            int schaden = rdm.Next(1, 7);

            werte[0] = ergebnis;
            werte[1] = trefferzone;
            werte[2] = schaden;
        }

        public int WuerfelnW6()
        {
            int ergebnis;

            Random rdm = new Random();
            ergebnis = rdm.Next(1, 7);

            return ergebnis;
        }

        public void NaechsterSpieler()
        {
            if (index >= maxAnzahl)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            PruefeAktuell();
        }

        public void PruefeWaffe(Geschoepf verteidiger)
        {
            PruefeTreffer(verteidiger);
        }

        public void PruefeTreffer(Geschoepf verteidiger)
        {
            if (werte[0] == 1 || werte[0] == 2)
            {

            }
            else if (werte[0] == 99 || werte[0] == 100)
            {

            }
        }

    }
}
