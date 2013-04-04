using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Sprites;
using Microsoft.Xna.Framework.Graphics;

namespace Armalia.Maps
{
    public abstract class Map
    {
        protected int width;
        protected int height;
        protected Texture2D sourceImage;
        public Map(int w, int h, Texture2D img)
        {
            this.width = w;
            this.height = h;
            this.sourceImage = img;
        }

        public abstract void Draw(SpriteBatch sb, int zindex, int firstX, int firstY, int mapHeight, int mapWidth);

    }
}
