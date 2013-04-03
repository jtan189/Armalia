using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Armalia.Spriting;
using Microsoft.Xna.Framework;

namespace Armalia.Mapping
{
    class Layer : Map
    {
        private ObjectSprite[,] objectMap;
        private static int tileHeight = 32;
        private static int tileWidth = 32;
        public Layer(int w, int h, Texture2D img, ObjectSprite[,] os) : base(w, h, img)
        {
            this.objectMap = os;
            
        }


 
    public override void Draw(SpriteBatch sb, int zindex,int firstX, int firstY, int mapHeight, int mapWidth)
    {
        int spX = (int)this.sourceImage.Width / tileWidth;
        int spY = (int)this.sourceImage.Height / tileHeight;
        Vector2 curPos = Vector2.Zero;
        if ( (firstX + mapWidth) > this.objectMap.GetLength(0) )
        {
            firstX = this.objectMap.GetLength(0) - mapWidth;
        }
        if(  (firstY + mapHeight) > this.objectMap.GetLength(1) )
        {
            firstY = this.objectMap.GetLength(1) - mapHeight;
        }
        if(firstY < 0) 
        {
            firstY=0;
        }
        if (firstX < 0)
        {
            firstX=0;
        }
        for (int x = (0+firstX); x < (firstX + mapWidth); x++)
        {
            for (int y = (0+firstY); y < (firstY + mapHeight); y++)
            {
                ObjectSprite os = objectMap[x, y];
                if (os != null)
                {
                    Rectangle r = new Rectangle((int)os.MapPosition.X,
                                                    (int)os.MapPosition.Y,
                                                    tileHeight,
                                                    tileWidth);
                    sb.Draw(this.sourceImage, curPos,
                              r,
                              Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, (0.1f * (zindex+2)));
                }
                curPos.Y += tileWidth;
            }
            curPos.X += tileHeight;
            curPos.Y = 0;
        }

    }
//END OF CLASS
    }
}
