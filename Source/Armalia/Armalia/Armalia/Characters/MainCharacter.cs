using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Sprites;
using Microsoft.Xna.Framework;
using Armalia.Levels;
using Microsoft.Xna.Framework.Graphics;
using Armalia.GameScreens;

namespace Armalia.Characters
{
    class MainCharacter : CombatableCharacter
    {

        public String Name { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="name">Name of the main character.</param>
        /// <param name="sprite">The animated sprite object for the main chracter.</param>
        /// <param name="position">The initial position of the character.</param>
        /// <param name="hitPoints">The number of hitpoints (life) the character has</param>
        /// <param name="manaPoints">The number of mana points the character has</param>
        /// <param name="expLevel">The number of EXP to level up</param>
        /// <param name="strength">The strength attribute</param>
        /// <param name="defense">The defense attribute</param>
        /// <param name="speed">The movement speed of the character</param>
        /// <param name="gameplayScreen">The gameplay screen for the game.</param>
        public MainCharacter(String name, AnimatedSprite sprite, Vector2 position, int hitPoints, int manaPoints,
            int expLevel, int strength, int defense, Vector2 speed, GameplayScreen gameplayScreen)
            : base(sprite, position, hitPoints, manaPoints, expLevel, strength, defense, speed, gameplayScreen)
        {
            this.Name = name;
        }

        /// <summary>
        /// This is implemented for level up
        /// </summary>
        public void LevelUp() { }

        public void SetPosition(Vector2 newPosition, Map map)
        {
            Position = newPosition;
            Rectangle newCameraView = CameraView;
            int cx = (int)Position.X - (CameraView.Width / 2) + (CharacterSprite.FrameSize.X / 2);
            int cy = (int)Position.Y - (CameraView.Height / 2) + (CharacterSprite.FrameSize.Y / 2);
            if ((Position.Y + (CharacterSprite.FrameSize.Y / 2) >= map.Size.Y - (CameraView.Height / 2)))
            {
                newCameraView.Y = map.Size.Y - CameraView.Height;
            }

            if ((Position.X + (CharacterSprite.FrameSize.X / 2) >= map.Size.X - (CameraView.Width / 2)))
            {
                newCameraView.X = map.Size.X - CameraView.Width;

            }

            // center y cood of camera
            if (Position.Y + (CharacterSprite.FrameSize.Y / 2) <= (CameraView.Height / 2))
            {
                // center camera to top edge
                newCameraView.Y = 0;
            }
            if (Position.X + (CharacterSprite.FrameSize.X / 2) <= (CameraView.Width / 2))
            {
                // center camera to left edge
                newCameraView.X = 0;
            }

            CameraView = newCameraView;
        }

        /// <summary>
        /// This uses input of the player to move the character
        /// </summary>
        /// <param name="direction">The direction to move the character</param>
        /// <param name="currentMap">The current map the character is on</param>
        /// <param name="hasCollided">Has it collided with another object</param>
        /// <returns>This returns true if the character has moved (i.e no collisson)</returns>
        public override bool Move(MoveDirection direction, Map currentMap, out bool hasCollided)
        {
            bool hasMoved = base.Move(direction, currentMap, out hasCollided);
            Rectangle newCameraView = CameraView;
            if (hasMoved)
            {
                // center x coord of camera
                if (Position.X + (CharacterSprite.FrameSize.X / 2) <= (CameraView.Width / 2))
                {
                    // center camera to left edge
                    newCameraView.X = 0;
                }
                else if (Position.X + (CharacterSprite.FrameSize.X / 2) >= currentMap.Size.X - (CameraView.Width / 2))
                {
                    // center camera to right edge
                    newCameraView.X = currentMap.Size.X - CameraView.Width;

                }
                else
                {
                    // center camera x coord to player position
                    newCameraView.X = (int)Position.X - (CameraView.Width / 2) + (CharacterSprite.FrameSize.X / 2);
                }

                // center y cood of camera
                if (Position.Y + (CharacterSprite.FrameSize.Y / 2) <= (CameraView.Height / 2))
                {
                    // center camera to top edge
                    newCameraView.Y = 0;
                }
                else if (Position.Y + (CharacterSprite.FrameSize.Y / 2) >= currentMap.Size.Y - (CameraView.Height / 2))
                {
                    // center camera to bottom edge
                    newCameraView.Y = currentMap.Size.Y - CameraView.Height;
                }
                else
                {
                    // center camera y coord to player position
                    newCameraView.Y = (int)Position.Y - (CameraView.Height / 2) + (CharacterSprite.FrameSize.Y / 2);
                }
            }

            CameraView = newCameraView;
            return hasMoved;
        }

        public void Update(GameTime gameTime, MoveDirection moveDirection, GameLevel currentLevel, bool playerPressedAttack)
        {
            if (playerPressedAttack)
            {
                Sword.InitiateAttack();
            }

            Sword.Update(gameTime, currentLevel);
            base.Update(gameTime, moveDirection, currentLevel.LevelMap);
        }

        /// <summary>
        /// This draws the character
        /// </summary>
        /// <param name="spriteBatch">The spritebatch to use to draw</param>
        /// <param name="cameraView">Camera view for the game.</param>
        public override void Draw(SpriteBatch spriteBatch, Rectangle cameraView)
        {
            base.Draw(spriteBatch, cameraView);

            if (Sword.Animating)
            {
                Vector2 rotationOrigin = new Vector2(gameplayScreen.CameraRelativePosition(Position).X + CharacterSprite.FrameSize.X / 2.0f,
                        gameplayScreen.CameraRelativePosition(Position).Y + CharacterSprite.FrameSize.Y / 2.0f);
                Sword.Draw(spriteBatch, rotationOrigin);
            }
        }

    }
}
