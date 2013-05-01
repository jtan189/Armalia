using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Armalia.Characters;

namespace Armalia.Object
{
    public abstract class LevelObject
    {
        protected Rectangle objectRect;

        public LevelObject(Rectangle objectRect)
        {
            this.objectRect = objectRect;
        }

        public bool Collides(MainCharacter mainChar)
        {
            return objectRect.Intersects(mainChar.getRectangle());
        }
    }
}
