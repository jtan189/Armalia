using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Armalia.Sprites
{
    class SwordSprite
    {
        private const int DEFAULT_MS_PER_FRAME = 17; //17
        private const float DEFAULT_SCALE = 1f;
        private const float DEFAULT_LAYER_DEPTH = 0F;
        private const float DEFAULT_ROTATION_INCREMENT = .4F;

        private int timeSinceLastFrame = 0;
        private int msPerFrame;

        private float scale;
        private float rotation;
        private float layerDepth;
        private Vector2 rotationPoint;
        private Texture2D texture;

        public bool Animating { get; set; }

        public SwordSprite(Texture2D texture, int msPerFrame, float scale, Vector2 rotationPoint)
        {
            this.layerDepth = DEFAULT_LAYER_DEPTH;
            this.msPerFrame = msPerFrame;
            this.texture = texture;
            this.scale = scale;
            this.rotationPoint = rotationPoint;
            this.rotation = 0;
            Animating = false;
        }

        public void Update(GameTime gameTime, bool playerPressedAttack)
        {
            if (!Animating && playerPressedAttack)
            {
                Animating = true;
            }

            if (Animating)
            {
                // update frame if time to do so, based on framerate
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > msPerFrame)
                {
                    // reset time since last frame
                    timeSinceLastFrame = 0;
                    if (rotation + DEFAULT_ROTATION_INCREMENT <= (2 * Math.PI))
                    {
                        rotation += DEFAULT_ROTATION_INCREMENT;
                    }
                    else
                    {
                        rotation = 0;
                        Animating = false;
                    }
                }
            }

        }

        /// <summary>
        /// This draws the animated sprite onto the map or whatever.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch object of the game.</param>
        /// <param name="spritePosition">The position of the sprite</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 spritePosition)
        {
            if (Animating)
            {
                // draw the sprite
                spriteBatch.Draw(texture, spritePosition,
                    null, Color.White, rotation, rotationPoint,
                    scale, SpriteEffects.None, layerDepth);
            }
        }
    }
}
