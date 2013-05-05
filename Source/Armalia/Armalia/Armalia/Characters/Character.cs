using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Armalia.Levels;
using Armalia.GameScreens;

namespace Armalia.Characters
{
    /// <summary>
    /// This is the base class for characters. Characters are your main character, enemies, etc
    /// </summary>
    abstract class Character
    {
        /// <summary>
        /// The default Layer that they're drawn at
        /// </summary>
        public const float DEFAULT_LAYER_DEPTH = 0.5F;

        /// <summary>
        /// This is used for currency.
        /// </summary>
        private int numCoins;

        /// <summary>
        /// The speed that the character moves
        /// </summary>
        protected Vector2 speed;

        protected GameplayScreen gameplayScreen;

        public Rectangle CameraView {
            get { return gameplayScreen.CameraView; }
            set { gameplayScreen.CameraView = value; }
        }

        /// <summary>
        /// The animated sprite that defines their looks and animations
        /// </summary>
        public AnimatedSprite CharacterSprite { get; set; }

        /// <summary>
        /// This is the location of the character on the map
        /// </summary>
        public Vector2 Position { get; set; }

        public Rectangle AsRectangle()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, CharacterSprite.FrameSize.X, CharacterSprite.FrameSize.Y);
        }

        /// <summary>
        /// Constructor 1
        /// </summary>
        /// <param name="sprite">The animation sprite of the character</param>
        /// <param name="position">The position to place the character</param>
        /// <param name="speed">The movement speed of the character</param>
        /// <param name="gameplayScreen">The gameplay screen for the game.</param>
        public Character(AnimatedSprite sprite, Vector2 position, Vector2 speed, GameplayScreen gameplayScreen)
            : this(sprite, position, 0, speed, gameplayScreen) { }

        /// <summary>
        /// Constructor 2
        /// </summary>
        /// <param name="sprite">The animation sprite of the character</param>
        /// <param name="position">The position of the character on the map</param>
        /// <param name="numCoins">The number of coins the character starts off </param>
        /// <param name="speed">The movement speed of the character.</param>
        /// <param name="gameplayScreen">The gameplay screen for the game.</param>
        public Character(AnimatedSprite sprite, Vector2 position, int numCoins, Vector2 speed, GameplayScreen gameplayScreen)
        {
            this.CharacterSprite = sprite;
            this.Position = position;
            this.numCoins = numCoins;
            this.speed = speed;
            this.gameplayScreen = gameplayScreen;
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
                Vector2 movedPosition = Position;

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
                    (movedPosition.X > currentMap.Size.X - CharacterSprite.FrameSize.X) ||
                    (movedPosition.Y > currentMap.Size.Y - CharacterSprite.FrameSize.Y))
                {
                    outOfBounds = true;
                }

                // TODO: use better collision offsets
                // check if movement would result in boundary collision
                Rectangle movedRectangle = new Rectangle(
                    (int)movedPosition.X, (int)movedPosition.Y, CharacterSprite.FrameSize.X, CharacterSprite.FrameSize.Y);
                hasCollided = currentMap.CollidesWithBoundary(movedRectangle);

                if (!(outOfBounds || hasCollided))
                {
                    Position = movedPosition;
                    hasMoved = true;
                }
            }

            return hasMoved;
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

            CharacterSprite.Update(gameTime, moveDirection, hasCollided);
        }

        public Vector2 DrawPosition(Rectangle cameraView)
        {
            int xOffset = (int)Position.X - cameraView.X;
            int yOffset = (int)Position.Y - cameraView.Y;
            return new Vector2(xOffset, yOffset);
        }

        /// <summary>
        /// This draws the character to the map
        /// </summary>
        /// <param name="spriteBatch">The spritebatch object used for drawing.</param>
        /// <param name="cameraView">Camera view for the player.</param>
        public virtual void Draw(SpriteBatch spriteBatch, Rectangle cameraView)
        {
            CharacterSprite.Draw(spriteBatch, DrawPosition(cameraView), DEFAULT_LAYER_DEPTH, Color.White);
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
    }
}
