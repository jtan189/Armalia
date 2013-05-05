using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Sprites;
using Microsoft.Xna.Framework;
using Armalia.GameScreens;

namespace Armalia.Characters
{
    class NonPlayableCharacter : Character
    {
        public NonPlayableCharacter(AnimatedSprite sprite, Vector2 position, Vector2 speed, GameplayScreen gameplayScreen)
            : base(sprite, position, speed, gameplayScreen) { }
    }
}
