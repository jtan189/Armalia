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
        public Vector2 mapPosition;
        protected bool isSolid;

        public Sprite(Vector2 pos, bool s)
        {
            this.mapPosition = pos;
            this.isSolid = s;
        }
        public Sprite() { }
         public Vector2 MapPosition
         {
             get { return this.mapPosition; }
       
        }
    }
}
