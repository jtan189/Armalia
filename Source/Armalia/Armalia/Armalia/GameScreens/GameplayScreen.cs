using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Maps;

namespace Armalia.GameScreens
{
    class GameplayScreen
    {

        private static Player player;
        private static GameLevel level;

        private ArmaliaGame game;

        public GameplayScreen(ArmaliaGame game)
        {
            this.game = game;
        }
    }
}
