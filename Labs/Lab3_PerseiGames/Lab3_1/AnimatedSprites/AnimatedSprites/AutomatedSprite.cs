// Lab Exercise 2
// CSCI 313
// Section 1
// March 7, 2013
// 
// Persei Games:
//    Anderson, Justin
//    Calvillo, Anthony
//    DeSilva, Nilmini
//    Tan, Josh

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimatedSprites
{
    class AutomatedSprite : Sprite
    {
        
        public AutomatedSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, string collisionCueName)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, collisionCueName)
        {
        }

        public AutomatedSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            int millisecondsPerFrame, string collisionCueName)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame, sheetSize,
            speed, millisecondsPerFrame, collisionCueName)
        {
        }

        // sprite is automated. Direction is same as speed
        public override Vector2 Direction
        {
            get { return speed; }
            protected set { speed = value; }
        }

        //public override string CollisionCueName
        //{
        //    get { return collisionCueName; }
        //    protected set { collisionCueName = value; }
        //}

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            // move sprite based on direction
            position += Direction;
            
            base.Update(gameTime, clientBounds);
        }

    }
}
