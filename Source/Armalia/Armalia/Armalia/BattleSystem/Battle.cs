using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Characters;

namespace Armalia.BattleSystem
{
    public class Battle
    {
        private MainCharacter mainCharacter;
        public EnemyCharacter Enemy {get; set;}
        public bool IsPlayerTurn {get; set;}

        public Battle(MainCharacter mainCharacter, EnemyCharacter enemy)
        {
            this.mainCharacter = mainCharacter;
            this.Enemy = enemy;
            IsPlayerTurn = true;
        }



        public void ExecutePlayerAttack(out bool enemyDead)
        {
            mainCharacter.Attack(Enemy, out enemyDead);   
        }
    }
}
