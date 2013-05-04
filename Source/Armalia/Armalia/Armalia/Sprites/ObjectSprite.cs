using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Armalia.Sprites
{
    class ObjectSprite : Sprite
    {
        public ObjectSprite(Texture2D texture, Point frameSize) : base(texture, frameSize) { }

        public override void Draw(SpriteBatch spriteBatch, Vector2 spritePosition, float layerDepth)
        {
            spriteBatch.Draw(texture, spritePosition, null,
                Color.White, 0, Vector2.Zero,
                1f, SpriteEffects.None, layerDepth);
        }
    }
}
