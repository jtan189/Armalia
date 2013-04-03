using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Armalia.Mapping
{
  public  class Level
    {
        private Map[] maps;
        private int width;
        private int height;
        private int xOffset;
        private int yOffset;
        public Level(Map[] m, int h, int w)
        {
            this.maps = m;
            this.width = w;
            this.height = h;
            this.xOffset = 0;
            this.yOffset = 0;
        }

        public void Draw(SpriteBatch sb)
        {
            for (int x = 0; x < this.maps.GetLength(0); x++)
            {
                Map m = this.maps[x];
                m.Draw(sb, x, this.xOffset, this.yOffset, this.height, this.width);

            }
        }

        public void MoveMap(int x, int y)
        {
            
            this.xOffset += x;
            this.yOffset += y;
        }

    }
}
