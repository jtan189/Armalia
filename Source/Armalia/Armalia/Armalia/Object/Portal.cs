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


  
    }
}
