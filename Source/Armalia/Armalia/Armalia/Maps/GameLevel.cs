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
        private Point mapSize;
        private Texture2D mapImage;


        public GameLevel(Texture2D mapImage) {
            MapImage = mapImage;
        }
        public GameLevel(Map[] m, int h, int w, Texture2D mapImage, Point mapSize)
        {
            this.maps = m;
            this.width = w;
            this.height = h;
            this.xOffset = 0;
            this.yOffset = 0;
            this.mapSize = mapSize;
            MapImage = mapImage;
        }

        public Point MapSize
        {
            get { return mapSize; }
        }

        public Texture2D MapImage { get { return mapImage; } set { mapImage = value; } }

        // use this:
        //
        //public void Draw (
        // Texture2D texture,
        // Rectangle destinationRectangle,
        // Nullable<Rectangle> sourceRectangle,
        // Color color,
        // float rotation,
        // Vector2 origin,
        // SpriteEffects effects,
        // float layerDepth
        //)

        public void Draw(SpriteBatch spriteBatch, Rectangle cameraView)
        {
            //for (int x = 0; x < this.maps.GetLength(0); x++)
            //{
            //    Map m = this.maps[x];
            //    m.Draw(sb, x, this.xOffset, this.yOffset, this.height, this.width);
            //}

            spriteBatch.Draw(MapImage,
            new Rectangle(0, 0, 800, 800), cameraView, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
        }

        //public void MoveCamera(Point offset)
        //{

        //    this.xOffset += offset.X;
        //    this.yOffset += offset.Y;
        //}

        //public void MoveMap(int x, int y)
        //{

        //    this.xOffset += x;
        //    this.yOffset += y;
        //}

    }
}
