using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Maps;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Armalia.Sprites;
using Armalia.Characters;
using Microsoft.Xna.Framework.Input;

namespace Armalia.GameScreens
{
    ///<summary>
    ///This class will be used to control what the player sees. This includes the current game level and the section of the game level's map.
    ///</summary>
    class GameplayScreen : Screen
    {
        private Point cameraSize;

        private Player player;
        private GameLevel level;
        private ArmaliaGame game;
        private MapMaker mapMaker;
        private ScreenManager manager;

        
        // private List<Tiles or something> uncoveredArea

        Texture2D splash;
        Texture2D box;
        Vector2 borderPos = Vector2.Zero;

        public GameplayScreen(ArmaliaGame game, ScreenManager manager)
        {
            this.game = game;
            this.manager = manager;
            mapMaker = new MapMaker(@"Maps\Village1\Village1.tmx", game.Content);
            cameraSize = new Point(25 * MapMaker.TILE_WIDTH, 25 * MapMaker.TILE_HEIGHT);
            
        }

        public void Load()
        {
            level = mapMaker.buildLevel();
            box = game.Content.Load<Texture2D>(@"SpriteImages\border");
            splash = game.Content.Load<Texture2D>(@"SpriteImages\splash");

            // create player
            int playerHP = 100;
            int playerMP = 100;
            int playerXP = 0;
            int playerStrength = 10;
            int playerDefense = 10;

            Texture2D playerTexture = game.Content.Load<Texture2D>(@"Characters\vx_chara01_b-1-1");
            Point playerTextureFrameSize = new Point(32, 48);
            int playerCollisionOffset = 0;
            Point playerInitialFrame = new Point(1, 0);
            Point playerSheetSize = new Point(3, 4);
            Vector2 playerSpeed = new Vector2(2, 2);
            Vector2 initialPlayerPos = new Vector2(80, 40);

            AnimatedSprite playerSprite = new AnimatedSprite(
                playerTexture, playerTextureFrameSize, playerCollisionOffset, playerInitialFrame, playerSheetSize);

            MainCharacter playerCharacter = new MainCharacter(playerSprite, initialPlayerPos, playerHP, playerMP,
                playerXP, playerStrength, playerDefense, playerSpeed, cameraSize, level);

            player = new Player(playerCharacter);
        }

        public void Update(GameTime gameTime)
        {
            // exit game if player presses back
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                game.Exit();

            MouseState mouseState = Mouse.GetState();
            if (manager.CurrentState == GameState.Splash)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    manager.CurrentState = GameState.Exploration;
                }
            }
            else if (manager.CurrentState == GameState.Exploration)
            {
                // draw cursor border
                borderPos.X = (int)Math.Floor((float)(mouseState.X / 32));
                borderPos.Y = (int)Math.Floor((float)(mouseState.Y / 32));
                borderPos.X = (borderPos.X * 32) + (32);
                borderPos.Y = (borderPos.Y * 32) + (32);

                //// move camera view - TODO: reimplement this within Player class
                //KeyboardState key = Keyboard.GetState();
                //if (key.IsKeyDown(Keys.Down))
                //{
                //    level.MoveMap(0, 1);
                //}
                //if (key.IsKeyDown(Keys.Right))
                //{
                //    level.MoveMap(1, 0);
                //}
                //if (key.IsKeyDown(Keys.Left))
                //{
                //    level.MoveMap(-1, 0);
                //}
                //if (key.IsKeyDown(Keys.Up))
                //{
                //    level.MoveMap(0, -1);

                //}
                player.Update(gameTime, level.MapSize);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            if (manager.CurrentState == GameState.Exploration)
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

                // draw level
                level.Draw(spriteBatch);

                // draw player
                player.Draw(spriteBatch);

                // draw cursor box
                spriteBatch.Draw(box,
                    borderPos,
                     new Rectangle(0, 0, 32, 32),
                     Color.White,
                     0.0f,
                     Vector2.Zero,
                     1.0f,
                     SpriteEffects.None, 0f);

                spriteBatch.End();
            }
            else if (manager.CurrentState == GameState.Splash)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(splash, new Rectangle(0, 0, splash.Width, splash.Height), Color.White);
                spriteBatch.End();
            }
        }
    }
}
