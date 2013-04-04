using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

// TODO: Have Map represent all of map stuff. Level contains Map. Map contain Layers (if want), rather just object with layerValue.
namespace Armalia.Maps
{
    ///<summary>
    ///This will be our town, dungeon, inside a house, etc. 
    ///</summary>
    public class GameLevel
    {
        private Map[] maps;
        private int width;
        private int height;
        private int xOffset;
        private int yOffset;
        private Rectangle mapBounds;

        public GameLevel(Map[] m, int h, int w, Rectangle mapBounds)
        {
            this.maps = m;
            this.width = w;
            this.height = h;
            this.xOffset = 0;
            this.yOffset = 0;
            this.mapBounds = mapBounds;
        }

        public Rectangle MapBounds
        {
            get { return mapBounds; }
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
