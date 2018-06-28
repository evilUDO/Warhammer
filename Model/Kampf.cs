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
        ObservableCollection<String> protokollSheet = null;

        ObservableCollection<Geschoepf> alleImKampf = null;

        List<Geschoepf> sortierer = null;

        Geschoepf aktuellerAngreifer;
        Int32 index = 0;
        Int32 maxAnzahl = 0;
        int[] werte = new int[3];

        public Kampf()
        {
            AlleSpieler = new ObservableCollection<Geschoepf>();
            AlleGegner = new ObservableCollection<Geschoepf>();
            AlleImKampf = new ObservableCollection<Geschoepf>();
            ProtokollSheet = new ObservableCollection<string>();
            new DBread(AlleSpieler, AlleGegner);
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

        public ObservableCollection<Geschoepf> AlleGegner
        {
            get
            {
                return alleGegner;
            }

            set
            {
                alleGegner = value;
            }
        }

        public ObservableCollection<Geschoepf> AlleSpieler
        {
            get
            {
                return alleSpieler;
            }

            set
            {
                alleSpieler = value;
            }
        }

        public ObservableCollection<Geschoepf> AlleImKampf
        {
            get
            {
                return alleImKampf;
            }

            set
            {
                alleImKampf = value;
            }
        }

        public List<Geschoepf> Sortierer
        {
            get
            {
                return sortierer;
            }

            set
            {
                sortierer = value;
            }
        }

        public void FuegeGegnerHinzu(Geschoepf aGesch)
        {
            foreach(Geschoepf g in AlleGegner)
            {
                if(g.Name.Equals(aGesch.Name))
                {
                    AlleImKampf.Add(aGesch);
                    ProtokolliereBeitritt(aGesch);
                }
            }

            AktualisiereAlle();
        }

        public void FuegeCharakterHinzu(Geschoepf aGesch)
        {
            foreach(Geschoepf g in AlleSpieler)
            {
                if(g.Name.Equals(aGesch.Name))
                {
                    AlleImKampf.Add(aGesch);
                    ProtokolliereBeitritt(aGesch);
                }
            }

            AktualisiereAlle();
        }

        private void ProtokolliereBeitritt(Geschoepf g)
        {
            ProtokollSheet.Add(String.Format("{0} tritt dem Kampf bei", g.Name));
        }

        private void AktualisiereAlle()
        {
            maxAnzahl = alleImKampf.Count();
            alleImKampf.Sort((n1, n2) => n2.Initiative.CompareTo(n1.Initiative));
            Sortierer = AlleImKampf.ToList<Geschoepf>();
            maxAnzahl = Sortierer.Count();
            Sortierer.Sort((n1, n2) => n1.Initiative.CompareTo(n2.Initiative));

            AlleImKampf.Clear();
            Sortierer.ForEach((n1) => AlleImKampf.Add(n1));

            Sortierer = null;
   
            PruefeAktuell();
        }

        public void PruefeAktuell()
        {
        
            AktuellerAngreifer = AlleImKampf.ElementAt(index);
            protokollSheet.Add(String.Format("{0} {1}", AktuellerAngreifer.Name, "ist nun an der Reihe"));
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

        public void Angriff(Geschoepf verteidiger)
        {
            if (aktuellerAngreifer.Kampfgeschick > werte[1])
            {
                if (Schadensberechnung(AktuellerAngreifer, verteidiger))
                {
                    Entferne(verteidiger);
                    SchreibeProtokoll(true, verteidiger);
                }
                else
                {
                    SchreibeProtokoll(false, verteidiger);
                }
            }
            else
            {
                String protokoll = String.Format("{0} {1} {2}", AktuellerAngreifer.Name, "misslingt der Angriff auf", verteidiger.Name);
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
            String protokoll = "";
            if (werte[0] == 1 || werte[0] == 2)
            {
                protokoll = String.Format("{0} {1} {2} {3}",AktuellerAngreifer.Name,"trifft",verteidiger.Name,"kritisch");
                protokollSheet.Add(protokoll);
                Angriff(verteidiger);
            }
            else if (werte[0] == 99 || werte[0] == 100)
            {
                protokoll = String.Format("{0} {1}", AktuellerAngreifer.Name, "verfehlt sein Gegenüber kritisch");
                protokollSheet.Add(protokoll);
                Angriff(verteidiger);
            }
            else
            {
                Angriff(verteidiger);
            }
            
        }

        private void SchreibeProtokoll(Boolean tot, Geschoepf verteidiger)
        {
            int zone = werte[1];
            String protokoll;
            if(tot)
            {
                // 1-15 Kopf - 16-35 r. Arm - 36-55 l. Arm - 56-80 Torso - 81-90 r. Bein - 91-100 l. Bein
                if(zone > 0 && zone <16)
                {
                    protokoll = String.Format("{0} {1} {2}", AktuellerAngreifer.Name, "zerteilt den Kopf von", verteidiger.Name);
                }
                else if(zone > 15 && zone < 36)
                {
                    protokoll = String.Format("{0} {1} {2}", AktuellerAngreifer.Name, "zerteilt den Kopf von", verteidiger.Name);
                }
                else if(zone > 35 && zone < 56)
                {
                    protokoll = String.Format("{0} {1} {2}", AktuellerAngreifer.Name, "zerteilt den Kopf von", verteidiger.Name);
                }
                else if(zone > 55 && zone < 81)
                {
                    protokoll = String.Format("{0} {1} {2}", AktuellerAngreifer.Name, "zerteilt den Kopf von", verteidiger.Name);
                }
                else if(zone > 80 && zone < 91)
                {
                    protokoll = String.Format("{0} {1} {2}", AktuellerAngreifer.Name, "zerteilt den Kopf von", verteidiger.Name);
                }
                else if(zone > 91)
                {
                    protokoll = String.Format("{0} {1} {2}", AktuellerAngreifer.Name, "zerteilt den Kopf von", verteidiger.Name);
                }
            }
            else
            {
                if (zone > 0 && zone < 16)
                {
                    protokoll = String.Format("{0} verwundet den Kopf von {1}.", AktuellerAngreifer.Name, verteidiger.Name);
                }
                else if (zone > 15 && zone < 36)
                {
                    protokoll = String.Format("{0} verwundet den rechten Arm von {1}", AktuellerAngreifer.Name, verteidiger.Name);
                }
                else if (zone > 35 && zone < 56)
                {
                    protokoll = String.Format("{0} verwundet den linken Arm von {1}", AktuellerAngreifer.Name, verteidiger.Name);
                }
                else if (zone > 55 && zone < 81)
                {
                    protokoll = String.Format("{0} rammt seine Waffe {1} in den Bauch und verwundet ihn", AktuellerAngreifer.Name, verteidiger.Name);
                }
                else if (zone > 80 && zone < 91)
                {
                    protokoll = String.Format("{0} verwundet das rechte Bein von {1}", AktuellerAngreifer.Name, verteidiger.Name);
                }
                else if (zone > 91)
                {
                    protokoll = String.Format("{0} verwundet das linke Bein von {1}", AktuellerAngreifer.Name, verteidiger.Name);
                }
            }
        }

        public void Entferne(Geschoepf gesch)
        {
            if (gesch != null)
            {
                if (alleImKampf.Contains(gesch))
                {
                    alleImKampf.Remove(gesch);
                    ProtokollSheet.Add(String.Format("{0} hatt den Kampf verlassen", gesch.Name));
                } 
            }
        }
    }

    
}
