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

        public MainViewModel()
        {
            warhammerKampf = new Kampf();
            this.AddGegCommand = new UserCommand(new Action<object>(AddGegner));

            this.AddCharCommand = new UserCommand(new Action<object>(AddCharakter));

            this.NaechsterCommand = new UserCommand(new Action<object>(Naechster));

            this.AngriffsCommand = new UserCommand(new Action<object>(Angriff));
            this.ButtonWuerfeln = new UserCommand(new Action<object>(Wuerfeln));
        }

        private void AddGegner(Object obj)
        {
            Geschoepf gesch = (Geschoepf)obj;
            warhammerKampf.FuegeGegnerHinzu(gesch);
        }


        private void AddCharakter(Object obj)
        {
            Geschoepf gesch = (Geschoepf)obj;
            warhammerKampf.FuegeCharakterHinzu(gesch);
        }

        private void Naechster(Object obj)
        {
            warhammerKampf.NaechsterSpieler();
        }

        private void Angriff(Object obj)
        {
            Geschoepf verteidiger = (Geschoepf)obj;
            warhammerKampf.PruefeWaffe(verteidiger);
        }

        private void Wuerfeln(Object obj)
        {
            warhammerKampf.Wuerfeln();
        }

    }
}
