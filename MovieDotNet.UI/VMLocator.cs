using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace MovieDotNet.UI
{
    public class VMLocator : ViewModelBase
    {
        public static VMLocator Instance { get; set; } = new VMLocator();

        public VMLocator()
        {
            Main = new LoginViewModel();
        }

        private ViewModelBase _main;
        public ViewModelBase Main
        {
            get { return _main;  }
            set
            {
                _main = value;
                RaisePropertyChanged(nameof(Main));
            }
        }


    }
}
