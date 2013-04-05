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

        public Rectangle CameraView { get { return cameraView; } }

        public override void Move(MoveDirection direction, Point mapSize)
        {
            base.Move(direction, mapSize);

            // center horizontally if not near edge
            if (position.X > (cameraView.Width / 2) - (sprite.FrameSize.X / 2) &&
                position.X < mapSize.X - (cameraView.Width / 2) - (sprite.FrameSize.X / 2))
            {   
                switch (direction)
                {
                    case MoveDirection.Left:
                        cameraView.X -= (int) speed.X;
                        //// quick fix
                        //base.Move(MoveDirection.Right, mapSize);
                        break;
                    case MoveDirection.Right:
                        //base.Move(MoveDirection.Left, mapSize);
                        cameraView.X += (int)speed.X;
                        break;
                }
            }

            // center vertically if not near edge
            if (position.Y > (cameraView.Height / 2) - (sprite.FrameSize.Y / 2) &&
                position.Y < mapSize.Y - (cameraView.Height / 2) - (sprite.FrameSize.Y / 2))
            {
                switch (direction)
                {
                    case MoveDirection.Up:
                        cameraView.Y -= (int)speed.Y;
                        //base.Move(MoveDirection.Down, mapSize);
                        break;
                    case MoveDirection.Down:
                        cameraView.Y += (int)speed.Y;
                        //base.Move(MoveDirection.Up, mapSize);
                        break;
                }
            }
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
