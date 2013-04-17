using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Armalia.Object
{
    public class Portal : GameObject
    {
        private string mapTo;
        private string mapFrom;
        Vector2 size;
        
        public Portal(Vector2 pos, string mt, string mf, Vector2 s) : base(pos)
        {
            this.mapFrom = mf;
            this.mapTo = mt;
            this.size = s;

        }
        public Portal(Vector2 pos, string mt, string mf, int w, int h)
            : base(pos)
        {
            this.mapFrom = mf;
            this.mapTo = mt;
            this.size = new Vector2(w,h);

        }
        public Portal(int x, int y, string mt, string mf, int w, int h)
            : base(new Vector2(x,y))
        {
            this.mapFrom = mf;
            this.mapTo = mt;
            this.size = new Vector2(w, h);

        }

        public string getMapTo()
        {
            return this.mapTo;
        }

        public bool Collide(Rectangle r)
        {
            return r.Intersects(new Rectangle((int)this.position.X, (int)this.position.Y, (int)this.size.X, (int) this.size.Y));
        }

  
    }
}
