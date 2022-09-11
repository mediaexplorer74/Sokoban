using Sokoban_Game_WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_Game_UWP
{
    class Parameters
    {
        Controller controller;
        MainPage page;

        public Parameters(Controller cont, MainPage main)
        {
            controller = cont;
            page = main;
        }

        public Parameters(Controller cont)
        {
            controller = cont;
        }

        public Controller getController() => controller;
        public MainPage getPage() => page;
    }
}
