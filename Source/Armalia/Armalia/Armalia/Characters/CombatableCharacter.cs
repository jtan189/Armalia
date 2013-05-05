using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Armalia.GameScreens;

namespace Armalia.Characters
{
    abstract class CombatableCharacter : Character
    {
        public int HitPoints { get; set; }
        public int ManaPoints { get; set; }
        public int ExpLevel { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }

        public Color[] TextureData { get; set; }
        public SwordSprite Sword { get; set; }
        public bool IsInPain { get; set; }

        public CombatableCharacter(AnimatedSprite sprite, Vector2 position, int hitPoints, int manaPoints,
            int expLevel, int strength, int defense, Vector2 speed, GameplayScreen gameplayScreen)
            : base(sprite, position, speed, gameplayScreen)
        {
            this.HitPoints = hitPoints;
            this.ManaPoints = manaPoints;
            this.ExpLevel = expLevel;
            this.Strength = strength;
            this.Defense = defense;
            IsInPain = false;

            TextureData =
                new Color[CharacterSprite.Texture.Width * CharacterSprite.Texture.Height];
            CharacterSprite.Texture.GetData(TextureData);
        }

        public void Attack(CombatableCharacter enemy)
        {
            int damage = (int)(Strength - (0.5 * enemy.Defense));
            enemy.HitPoints -= damage;
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle cameraView)
        {
            Color tint = IsInPain ? Color.Red : Color.White;
            CharacterSprite.Draw(spriteBatch, DrawPosition(cameraView), DEFAULT_LAYER_DEPTH, tint);
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
