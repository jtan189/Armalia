using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Armalia.Sprites
{
    abstract class Sprite
    {
        protected Texture2D texture;
        protected Point frameSize;

        private Vector2 mapPosition; // delete
        protected bool isSolid; // delete

        public Sprite(Texture2D texture, Point frameSize) {
            this.texture = texture;
            this.frameSize = frameSize;
        }

        // delete? - position handled by containing object, isSolid is implied by subclass
        public Sprite(Vector2 mapPosition, bool isSolid)
        {
            this.mapPosition = mapPosition;
            this.isSolid = isSolid;
        }

        public Point FrameSize
        {
            get { return frameSize; }
        }

        // delete - position handled by containing object
        public Vector2 MapPosition
        {
            get { return this.mapPosition; }
        }

        public abstract void Draw(SpriteBatch spriteBatch, Vector2 spritePosition, float layerDepth);
        
    }
}
