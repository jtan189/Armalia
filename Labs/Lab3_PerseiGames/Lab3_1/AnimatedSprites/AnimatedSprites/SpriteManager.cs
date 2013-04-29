// Lab Exercise 3.1
// CSCI 313
// Section 1
// March 7, 2013
//SpriteManager.cs
// 
// Persei Games:
//    Anderson, Justin
//    Calvillo, Anthony
//    DeSilva, Nilmini
//    Tan, Josh

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimatedSprites
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        // SpriteBatch for drawing
        SpriteBatch spriteBatch;

        // a sprite for the player and a list of automated sprites
        UserControlledSprite player;
        List<Sprite> spriteList = new List<Sprite>();

        public SpriteManager(Game game)
            : base(game)
        {
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            // load the player sprite
            player = new UserControlledSprite(
                Game.Content.Load<Texture2D>(@"Images/threerings"),
                Vector2.Zero, new Point(75, 75), 10, new Point(0, 0),
                new Point(6, 8), new Vector2(6, 6));

            // load several different automated sprites into the list
            spriteList.Add(new BouncingSprite(
                Game.Content.Load<Texture2D>(@"Images/skullball"),
                new Vector2(150, 150), new Point(75, 75), 10, new Point(0, 0),
                new Point(6, 8), BouncingSprite.GenerateRandomVelocity(), "skullcollision"));
            spriteList.Add(new BouncingSprite(
                Game.Content.Load<Texture2D>(@"Images/plus"),
                new Vector2(300, 150), new Point(75, 75), 10, new Point(0, 0),
                new Point(6, 4), BouncingSprite.GenerateRandomVelocity(), "skullcollision"));
            spriteList.Add(new BouncingSprite(
                Game.Content.Load<Texture2D>(@"Images/skullball"),
                new Vector2(150, 300), new Point(75, 75), 10, new Point(0, 0),
                new Point(6, 8), BouncingSprite.GenerateRandomVelocity(), "skullcollision"));
            spriteList.Add(new BouncingSprite(
                Game.Content.Load<Texture2D>(@"Images/plus"),
                new Vector2(600, 400), new Point(75, 75), 10, new Point(0, 0),
                new Point(6, 4), BouncingSprite.GenerateRandomVelocity(), "skullcollision"));

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // update player
            player.Update(gameTime, Game.Window.ClientBounds);

            // update all sprites
            //foreach (Sprite s in spriteList)
            //{
            //    s.Update(gameTime, Game.Window.ClientBounds);

            //    // check for collisions and exit game if there is one
            //    if (s.CollisionRect.Intersects(player.CollisionRect))
            //        Game.Exit();
            //}

            // Update all sprites
            for (int i = 0; i < spriteList.Count; ++i)
            {
                Sprite s = spriteList[i];
                s.Update(gameTime, Game.Window.ClientBounds);
                // Check for collisions
                if (s.CollisionRect.Intersects(player.CollisionRect))
                {
                    // Play collision sound
                    if(s.collisionCueName != null)
                    ((Game1)Game).PlayCue(s.collisionCueName);

                    // Remove collided sprite from the game
                    spriteList.RemoveAt(i);
                    i -= i;//−−i;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            // draw the player
            player.Draw(gameTime, spriteBatch);

            // draw all sprites
            foreach (Sprite s in spriteList)
                s.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
