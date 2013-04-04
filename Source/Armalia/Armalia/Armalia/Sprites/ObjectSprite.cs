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
        public ObjectSprite(Vector2 pos, bool s)
            : base(pos, s)
        {

        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 spritePosition, float layerDepth)
        {
            // draw the sprite
            spriteBatch.Draw(texture, spritePosition, null, 
                Color.White, 0, Vector2.Zero,
                1f, SpriteEffects.None, layerDepth);
        }

    }
}
