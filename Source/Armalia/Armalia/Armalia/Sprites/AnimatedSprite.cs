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
        private const int DEFAULT_MS_PER_FRAME = 100;

        // stuff needed to draw the sprite
        /// <summary>
        /// Point of current Frame.
        /// </summary>
        protected Point currentFrame;
        /// <summary>
        /// We need to keep track of the previous frame for animations.
        /// </summary>
        protected Point prevFrame;
        /// <summary>
        /// The number of frames per row and column.
        /// </summary>
        protected Point sheetSize;

        /// <summary>
        /// This is the collission offset. Gives more precision with colission
        /// </summary>
        protected int collisionOffset;

        /// <summary>
        /// We don't want our animated sprite to be moving to fast. The player wont be able to see
        /// the animations.
        /// </summary>
        protected int timeSinceLastFrame = 0;
        /// <summary>
        /// How many frames per second. This is to control the frame rate.
        /// </summary>
        protected int msPerFrame;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="texture">This is the Texture2D image of the sprite sheet.</param>
        /// <param name="frameSize">The frame size in pixels. Width x Height</param>
        /// <param name="collisionOffset">The collission offset to give more accurate collission</param>
        /// <param name="initialFrame">The initial frame to be drawn</param>
        /// <param name="sheetSize">The number of frames per row/column.</param>
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
        /// <summary>
        /// This is the update method. Update our animated sprite by chaning the frame.
        /// </summary>
        /// <param name="gameTime">This is the game time to control the frame rate.</param>
        /// <param name="moveDirection">The direction of the sprite (up, down, left, right, none)</param>
        /// <param name="hasCollided">Has it collided with another rectangle</param>
        public void Update(GameTime gameTime, Character.MoveDirection moveDirection, bool hasCollided)
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
                if (currentDirection == (int)moveDirection && !hasCollided)
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
        /// <summary>
        /// This restarts the animations. I.E the player walks left and there are 2 frames for that. 
        /// We need to reset the walking animations. The paramaters are weakly described.
        /// Talk to Josh for more details. He may even update the comments
        /// </summary>
        /// <param name="current">Current time</param>
        /// <param name="prev">Previous time</param>
        /// <param name="min">The minumum</param>
        /// <param name="max">The maximum</param>
        /// <returns>The frame to be drawn?</returns>
        private int GetOscillatedValue(int current, int prev, int min, int max)
        {
            if (current <= prev)
            {

                return (current == min) ? ++current : --current;
            }
            else
            {
                return (current == max) ? --current : ++current;
            }
        }
        /// <summary>
        /// This draws the animated sprite onto the map or whatever.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch object of the game.</param>
        /// <param name="spritePosition">The position of the sprite</param>
        /// <param name="layerDepth">The z-index of the sprite</param>
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
