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
        private string splashFilename;
        private Texture2D splashTexture;

        public SplashScreen(ArmaliaGame game, ScreenManager manager, String splashFilename)
            : base(game, manager)
        {
            this.splashFilename = splashFilename;
        }

        public void Load()
        {
            splashTexture = game.Content.Load<Texture2D>(splashFilename);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(splashTexture, new Rectangle(0, 0, splashTexture.Width, splashTexture.Height), Color.White);
            spriteBatch.End();
        }
    }
}
