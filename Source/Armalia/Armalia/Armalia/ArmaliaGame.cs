using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Armalia.Maps;
using Armalia.Characters;
using Armalia.Sprites;

namespace Armalia
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ArmaliaGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D box;
        Vector2 borderPos = Vector2.Zero;
        Texture2D splash;

        MapMaker mapMaker;

        GameLevel level;
        GameState state;
        Player player;

        public ArmaliaGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 800;
            Content.RootDirectory = "Content";

            state = GameState.Splash;
            mapMaker = new MapMaker(@"Maps\Village1\Village1.tmx", Content);
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            level= mapMaker.buildLevel();
            box = Content.Load<Texture2D>(@"SpriteImages\border");
            splash = Content.Load<Texture2D>(@"SpriteImages\splash");
            
            // create player
            int playerHP = 100;
            int playerMP = 100;
            int playerXP = 0;
            int playerStrength = 10;
            int playerDefense = 10;

            Texture2D playerTexture = Content.Load<Texture2D>(@"Characters\vx_chara01_b-1-1");
            Point playerTextureFrameSize = new Point(32, 48);
            int playerCollisionOffset = 0;
            Point playerInitialFrame = new Point(1, 0);
            Point playerSheetSize = new Point(3, 4);
            Vector2 playerSpeed = new Vector2(3, 3);
            Vector2 initialPlayerPos = new Vector2(80, 40);

            AnimatedSprite playerSprite = new AnimatedSprite(
                playerTexture, playerTextureFrameSize, playerCollisionOffset, playerInitialFrame, playerSheetSize);

            MainCharacter playerCharacter = new MainCharacter(playerSprite, initialPlayerPos, playerHP, playerMP,
                playerXP, playerStrength, playerDefense, playerSpeed);

            player = new Player(playerCharacter);

            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // exit game if player presses back
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            MouseState mouseState = Mouse.GetState();
            if (this.state == GameState.Splash)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    this.state = GameState.Exploration;
                }
            }
            else if (this.state == GameState.Exploration)
            {
                // draw cursor border
                borderPos.X = (int)Math.Floor((float)(mouseState.X / 32));
                borderPos.Y = (int)Math.Floor((float)(mouseState.Y / 32));
                borderPos.X = (borderPos.X * 32) + (32);
                borderPos.Y = (borderPos.Y * 32) + (32);

                // move camera view - TODO: reimplement this within Player class
                KeyboardState key = Keyboard.GetState();
                if (key.IsKeyDown(Keys.Down))
                {
                    level.MoveMap(0, 1);
                }
                if (key.IsKeyDown(Keys.Right))
                {
                    level.MoveMap(1, 0);
                }
                if (key.IsKeyDown(Keys.Left))
                {
                    level.MoveMap(-1, 0);
                }
                if (key.IsKeyDown(Keys.Up))
                {
                    level.MoveMap(0, -1);

                }
                player.Update(gameTime, level.MapBounds);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            if (this.state == GameState.Exploration)
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
                base.Draw(gameTime);
            }
            else if (this.state == GameState.Splash)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(splash, new Rectangle(0,0, splash.Width, splash.Height), Color.White);
                spriteBatch.End();
            }
        }
    }

    enum GameState
    {
        Exploration,
        Battle,
        CutScene,
        GameOver,
        Splash

    }
}
