using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Armalia.Characters;
using Armalia.Object;

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
        private Song bgMusic;
        private List<GameObject> gameObjects;
        private List<EnemyCharacter> enemies;

        public GameLevel(Map levelMap, Song bgMusic, List<EnemyCharacter> enemies, List<GameObject> objs) {
            this.map = levelMap;
            this.bgMusic = bgMusic;
            this.enemies = enemies;
            this.gameObjects = objs;
        }

        public string telePort(Rectangle playerPos)
        {
            foreach (GameObject obj in this.gameObjects)
            {
                if( obj.GetType() == typeof(Portal) )
                {
                    Portal port = (Portal) obj;
                    if(port.Collide(playerPos) )
                         return port.getMapTo();
                }
            }
            return null;
        }
        /// <summary>
        /// This just returns the map object.
        /// </summary>
        public Map LevelMap { get { return map; } }
        public void addEnemies(List<EnemyCharacter> enemies)
        {
            this.enemies = enemies;
        }
        /// <summary>
        /// This draws the gam level
        /// </summary>
        /// <param name="spriteBatch">The sprite batch object used for the game</param>
        /// <param name="mapWindow">The section of the screen to draw the map onto</param>
        /// <param name="cameraView">The viewable map of the player</param>
        public void PlayBgMusic()
        {
            MediaPlayer.Play(bgMusic);
        }

        public void Update(GameTime gameTime)
        {
            // update enemies
            foreach (EnemyCharacter enemy in enemies)
            {
                enemy.Update(gameTime, map);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle mapWindow, Rectangle cameraView)
        {
            // draw map
           map.Draw(spriteBatch, mapWindow, cameraView);

            // draw enemies, NPCs, etc
           foreach (EnemyCharacter enemy in enemies)
           {
               enemy.Draw(spriteBatch);
           }
        }

    }
}
