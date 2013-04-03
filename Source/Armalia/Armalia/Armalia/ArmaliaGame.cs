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
using Armalia.Mapping;

namespace Armalia
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ArmaliaGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MapMaker mm;
        Texture2D box;
        Vector2 borderPos = Vector2.Zero;
        Level level;
        Texture2D splash;
        GameState gs;
        public ArmaliaGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 800;
            Content.RootDirectory = "Content";
            gs = GameState.Splash;
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
            mm = new MapMaker(@"maps\map.tmx", Content);
           
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
            level= mm.buildLevel();
            box = Content.Load<Texture2D>(@"SpriteImages\border");
            splash = Content.Load<Texture2D>(@"SpriteImages\splash");
            // TODO: use this.Content to load your game content here
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            MouseState ms = Mouse.GetState();
            if (this.gs == GameState.Splash)
            {
                if (ms.LeftButton == ButtonState.Pressed)
                {
                    this.gs = GameState.Exploration;
                }
            }
            else if (this.gs == GameState.Exploration)
            {
               
                borderPos.X = (int)Math.Floor((float)(ms.X / 32));
                borderPos.Y = (int)Math.Floor((float)(ms.Y / 32));
                borderPos.X = (borderPos.X * 32) + (32);
                borderPos.Y = (borderPos.Y * 32) + (32);
                // TODO: Add your update logic here
                KeyboardState key = Keyboard.GetState();
                if (key.IsKeyDown(Keys.Down) || key.IsKeyDown(Keys.S))
                {
                    level.MoveMap(0, 1);
                }
                if (key.IsKeyDown(Keys.Right) || key.IsKeyDown(Keys.D))
                {
                    level.MoveMap(1, 0);
                }
                if (key.IsKeyDown(Keys.Left) || key.IsKeyDown(Keys.A))
                {
                    level.MoveMap(-1, 0);
                }
                if (key.IsKeyDown(Keys.Up) || key.IsKeyDown(Keys.W))
                {
                    level.MoveMap(0, -1);

                }
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
            if (this.gs == GameState.Exploration)
            {
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                level.Draw(spriteBatch);
                spriteBatch.Draw(box,
                    borderPos,
                     new Rectangle(0, 0, 32, 32),
                     Color.White,
                     0.0f,
                     Vector2.Zero,
                     1.0f,
                     SpriteEffects.None, 0.1f);
                // TODO: Add your drawing code here
                spriteBatch.End();
                base.Draw(gameTime);
            }
            else if (this.gs == GameState.Splash)
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
