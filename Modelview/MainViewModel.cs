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
            this.ButtonWuerfeln = new UserCommand(new Action<object>(Wuerfeln));
        }

        private void AddGegner(Object obj)
        {
            Geschoepf gesch = (Geschoepf)obj;
            warhammerKampf.FuegeGegnerHinzu(gesch);
        }

        private void Wuerfeln(Object obj)
        {
            warhammerKampf.Wuerfeln();
        }

    }
}
