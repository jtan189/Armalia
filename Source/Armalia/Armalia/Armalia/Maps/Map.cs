using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Armalia.Maps
{
    public abstract class Map
    {
        protected int width;
        protected int height;

        protected Texture2D sourceImage;
        protected Rectangle mapWindow;

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

        public Map(int w, int h, Texture2D img)
        {
            this.width = w;
            this.height = h;
            this.sourceImage = img;
        }

        //public abstract void Draw(SpriteBatch spriteBatch, Nullable<Rectangle> cameraRectangle);

        public abstract void Draw(SpriteBatch sb, int zindex, int firstX, int firstY, int mapHeight, int mapWidth);

    }
}
