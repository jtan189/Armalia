using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Armalia.Maps
{
    public class Map
    {
        public const float MAY_LAYER_VALUE = 1f;

        private Texture2D mapImage;
        private List<Rectangle> boundaries;

        public Map(Texture2D mapImage, List<Rectangle> boundaries)
        {
            this.mapImage = mapImage;
            this.boundaries = boundaries;
        }

        public Point Size { get { return new Point(mapImage.Width, mapImage.Height); } }

        public void Draw(SpriteBatch spriteBatch, Rectangle mapWindow, Rectangle cameraView)
        {
            spriteBatch.Draw(mapImage, mapWindow, cameraView, Color.White, 0f, Vector2.Zero, SpriteEffects.None, MAY_LAYER_VALUE);
        }

        public bool CollidesWithBoundary(Rectangle characterRect)
        {
            foreach (Rectangle boundary in boundaries) {
                if (boundary.Intersects(characterRect)) {
        
                    return true;
                }
            }
            return false;
        }

    }
}
