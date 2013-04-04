using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Armalia.Characters
{
    abstract class Character
    {
        public const float DEFAULT_LAYER_DEPTH = 0.1F;

        private AnimatedSprite sprite;
        private int numCoins;

        // movement data
        protected Vector2 position;
        protected Vector2 speed;

        public Character(AnimatedSprite sprite, Vector2 position, Vector2 speed) : this(sprite, position, 0, speed) { }

        public Character(AnimatedSprite sprite, Vector2 position, int numCoins, Vector2 speed)
        {
            this.sprite = sprite;
            this.position = position;
            numCoins = 0;
            this.speed = speed;
        }

        // validate here - make sure not out of bounds or collision
        public void Move(MoveDirection direction, Rectangle mapBounds)
        {
            if (direction != MoveDirection.None)
            {
                Vector2 movedPosition = position;

                // caculate position if character were to move
                switch (direction)
                {
                    case MoveDirection.Up:
                        movedPosition.Y -= speed.Y;
                        break;
                    case MoveDirection.Down:
                        movedPosition.Y += speed.Y;
                        break;
                    case MoveDirection.Left:
                        movedPosition.X -= speed.X;
                        break;
                    case MoveDirection.Right:
                        movedPosition.X += speed.X;
                        break;
                }

                // check if movement would take character out of bounds
                bool outOfBounds = false;
                if ((movedPosition.X < 0) || (movedPosition.Y < 0) ||
                    (movedPosition.X > mapBounds.Width - sprite.FrameSize.X) ||
                    (movedPosition.Y > mapBounds.Height - sprite.FrameSize.Y))
                {
                    outOfBounds = true;
                }

                if (!outOfBounds)
                {
                    position = movedPosition;
                    // Console.WriteLine("Moved to {0}, {1}", position.X, position.Y);
                }

            }

        }

        public enum MoveDirection
        {
            Down = 0,
            Left = 1,
            Right = 2,
            Up = 3,
            None = -1
        }

        public virtual void Update(GameTime gameTime, MoveDirection moveDirection, Rectangle mapBounds)
        {

            Move(moveDirection, mapBounds);
            sprite.Update(gameTime, moveDirection);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, position, DEFAULT_LAYER_DEPTH);
        }
    }
}
