using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Armalia.GameScreens
{
    /// <summary>
    /// This class will draw our stuff to the screen.
    /// </summary>
    class ScreenManager : DrawableGameComponent
    {
        private const string SPLASH_FILENAME = @"SpriteImages\splash";

        private SpriteBatch spriteBatch;
        private SplashScreen splashScreen;
        private GameplayScreen gameplayScreen;

        public GameState CurrentState { get; set; }

        public ScreenManager(ArmaliaGame game)
            : base(game)
        {
            spriteBatch = game.SpriteBatch;
            splashScreen = new SplashScreen(game, this, SPLASH_FILENAME);
            gameplayScreen = new GameplayScreen(game, this);
            CurrentState = GameState.Splash;
        }

        protected override void LoadContent()
        {
            splashScreen.Load();
            gameplayScreen.Load();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            switch (CurrentState)
            {
                case GameState.Splash:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        CurrentState = GameState.Gameplay;
                    }
                    break;
                case GameState.Gameplay:
                    gameplayScreen.Update(gameTime);
                    break;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            switch (CurrentState)
            {
                case GameState.Splash:
                    splashScreen.Draw(spriteBatch);
                    break;
                case GameState.Gameplay:
                    gameplayScreen.Draw(gameTime, spriteBatch);
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
