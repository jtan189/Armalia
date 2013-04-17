﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Armalia.Sprites
{
 public abstract class Sprite
    {
        protected Texture2D texture;
        protected Point frameSize;

        public Sprite(Texture2D texture, Point frameSize) {
            this.texture = texture;
            this.frameSize = frameSize;
        }

        public Point FrameSize
        {
            get { return frameSize; }
        }
        public Vector2 Size()
        {
            return new Vector2(texture.Width, texture.Height);
        }


        public abstract void Draw(SpriteBatch spriteBatch, Vector2 spritePosition, float layerDepth);
        
    }
}
