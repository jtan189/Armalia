using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Armalia.Characters;

namespace Armalia.Sprites
{
    /// <summary>
    /// This is the base class for spites that have animation frames
    /// </summary>
    class AnimatedSprite : Sprite
    {
        private const int DEFAULT_MS_PER_FRAME = 150;

        // stuff needed to draw the sprite
        protected Point currentFrame;
        protected Point prevFrame;
        protected Point sheetSize;

        // collision data
        protected int collisionOffset;

        // framerate stuff
        protected int timeSinceLastFrame = 0;
        protected int msPerFrame;

        public AnimatedSprite(Texture2D texture, Point frameSize, int collisionOffset,
            Point initialFrame, Point sheetSize)
            : base(texture, frameSize)
        {
            this.collisionOffset = collisionOffset;
            currentFrame = initialFrame;
            this.sheetSize = sheetSize;

            // set prevFrame coords to negative value, so animation can start (TODO: necessary?)
            prevFrame = new Point(-1, -1);
            msPerFrame = DEFAULT_MS_PER_FRAME;
        }

        public void Update(GameTime gameTime, Character.MoveDirection moveDirection)
        {
            // update frame if time to do so, based on framerate
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > msPerFrame)
            {
                // reset time since last frame
                timeSinceLastFrame = 0;
                // if moving

                Point tempCurrentFrame = currentFrame;
                // change to next frame based on previous frame and movement direction
                int currentDirection = currentFrame.Y;
                if (currentDirection == (int)moveDirection)
                {
                    currentFrame.X = GetOscillatedValue(currentFrame.X, prevFrame.X, 0, sheetSize.X - 1);
                }
                else
                {
                    // reset animation at X coord of 1
                    currentFrame.X = 1;

                    // if character is moving, change direction
                    if (moveDirection != Character.MoveDirection.None)
                    {
                        currentFrame.Y = (int)moveDirection;
                    }

                }

                prevFrame = tempCurrentFrame;
             
            }

        }

        private int GetOscillatedValue(int current, int prev, int min, int max)
        {
            if (current <= prev)
            {

                return (current <= min) ? ++current : --current;
            }
            else
            {
                return (current == max) ? --current : ++current;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 spritePosition, float layerDepth)
        {
            // draw the sprite
            spriteBatch.Draw(texture, spritePosition,
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X, frameSize.Y),
                Color.White, 0, Vector2.Zero,
                1f, SpriteEffects.None, layerDepth);
        }
    }
}
