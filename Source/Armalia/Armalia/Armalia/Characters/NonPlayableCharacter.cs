using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Sprites;
using Microsoft.Xna.Framework;

namespace Armalia.Characters
{
    class NonPlayableCharacter : Character
    {
        public NonPlayableCharacter(AnimatedSprite sprite, Vector2 position, Vector2 speed) : base(sprite, position, speed) { }
    }
}
