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

        public MainViewModel()
        {
           warhammerKampf = new Kampf();
            this.AddGegCommand = new UserCommand(new Action<object>(AddGegner));
        }

        private void AddGegner(Object obj)
        {
            Geschoepf gesch = (Geschoepf)obj;
            warhammerKampf.FuegeGegnerHinzu(gesch);
        }

    }
}
