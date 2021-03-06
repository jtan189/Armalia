﻿using System;
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
        protected const int DEFAULT_MS_PER_FRAME = 100;
        protected const float DEFAULT_SCALE = 1f;

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

        public float Scale { get; set; }

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
            : this(texture, frameSize, collisionOffset, initialFrame, sheetSize, DEFAULT_MS_PER_FRAME, DEFAULT_SCALE)
        { }

        public AnimatedSprite(Texture2D texture, Point frameSize, int collisionOffset,
            Point initialFrame, Point sheetSize, int msPerFrame, float scale)
            : base(texture, frameSize)
        {
            this.msPerFrame = msPerFrame;
            this.collisionOffset = collisionOffset;
            currentFrame = initialFrame;
            this.sheetSize = sheetSize;
            this.Scale = scale;
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
                Point tempCurrentFrame = currentFrame;

                // change to next frame based on previous frame and movement direction
                int currentDirection = currentFrame.Y;

                if (currentDirection == (int)moveDirection && !hasCollided) // if moving
                {
                    currentFrame.X = OscillatedAnimationIndexX(currentFrame.X, prevFrame.X, 0, sheetSize.X - 1);
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
        /// We need to reset the walking animations.
        /// </summary>
        /// <param name="currentFrameIndexX">Current frame X index</param>
        /// <param name="prevFrameIndexX">Previous frame X index</param>
        /// <param name="minFrameIndexX">The minumum frame X index to use</param>
        /// <param name="maxFrameIndexX">The maximum frame X index to use</param>
        /// <returns>The X index of frame to be drawn</returns>
        private int OscillatedAnimationIndexX(int currentFrameIndexX, int prevFrameIndexX, int minFrameIndexX, int maxFrameIndexX)
        {
            if (currentFrameIndexX <= prevFrameIndexX)
            {

                return (currentFrameIndexX == minFrameIndexX) ? ++currentFrameIndexX : --currentFrameIndexX;
            }
            else
            {
                return (currentFrameIndexX == maxFrameIndexX) ? --currentFrameIndexX : ++currentFrameIndexX;
            }
        }

        ///// <summary>
        ///// This draws the animated sprite onto the map or whatever.
        ///// </summary>
        ///// <param name="spriteBatch">The SpriteBatch object of the game.</param>
        ///// <param name="spritePosition">The position of the sprite</param>
        ///// <param name="layerDepth">The z-index of the sprite</param>
        //public override void Draw(SpriteBatch spriteBatch, Vector2 spritePosition, float layerDepth)
        //{
        //    Draw(spriteBatch, spritePosition, layerDepth, Color.White);
        //}

        public override void Draw(SpriteBatch spriteBatch, Vector2 spritePosition, float layerDepth, Color tint)
        {
            // draw the sprite
            spriteBatch.Draw(texture, spritePosition,
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X, frameSize.Y),
                tint, 0, Vector2.Zero,
                Scale, SpriteEffects.None, layerDepth);
        }
    }
}
