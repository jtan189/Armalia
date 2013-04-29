using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Sprites;
using Microsoft.Xna.Framework;

namespace Armalia.Characters
{
    public abstract class CombatableCharacter : Character
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
            HitPoints = hitPoints;
            ManaPoints = manaPoints;
            ExpLevel = expLevel;
            Strength = strength;
            Defense = defense;
        }

        public void Attack(CombatableCharacter enemy) { }

        
    }
}
