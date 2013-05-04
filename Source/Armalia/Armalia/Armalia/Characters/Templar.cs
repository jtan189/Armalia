using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Sprites;
using Microsoft.Xna.Framework;
using Armalia.GameScreens;

namespace Armalia.Characters
{
    class Templar : EnemyCharacter
    {
        public Templar(AnimatedSprite sprite, Vector2 position, int hitPoints, int manaPoints,
            int expLevel, int strength, int defense, Vector2 speed, GameplayScreen gameplayScreen, List<Point> patrolTargets, MainCharacter mainCharacter)
            : base(sprite, position, hitPoints, manaPoints, expLevel, strength, defense, speed, gameplayScreen, patrolTargets, mainCharacter)
        {

        }
    }
}
