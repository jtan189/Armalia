using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

// TODO: Have Map represent all of map stuff. Level contains Map. Map contain Layers (if want), rather just object with layerValue.
namespace Armalia.Maps
{
    ///<summary>
    ///This will be our town, dungeon, inside a house, etc. 
    ///</summary>
    public class GameLevel
    {
        private Map map;

        public GameLevel(Map levelMap) {
            this.map = levelMap;
        }

        public Map LevelMap { get { return map; } }

        public void Draw(SpriteBatch spriteBatch, Rectangle mapWindow, Rectangle cameraView)
        {
            // draw map
           map.Draw(spriteBatch, mapWindow, cameraView);

            // draw enemies, NPCs, etc
        }

    }
}
