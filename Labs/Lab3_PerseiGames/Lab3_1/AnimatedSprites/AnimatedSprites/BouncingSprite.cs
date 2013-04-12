// Lab Exercise 2
// CSCI 313
// Section 1
// March 7, 2013
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
    class BouncingSprite : AutomatedSprite
    {
        private const int DEFAULT_MIN_SPEED = 4;
        private const int DEFAULT_MAX_SPEED = 6;
        
        // random number generator for initial sprite velocities
        private static Random rand = new Random();

        public BouncingSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, string collisionCueName)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, collisionCueName)
        {
        }

        public BouncingSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            int millisecondsPerFrame, string collisionCueName)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize,
            speed, millisecondsPerFrame, collisionCueName)
        {
        }

        // generate a random initial velocity
        public static Vector2 GenerateRandomVelocity()
        {
            int randomSpeedX = rand.Next(DEFAULT_MIN_SPEED, DEFAULT_MAX_SPEED);
            int randomSpeedY = rand.Next(DEFAULT_MIN_SPEED, DEFAULT_MAX_SPEED);
            return new Vector2(randomSpeedX, randomSpeedY);
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            // check if sprite has gone off the screen
            if (position.X < 0)
            {
                position.X = 0;
                ReverseHorizontalDirection();
            }
            if (position.Y < 0)
            {
                position.Y = 0;
                ReverseVerticalDirection();
            }
            if (position.X > clientBounds.Width - frameSize.X)
            {
                position.X = clientBounds.Width - frameSize.X;
                ReverseHorizontalDirection();
            }
            if (position.Y > clientBounds.Height - frameSize.Y)
            {
                position.Y = clientBounds.Height - frameSize.Y;
                ReverseVerticalDirection();
            }

            base.Update(gameTime, clientBounds);
        }

        // reverse the horizontal direction of this sprite
        void ReverseHorizontalDirection()
        {
            Direction = new Vector2(Direction.X * -1, Direction.Y);
        }

        // reverse the vertical direction of this sprite
        void ReverseVerticalDirection()
        {
            Direction = new Vector2(Direction.X, Direction.Y * -1);
        }

    }
}
