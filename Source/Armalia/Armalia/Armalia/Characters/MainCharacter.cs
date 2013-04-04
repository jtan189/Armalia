using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Sprites;
using Microsoft.Xna.Framework;

namespace Armalia.Characters
{
    class MainCharacter : CombatableCharacter
    {
        enum StatusEffect
        {
            Cursed
        }

        public MainCharacter(AnimatedSprite sprite, Vector2 position, int hitPoints, int manaPoints,
            int expLevel, int strength, int defense, Vector2 speed)
            : base(sprite, position, hitPoints, manaPoints, expLevel, strength, defense, speed) { }

        public void LevelUp() { }
    }
}
