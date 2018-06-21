using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viewmodel
{
    public class MainViewModel
    {
        Kampf warhammerKampf = null;

        public MainViewModel()
        {
           warhammerKampf = new Kampf();
        }

    }
}
