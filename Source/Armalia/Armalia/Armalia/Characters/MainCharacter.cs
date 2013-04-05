using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Sprites;
using Microsoft.Xna.Framework;
using Armalia.Maps;

namespace Armalia.Characters
{
    class MainCharacter : CombatableCharacter
    {
        private Point cameraSize;
        private GameLevel level;

        public MainCharacter(AnimatedSprite sprite, Vector2 position, int hitPoints, int manaPoints,
            int expLevel, int strength, int defense, Vector2 speed, Point cameraSize, GameLevel level)
            : base(sprite, position, hitPoints, manaPoints, expLevel, strength, defense, speed)
        {
            this.cameraSize = cameraSize;
            this.level = level;
        }

        public void LevelUp() { }

        public override void Move(MoveDirection direction, Point mapSize)
        {
            base.Move(direction, mapSize);

            Point cameraOffset = new Point();

            // center horizontally if not near edge
            if (position.X > (cameraSize.X / 2) - (sprite.FrameSize.X / 2) &&
                position.X < mapSize.X - (cameraSize.X / 2) - (sprite.FrameSize.X / 2))
            {   
                switch (direction)
                {
                    case MoveDirection.Left:
                        cameraOffset.X -= (int) speed.X;
                        break;
                    case MoveDirection.Right:
                        cameraOffset.X += (int)speed.X;
                        break;
                }
            }

            // center vertically if not near edge
            if (position.Y > (cameraSize.Y / 2) - (sprite.FrameSize.Y / 2) &&
                position.Y < mapSize.Y - (cameraSize.Y / 2) - (sprite.FrameSize.Y / 2))
            {
                switch (direction)
                {
                    case MoveDirection.Up:
                        cameraOffset.Y -= (int)speed.Y;
                        break;
                    case MoveDirection.Down:
                        cameraOffset.Y += (int)speed.Y;
                        break;
                }
            }

            // NOT WORKING - problem mapping drawing 32x32 tiles to continuous movement
            //level.MoveCamera(cameraOffset);
        }

        enum StatusEffect
        {
            Cursed
        }
    }
}
