using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class Geschoepf
    {
        String name;
        int kampfgeschick;
        int ballistischefertigkeiten;
        int staerke;
        int widerstand;
        int lebenspunkte;
        int initiative;
        int aktion;
        int waffenbonus;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public int Kampfgeschick
        {
            get
            {
                return kampfgeschick;
            }

            set
            {
                kampfgeschick = value;
            }
        }

        public int Ballistischefertigkeiten
        {
            get
            {
                return ballistischefertigkeiten;
            }

            set
            {
                ballistischefertigkeiten = value;
            }
        }

        public int Staerke
        {
            get
            {
                return staerke;
            }

            set
            {
                staerke = value;
            }
        }

        public int Widerstand
        {
            get
            {
                return widerstand;
            }

            set
            {
                widerstand = value;
            }
        }

        public int Lebenspunkte
        {
            get
            {
                return lebenspunkte;
            }

            set
            {
                lebenspunkte = value;
            }
        }

        public int Initiative
        {
            get
            {
                return initiative;
            }

            set
            {
                initiative = value;
            }
        }

        public int Aktion
        {
            get
            {
                return aktion;
            }

            set
            {
                aktion = value;
            }
        }

        public int Waffenbonus
        {
            get
            {
                return waffenbonus;
            }

            set
            {
                waffenbonus = value;
            }
        }
    }
}
