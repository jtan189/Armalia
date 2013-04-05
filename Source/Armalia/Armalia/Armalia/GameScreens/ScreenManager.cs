using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Armalia.GameScreens
{
     /// <summary>
     /// This class will draw our stuff to the screen.
     /// </summary>
    class ScreenManager : DrawableGameComponent
    {

        private Screen currentScreen;
        public GameState CurrentState { get; set; }
        private SpriteBatch spriteBatch;

        private GameplayScreen gameplayScreen;

        public ScreenManager(ArmaliaGame game) : this(game, new SplashScreen(), GameState.Splash) { }

        public ScreenManager(ArmaliaGame game, Screen currentScreen, GameState state)
            : base(game)
        {
            this.currentScreen = currentScreen;
            CurrentState = state;
            spriteBatch = game.SpriteBatch;

            gameplayScreen = new GameplayScreen(game, this);
        }

        protected override void LoadContent()
        {

            gameplayScreen.Load();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            gameplayScreen.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            gameplayScreen.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
    }
}
