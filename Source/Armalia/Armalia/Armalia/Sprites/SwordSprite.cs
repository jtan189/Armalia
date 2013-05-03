using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Armalia.Sprites
{
    public class SwordSprite
    {

        protected const int DEFAULT_MS_PER_FRAME = 17; //17
        protected const float DEFAULT_SCALE = 1f;
        protected const float DEFAULT_LAYER_DEPTH = 0F;
        protected const float DEFAULT_ROTATION_INCREMENT = .17F;


        /// <summary>
        /// We don't want our animated sprite to be moving to fast. The player wont be able to see
        /// the animations.
        /// </summary>
        protected int timeSinceLastFrame = 0;
        /// <summary>
        /// How many frames per second. This is to control the frame rate.
        /// </summary>
        protected int msPerFrame;
        private float scale;
        private float rotation;
        private float layerDepth;
        private Vector2 gripPoint;
        private Texture2D texture;

        public SwordSprite(Texture2D texture, int msPerFrame, float scale, Vector2 gripPoint)
        {
            layerDepth = DEFAULT_LAYER_DEPTH;
            this.msPerFrame = msPerFrame;
            this.texture = texture;
            this.scale = scale;
            this.gripPoint = gripPoint;
            rotation = 0;
        }


        public void Update(GameTime gameTime, out bool attackFinished)
        {
            attackFinished = false;

            // update frame if time to do so, based on framerate
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > msPerFrame)
            {
                // reset time since last frame
                timeSinceLastFrame = 0;
                if (rotation + DEFAULT_ROTATION_INCREMENT <= Math.PI / 2.0)
                {
                    rotation += DEFAULT_ROTATION_INCREMENT;
                }
                else
                {
                    rotation = 0;
                    attackFinished = true;
                }
            }

        }

        /// <summary>
        /// This draws the animated sprite onto the map or whatever.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch object of the game.</param>
        /// <param name="spritePosition">The position of the sprite</param>
        /// <param name="layerDepth">The z-index of the sprite</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 spritePosition)
        {
            // draw the sprite
            spriteBatch.Draw(texture, spritePosition,
                null, Color.White, rotation, gripPoint,
                scale, SpriteEffects.None, layerDepth);
        }
    }
}
