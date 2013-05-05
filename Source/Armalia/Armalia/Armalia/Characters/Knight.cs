using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Sprites;
using Microsoft.Xna.Framework;
using Armalia.GameScreens;
using Microsoft.Xna.Framework.Graphics;
using Armalia.Levels;

namespace Armalia.Characters
{
    class Knight : EnemyCharacter
    {

        public Knight(AnimatedSprite sprite, Vector2 position, int hitPoints, int manaPoints,
            int expLevel, int strength, int defense, Vector2 speed, List<Vector2> patrolTargets, MainCharacter mainCharacter,
            GameplayScreen gameplayScreen)
            : base(sprite, position, hitPoints, manaPoints, expLevel, strength, defense, speed,
            patrolTargets, mainCharacter, gameplayScreen)
        {

        }

        public override void Update(GameTime gameTime, GameLevel currentLevel)
        {
            if (currentState == EnemyState.Battle)
            {
                Sword.InitiateAttack();
            }

            Sword.Update(gameTime, currentLevel);
            base.Update(gameTime, currentLevel);
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
