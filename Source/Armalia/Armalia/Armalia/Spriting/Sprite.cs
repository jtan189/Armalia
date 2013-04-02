using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Armalia.Spriting
{
    abstract class Sprite
    {
        Texture2D spriteImage;
        private Boolean isSolid;
        private double collisionOffset;

        public Sprite()
        {

        }
    }
}
