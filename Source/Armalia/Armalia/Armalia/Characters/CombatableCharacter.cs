using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Sprites;
using Microsoft.Xna.Framework;

namespace Armalia.Characters
{
    abstract class CombatableCharacter : Character
    {
        public int HitPoints { get; set; }
        public int ManaPoints { get; set; }
        public int ExpLevel { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }

        public CombatableCharacter(AnimatedSprite sprite, Vector2 position, int hitPoints, int manaPoints,
            int expLevel, int strength, int defense, Vector2 speed)
            : base(sprite, position, speed)
        {
            this.HitPoints = hitPoints;
            this.ManaPoints = manaPoints;
            this.ExpLevel = expLevel;
            this.Strength = strength;
            this.Defense = defense;
        }

        public void Attack(CombatableCharacter enemy) {
            int damage = (int) (Strength - (0.5 * enemy.Defense));
            enemy.HitPoints -= damage;
        }

        /// <summary>
        /// The enumeration used for drawing
        /// </summary>
        enum StatusEffect
        {
            Cursed
        }

    }
}
