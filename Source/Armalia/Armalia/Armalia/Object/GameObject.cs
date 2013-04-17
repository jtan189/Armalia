using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Armalia.Object
{
    public abstract class GameObject
    {
        protected Vector2 position;

        public GameObject(Vector2 pos)
        {
            this.position = pos;
        }
    }
}
