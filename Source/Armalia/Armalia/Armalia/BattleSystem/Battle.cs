using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Armalia.Levels;

namespace Armalia.BattleSystem
{
    class Battle
    {
        private GameLevel level;
        private MainCharacter mainCharacter;

        public EnemyCharacter Enemy { get; set; }
        public bool IsPlayerTurn { get; set; }

        private bool attackInProgress;

        public Battle(MainCharacter mainCharacter, EnemyCharacter enemy, GameLevel level)
        {
            this.mainCharacter = mainCharacter;
            this.Enemy = enemy;
            this.level = level;
            attackInProgress = false;
            IsPlayerTurn = true;
        }

        public void InitiatePlayerAttack()
        {
            attackInProgress = true;
            mainCharacter.Attack(Enemy);
        }

        //public void Update(GameTime gameTime, out bool battleOver)
        //{

            //if (Enemy.HitPoints <= 0)
            //{
            //    level.RemoveEnemy(Enemy);
            //    battleOver = true;
            //}
            //else
            //{
            //    battleOver = false;
            //}

            //if (IsPlayerTurn && attackInProgress)
            //{
                //bool attackFinished;
                //mainCharacter.Sword.Update(gameTime, out attackFinished);

                //if (attackFinished)
                //{
                //    attackInProgress = false;
                //}
            //}

        //}

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsPlayerTurn && attackInProgress)
            {
                Vector2 handPosition = new Vector2(mainCharacter.CameraRelativePosition.X + mainCharacter.CharacterSprite.FrameSize.X / 2.0f,
                    mainCharacter.CameraRelativePosition.Y + mainCharacter.CharacterSprite.FrameSize.Y * (2 / 3.0f));
                mainCharacter.Sword.Draw(spriteBatch, handPosition);
            }
        }
    }
}
