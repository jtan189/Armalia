using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Armalia.GameScreens
{
    abstract class Screen
    {
        protected ArmaliaGame game;
        protected ScreenManager manager;

        public Screen(ArmaliaGame game, ScreenManager manager)
        {
            this.game = game;
            this.manager = manager;
        }
        
    }
}
