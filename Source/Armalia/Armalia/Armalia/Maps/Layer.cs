using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Armalia.Sprites;
using Microsoft.Xna.Framework;

namespace Armalia.Maps
{
    /// <summary>
    /// This contains the an array of sprites and their locations
    /// </summary>
    class Layer : Map
    {
        private ObjectSprite[,] objectMap;
        private static int tileHeight = 32;
        private static int tileWidth = 32;
        public Layer(int w, int h, Texture2D img, ObjectSprite[,] os)
            : base(w, h, img)
        {
            this.objectMap = os;

        }

        //public override void Draw(SpriteBatch spriteBatch, int zIndex, int firstX, int firstY, int height, int width)
        //{
        //    int spX = (int)this.sourceImage.Width / tileWidth;
        //    int spY = (int)this.sourceImage.Height / tileHeight;
        //    Vector2 curPos = Vector2.Zero;
        //    if ((firstX + width) > this.objectMap.GetLength(0))
        //    {
        //        firstX = this.objectMap.GetLength(0) - width;
        //    }
        //    if ((firstY + height) > this.objectMap.GetLength(1))
        //    {
        //        firstY = this.objectMap.GetLength(1) - height;
        //    }
        //    if (firstY < 0)
        //    {
        //        firstY = 0;
        //    }
        //    if (firstX < 0)
        //    {
        //        firstX = 0;
        //    }
        //    for (int x = (0 + firstX); x < (firstX + width); x++)
        //    {
        //        for (int y = (0 + firstY); y < (firstY + height); y++)
        //        {
        //            ObjectSprite os = objectMap[x, y];
        //            if (os != null)
        //            {
        //                Rectangle r = new Rectangle((int)os.MapPosition.X,
        //                                                (int)os.MapPosition.Y,
        //                                                tileHeight,
        //                                                tileWidth);
        //                spriteBatch.Draw(this.sourceImage, curPos,
        //                          r,
        //                          Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, (0.1f * (zIndex + 7)));
        //            }
        //            curPos.Y += tileWidth;
        //        }
        //        curPos.X += tileHeight;
        //        curPos.Y = 0;
        //    }

        //}
        //END OF CLASS
    }
}
