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
        /// <summary>
        /// The map object of the level
        /// </summary>
        private Map map;
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="levelMap">The map object to instantiate the level with.</param>
        public GameLevel(Map levelMap) {
            this.map = levelMap;
        }
        /// <summary>
        /// This just returns the map object.
        /// </summary>
        public Map LevelMap { get { return map; } }

        /// <summary>
        /// This draws the gam level
        /// </summary>
        /// <param name="spriteBatch">The sprite batch object used for the game</param>
        /// <param name="mapWindow">The section of the screen to draw the map onto</param>
        /// <param name="cameraView">The viewable map of the player</param>
        public void Draw(SpriteBatch spriteBatch, Rectangle mapWindow, Rectangle cameraView)
        {
            // draw map
           map.Draw(spriteBatch, mapWindow, cameraView);

            // draw enemies, NPCs, etc
        }

    }
}
