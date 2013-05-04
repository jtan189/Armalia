using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Sprites;
using Microsoft.Xna.Framework;
using Armalia.Levels;
using Microsoft.Xna.Framework.Graphics;

namespace Armalia.Characters
{
    class MainCharacter : CombatableCharacter
    {
        /// <summary>
        /// This is a subjection of the map. This is what dictates what the player/character
        /// can see.
        /// </summary>
        private Rectangle cameraView;

        private bool attackInProgress;

        public String Name { get; set; }
        public SwordSprite Sword { get; set; }

        /// <summary>
        /// The view of the map the character has
        /// </summary>
        public Rectangle CameraView
        {
            get { return cameraView; }
            private set { cameraView = value; }
        }

        public Vector2 CameraRelativePosition
        {
            get { return new Vector2(Position.X - CameraView.X, Position.Y - CameraView.Y); }
        }

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
        /// <param name="cameraView">The subsection of the map to the player can see.</param>
        /// <param name="swordSprite">Sword sprite associated with the main character.</param>
        public MainCharacter(String name, AnimatedSprite sprite, Vector2 position, int hitPoints, int manaPoints,
            int expLevel, int strength, int defense, Vector2 speed, Rectangle cameraView, SwordSprite swordSprite)
            : base(sprite, position, hitPoints, manaPoints, expLevel, strength, defense, speed)
        {
            this.Name = name;
            this.cameraView = cameraView;
            this.Sword = swordSprite;
            attackInProgress = false;
        }

        /// <summary>
        /// This is implemented for level up
        /// </summary>
        public void LevelUp() { }

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
            if (hasMoved)
            {
                // center x coord of camera
                if (Position.X + (CharacterSprite.FrameSize.X / 2) <= (cameraView.Width / 2))
                {
                    // center camera to left edge
                    cameraView.X = 0;
                }
                else if (Position.X + (CharacterSprite.FrameSize.X / 2) >= currentMap.Size.X - (cameraView.Width / 2))
                {
                    // center camera to right edge
                    cameraView.X = currentMap.Size.X - cameraView.Width;

                }
                else
                {
                    // center camera x coord to player position
                    cameraView.X = (int)Position.X - (cameraView.Width / 2) + (CharacterSprite.FrameSize.X / 2);
                }

                // center y cood of camera
                if (Position.Y + (CharacterSprite.FrameSize.Y / 2) <= (cameraView.Height / 2))
                {
                    // center camera to top edge
                    cameraView.Y = 0;
                }
                else if (Position.Y + (CharacterSprite.FrameSize.Y / 2) >= currentMap.Size.Y - (cameraView.Height / 2))
                {
                    // center camera to bottom edge
                    cameraView.Y = currentMap.Size.Y - cameraView.Height;
                }
                else
                {
                    // center camera y coord to player position
                    cameraView.Y = (int)Position.Y - (cameraView.Height / 2) + (CharacterSprite.FrameSize.Y / 2);
                }
            }
            return hasMoved;
        }

        public void Update(GameTime gameTime, MoveDirection moveDirection, Map currentMap, bool playerPressedAttack)
        {
            if (!Sword.Animating)
            {
                // Attack Enemy
            }

            Sword.Update(gameTime, playerPressedAttack);
            base.Update(gameTime, moveDirection, currentMap);
        }

        /// <summary>
        /// This draws the character
        /// </summary>
        /// <param name="spriteBatch">The spritebatch to use to draw</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            int xOffset = (int)Position.X - CameraView.X;
            int yOffset = (int)Position.Y - CameraView.Y;
            Vector2 drawPosition = new Vector2(xOffset, yOffset);

            // normalize position relative to camera view
            CharacterSprite.Draw(spriteBatch, drawPosition, DEFAULT_LAYER_DEPTH);

            if (Sword.Animating)
            {
                Vector2 rotationOrigin = new Vector2(CameraRelativePosition.X + CharacterSprite.FrameSize.X / 2.0f,
                        CameraRelativePosition.Y + CharacterSprite.FrameSize.Y / 2.0f);
                Sword.Draw(spriteBatch, rotationOrigin);
            }
        }

    }
}
