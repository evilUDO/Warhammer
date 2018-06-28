using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Viewmodel
{
    public class MainViewModel
    {
        Kampf warhammerKampf = null;

        private ICommand addGegCommand;

        private ICommand buttonWuerfeln;

        private ICommand addCharCommand;

        private ICommand naechsterCommand;

        private ICommand angriffsCommand;

        private ICommand removeCommand;

        public ICommand AddGegCommand
        {
            get
            {
                return addGegCommand;
            }

            set
            {
                addGegCommand = value;
            }
        }

        public ICommand AddCharCommand
        {
            get
            {
                return addCharCommand;
            }

            set
            {
                addCharCommand = value;
            }
        }

        public ICommand NaechsterCommand
        {
            get
            {
                return naechsterCommand;
            }

            set
            {
                naechsterCommand = value;
            }
        }

        public ICommand AngriffsCommand
        {
            get
            {
                return angriffsCommand;
            }

            set
            {
                angriffsCommand = value;
            }
        }

        public ICommand ButtonWuerfeln
        {
            get
            {
                return buttonWuerfeln;
            }

            set
            {
                buttonWuerfeln = value;
            }
        }

        public ICommand RemoveCommand
        {
            get
            {
                return removeCommand;
            }

            set
            {
                removeCommand = value;
            }
        }

        public Kampf WarhammerKampf
        {
            get
            {
                return warhammerKampf;
            }

            set
            {
                warhammerKampf = value;
            }
        }

        public MainViewModel()
        {
            WarhammerKampf = new Kampf();
            this.AddGegCommand = new UserCommand(new Action<object>(AddGegner));

            this.AddCharCommand = new UserCommand(new Action<object>(AddCharakter));

            this.NaechsterCommand = new UserCommand(new Action<object>(Naechster));

            this.AngriffsCommand = new UserCommand(new Action<object>(Angriff));
            this.ButtonWuerfeln = new UserCommand(new Action<object>(Wuerfeln));

            this.RemoveCommand = new UserCommand(new Action<object>(Remove));
        }

        private void Remove(Object obj)
        {
            Geschoepf gesch = (Geschoepf) obj; 
            warhammerKampf.Entferne(gesch);
        }

        private void AddGegner(Object obj)
        {
            Geschoepf gesch = (Geschoepf)obj;
            WarhammerKampf.FuegeGegnerHinzu(gesch);
        }


        private void AddCharakter(Object obj)
        {
            Geschoepf gesch = (Geschoepf)obj;
            WarhammerKampf.FuegeCharakterHinzu(gesch);
        }

        private void Naechster(Object obj)
        {
            WarhammerKampf.NaechsterSpieler();
        }

        private void Angriff(Object obj)
        {
            Geschoepf verteidiger = (Geschoepf) obj;         
            WarhammerKampf.PruefeWaffe(verteidiger);
        }

        private void Wuerfeln(Object obj)
        {
            WarhammerKampf.Wuerfeln();
        }

    }
}
