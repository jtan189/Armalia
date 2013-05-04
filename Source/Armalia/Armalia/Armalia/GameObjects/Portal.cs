using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Armalia.Levels;

namespace Armalia.GameObjects
{
    class Portal : LevelObject
    {
        private String destinationLevelFilename;
        private LevelManager levelManager;
        public GameLevel DestinationLevel { get { return levelManager.GetLevel(destinationLevelFilename); } }
        public Vector2 CharStartPosition { get; set; }

        public Portal(Rectangle portalRect, String destinationLevelFilename, Vector2 charStartPosition, LevelManager levelManager)
            : base(portalRect)
        {
            this.levelManager = levelManager;
            this.destinationLevelFilename = destinationLevelFilename;
            this.CharStartPosition = charStartPosition;
        }
    }
}
