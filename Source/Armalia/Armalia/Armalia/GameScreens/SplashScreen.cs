using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Armalia.GameScreens
{
    class SplashScreen : Screen
    {
        private ArmaliaGame game;
        private ScreenManager manager;
        private Texture2D splash;

        public SplashScreen(ArmaliaGame game, ScreenManager manager)
        {
            this.game = game;
            this.manager = manager;
        }

        public void Load()
        {
            splash = game.Content.Load<Texture2D>(@"SpriteImages\splash");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(splash, new Rectangle(0, 0, splash.Width, splash.Height), Color.White);
            spriteBatch.End();
        }
    }
}
