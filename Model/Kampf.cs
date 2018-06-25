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
        ObservableCollection<String> protokollSheet = null;

        List<Geschoepf> alleImKampf = null;

        Geschoepf aktuellerAngreifer;
        Geschoepf aktuellerGegner;
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

        public ObservableCollection<string> ProtokollSheet
        {
            get
            {
                return protokollSheet;
            }

            set
            {
                protokollSheet = value;
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

        private Boolean Schadensberechnung(Geschoepf angreifer, Geschoepf ziel)
        {
            ziel.AktuelleLP -= angreifer.Staerke + angreifer.Waffenbonus + werte[2] - ziel.Widerstand - ziel.Ruestungsbonus;

            if (ziel.Lebenspunkte > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Angriff()
        {
            if(aktuellerAngreifer.Kampfgeschick > werte[1])
            {
                if(Schadensberechnung(AktuellerAngreifer, aktuellerGegner))
                {
                    EntferneAusListe();
                    SchreibeProtokoll(true);
                }
                else
                {
                    SchreibeProtokoll(false);
                }
            }
            else
            {
                String protokoll = String.Format("{0} {1} {2}", AktuellerAngreifer.Name, "misslingt der Angriff auf", aktuellerGegner.Name);
            }
        }

        private void EntferneAusListe()
        {
            alleImKampf.Remove(aktuellerGegner);
            if(spielerImKampf.Contains(aktuellerGegner))
            {
                spielerImKampf.Remove(aktuellerGegner);
            }
            else
            {
                gegnerImKampf.Remove(aktuellerGegner);
            }
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
            else
            {

            }
        }

        private void SchreibeProtokoll(Boolean tot)
        {
            int zone = werte[1];
            String protokoll;
            if(tot)
            {
                // 1-15 Kopf - 16-35 r. Arm - 36-55 l. Arm - 56-80 Torso - 81-90 r. Bein - 91-100 l. Bein
                if(zone > 0 && zone <16)
                {
                    protokoll = String.Format("{0} {1} {2}", AktuellerAngreifer.Name, "zerteilt den Kopf von", aktuellerGegner.Name);
                }
                else if(zone > 15 && zone < 36)
                {
                    protokoll = String.Format("{0} {1} {2}", AktuellerAngreifer.Name, "zerteilt den Kopf von", aktuellerGegner.Name);
                }
                else if(zone > 35 && zone < 56)
                {
                    protokoll = String.Format("{0} {1} {2}", AktuellerAngreifer.Name, "zerteilt den Kopf von", aktuellerGegner.Name);
                }
                else if(zone > 55 && zone < 81)
                {
                    protokoll = String.Format("{0} {1} {2}", AktuellerAngreifer.Name, "zerteilt den Kopf von", aktuellerGegner.Name);
                }
                else if(zone > 80 && zone < 91)
                {
                    protokoll = String.Format("{0} {1} {2}", AktuellerAngreifer.Name, "zerteilt den Kopf von", aktuellerGegner.Name);
                }
                else if(zone > 91)
                {
                    protokoll = String.Format("{0} {1} {2}", AktuellerAngreifer.Name, "zerteilt den Kopf von", aktuellerGegner.Name);
                }
            }
            else
            {
                if (zone > 0 && zone < 16)
                {

                }
                else if (zone > 15 && zone < 36)
                {

                }
                else if (zone > 35 && zone < 56)
                {

                }
                else if (zone > 55 && zone < 81)
                {

                }
                else if (zone > 80 && zone < 91)
                {

                }
                else if (zone > 91)
                {

                }
            }
        }
    }
}
