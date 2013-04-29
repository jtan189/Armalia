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
   public class ScreenManager : DrawableGameComponent
    {

        //private Screen currentScreen;
        private SpriteBatch spriteBatch;
        private Stack<GameState> stateStack;

        private GameplayScreen gameplayScreen;
        private SplashScreen splashScreen;

        public GameState CurrentState
        {
            get
            {
                return stateStack.Peek();
            }

            set
            {
                stateStack.Push(value);
            }
        }

        public ScreenManager(ArmaliaGame game)
            : base(game)
        {
            this.splashScreen = new SplashScreen(game, this);
            //this.currentScreen = splashScreen;
            stateStack = new Stack<GameState>();
            CurrentState = GameState.Splash; // push onto stack
            spriteBatch = game.SpriteBatch;
            gameplayScreen = new GameplayScreen(game, this);
        }

        protected override void LoadContent()
        {
            splashScreen.Load();
            gameplayScreen.Load();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            switch (stateStack.Peek())
            {
                case GameState.Splash:

                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        CurrentState = GameState.Exploration;
                    }
                    break;
                case GameState.Exploration:
                    gameplayScreen.Update(gameTime);
                    break;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            switch (stateStack.Peek())
            {
                case GameState.Splash:
                    DrawSplashScreen(spriteBatch);
                    break;
                case GameState.Exploration:
                    DrawExplorationScreen(gameTime, spriteBatch);
                    break;
                case GameState.TransitionToBattle:
                    DrawExplorationScreen(gameTime, spriteBatch);
                    break;
            }

            base.Draw(gameTime);
        }

        public void DrawSplashScreen(SpriteBatch spriteBatch)
        {
            splashScreen.Draw(spriteBatch);
        }

        public void DrawExplorationScreen(GameTime gameTime, SpriteBatch spriteBatch)
        {
            gameplayScreen.Draw(gameTime, spriteBatch);
        }
    }
}
