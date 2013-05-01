using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Armalia.Maps;

namespace Armalia.Object
{
    public class Portal : LevelObject
    {
        private String destinationLevelFilename;
        private MapHandler mapHandler;
        public GameLevel DestinationLevel { get { return mapHandler.getLevel(destinationLevelFilename); } }
        public Vector2 CharStartPosition { get; set; }
        
        public Portal(Rectangle portalRect, String destinationLevelFilename, Vector2 charStartPosition, MapHandler mapHandler) : base(portalRect)
        {
            this.mapHandler = mapHandler;
            this.destinationLevelFilename = destinationLevelFilename;
            this.CharStartPosition = charStartPosition;

        }
        //public Portal(Vector2 pos, string mt, int w, int h, Point p)
        //    : base(pos)
        //{
        //    this.mapToPt = p;
        //    this.mapTo = mt;
        //    this.size = new Vector2(w,h);

        //}
        //public Portal(int x, int y, string mt, int w, int h, Point p)
        //    : base(new Vector2(x,y))
        //{
        //    this.mapTo = mt;
        //    this.size = new Vector2(w, h);
        //    this.mapToPt = p;

        //}

        //public string getMapTo()
        //{
        //    return this.mapTo;
        //}
        //public Point getMapToPt()
        //{
        //    return this.mapToPt;
        //}

        //public bool Collide(Rectangle r)
        //{
        //    return r.Intersects(new Rectangle((int)this.position.X, (int)this.position.Y, (int)this.size.X, (int) this.size.Y));
        //}

  
    }
}
