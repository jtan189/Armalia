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
    /// <summary>
    /// This is the base class for characters. Characters are your main character, enemies, etc
    /// </summary>
   public abstract class Character
    {
        /// <summary>
        /// The default Layer that they're drawn at
        /// </summary>
        public const float DEFAULT_LAYER_DEPTH = 0.1F;
        /// <summary>
        /// The animated sprite that defines their looks and animations
        /// </summary>
        protected AnimatedSprite sprite;
        /// <summary>
        /// This is used for currency.
        /// </summary>
        private int numCoins;

        /// <summary>
        /// This is the location of the character on the map
        /// </summary>
        public Vector2 position;
        /// <summary>
        /// The speed that the character moves
        /// </summary>
        protected Vector2 speed;

        /// <summary>
        /// Constructor 1 (don't use)
        /// It is not implemented
        /// </summary>
        /// <param name="sprite">The animation sprite of the character</param>
        /// <param name="position">The position to place the character</param>
        /// <param name="speed">The movement speed of the character</param>
        public Character(AnimatedSprite sprite, Vector2 position, Vector2 speed) : this(sprite, position, 0, speed) { }
        /// <summary>
        /// Constructor 2
        /// </summary>
        /// <param name="sprite">The animation sprite of the character</param>
        /// <param name="position">The position of the character on the map</param>
        /// <param name="numCoins">The number of coins the character starts off </param>
        /// <param name="speed">The movement speed of the character.</param>
        public Character(AnimatedSprite sprite, Vector2 position, int numCoins, Vector2 speed)
        {
            this.sprite = sprite;
            this.position = position;
            this.numCoins = 0;
            this.speed = speed;
        }

        /// <summary>
        /// This moves a character if there were no collissions within the map.
        /// </summary>
        /// <param name="direction">The direction to move the character (uses the enum below)</param>
        /// <param name="currentMap">The map to check if the character has collided with an object</param>
        /// <param name="hasCollided">This is the value it returns if the character collided</param>
        /// <returns>True if the character collided, false otherwise</returns>
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
        /// <summary>
        /// This is an enumeration of movements
        /// </summary>
        public enum MoveDirection
        {
            Down = 0,
            Left = 1,
            Right = 2,
            Up = 3,
            None = -1
        }
        /// <summary>
        /// This updates the character's state
        /// </summary>
        /// <param name="gameTime">The game time to use for animation stuff</param>
        /// <param name="moveDirection">The movement direction (I.E input from the player)</param>
        /// <param name="currentMap">The map the currect character is on</param>
        public virtual void Update(GameTime gameTime, MoveDirection moveDirection, Map currentMap)
        {
            bool hasCollided;
            bool hasMoved = Move(moveDirection, currentMap, out hasCollided);

            sprite.Update(gameTime, moveDirection, hasCollided);
        }
        /// <summary>
        /// This draws the character to the map
        /// </summary>
        /// <param name="spriteBatch">The spritebatch object used for drawing.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, position, DEFAULT_LAYER_DEPTH);
        }

        public Rectangle getRectangle()
        {
            return new Rectangle((int)position.X, (int)position.Y, sprite.FrameSize.X, sprite.FrameSize.Y);
        }
    }
}
