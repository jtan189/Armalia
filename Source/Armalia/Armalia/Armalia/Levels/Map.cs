using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Armalia.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Armalia.Levels
{
    class Map
    {
        public const float MAP_LAYER_VALUE = 1f;

        private List<Rectangle> boundaries;

        public Texture2D MapImage { get; set; }
        public Point Size { get { return new Point(MapImage.Width, MapImage.Height); } }

        public Map(Texture2D mapImage, List<Rectangle> boundaries)
        {
            this.MapImage = mapImage;
            this.boundaries = boundaries;
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle mapWindow, Rectangle cameraView)
        {
            spriteBatch.Draw(MapImage, mapWindow, cameraView, Color.White, 0f, Vector2.Zero, SpriteEffects.None, MAP_LAYER_VALUE);
        }
        public List<Rectangle> GetBounds()
        {
            return this.boundaries;
        }

        public bool CollidesWithBoundary(Rectangle characterRect)
        {
            foreach (Rectangle boundary in boundaries)
            {
                if (boundary.Intersects(characterRect))
                {

                    return true;
                }
            }
            return false;
        }

    }
}
