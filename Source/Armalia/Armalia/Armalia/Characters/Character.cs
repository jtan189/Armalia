using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Armalia.Maps;

namespace Armalia.Characters
{
    abstract class Character
    {
        public const float DEFAULT_LAYER_DEPTH = 0.1F;

        protected AnimatedSprite sprite;
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
        public virtual bool Move(MoveDirection direction, Map currentMap, out bool hasCollided)
        {
            bool hasMoved = false;
            hasCollided = false;
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
                    (movedPosition.X > currentMap.Size.X - sprite.FrameSize.X) ||
                    (movedPosition.Y > currentMap.Size.Y - sprite.FrameSize.Y))
                {
                    outOfBounds = true;
                }

                // TODO: use better collision offsets
                // check if movement would result in boundary collision
                Rectangle movedRectangle = new Rectangle((int)movedPosition.X, (int)movedPosition.Y, sprite.FrameSize.X, sprite.FrameSize.Y);
                bool isCollision = currentMap.CollidesWithBoundary(movedRectangle);
                hasCollided = isCollision; // needed to avoid abruptly stopping animation when collision occurs

                if (!(outOfBounds || isCollision))
                {
                    position = movedPosition;
                    hasMoved = true;
                }

            }

            return hasMoved;

        }

        public enum MoveDirection
        {
            Down = 0,
            Left = 1,
            Right = 2,
            Up = 3,
            None = -1
        }

        public virtual void Update(GameTime gameTime, MoveDirection moveDirection, Map currentMap)
        {
            bool hasCollided;
            bool hasMoved = Move(moveDirection, currentMap, out hasCollided);

            sprite.Update(gameTime, moveDirection, hasCollided);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, position, DEFAULT_LAYER_DEPTH);
        }
    }
}
