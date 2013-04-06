using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Sprites;
using Microsoft.Xna.Framework;
using Armalia.Maps;
using Microsoft.Xna.Framework.Graphics;

namespace Armalia.Characters
{
    class MainCharacter : CombatableCharacter
    {
        private Rectangle cameraView;

        public MainCharacter(AnimatedSprite sprite, Vector2 position, int hitPoints, int manaPoints,
            int expLevel, int strength, int defense, Vector2 speed, Rectangle cameraView)
            : base(sprite, position, hitPoints, manaPoints, expLevel, strength, defense, speed)
        {
            this.cameraView = cameraView;
        }

        public void LevelUp() { }

        public Rectangle CameraView
        {
            get { return cameraView; }
            private set { cameraView = value; }
        }

        public override bool Move(MoveDirection direction, Map currentMap, out bool hasCollided)
        {
            bool hasMoved = base.Move(direction, currentMap, out hasCollided);
            if (hasMoved)
            {

                // center x coord of camera
                if (position.X + (sprite.FrameSize.X / 2) <= (cameraView.Width / 2))
                {
                    // center camera to left edge
                    cameraView.X = 0;
                }
                else if (position.X + (sprite.FrameSize.X / 2) >= currentMap.Size.X - (cameraView.Width / 2))
                {
                    // center camera to right edge
                    cameraView.X = currentMap.Size.X - cameraView.Width;

                }
                else
                {
                    // center camera x coord to player position
                    cameraView.X = (int)position.X - (cameraView.Width / 2) + (sprite.FrameSize.X / 2);
                }

                // center y cood of camera
                if (position.Y + (sprite.FrameSize.Y / 2) <= (cameraView.Height / 2))
                {
                    // center camera to top edge
                    cameraView.Y = 0;
                }
                else if (position.Y + (sprite.FrameSize.Y / 2) >= currentMap.Size.Y - (cameraView.Height / 2))
                {
                    // center camera to bottom edge
                    cameraView.Y = currentMap.Size.Y - cameraView.Height;
                }
                else
                {
                    // center camera y coord to player position
                    cameraView.Y = (int)position.Y - (cameraView.Height / 2) + (sprite.FrameSize.Y / 2);
                }
            }
            return hasMoved;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int xOffset = (int)position.X - CameraView.X;
            int yOffset = (int)position.Y - CameraView.Y;
            Vector2 drawPosition = new Vector2(xOffset, yOffset);
            // normalize position relative to camera view
            sprite.Draw(spriteBatch, drawPosition, DEFAULT_LAYER_DEPTH);
        }

        enum StatusEffect
        {
            Cursed
        }
    }
}
