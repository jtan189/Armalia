using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Armalia.Characters;

// TODO: Have Map represent all of map stuff. Level contains Map. Map contain Layers (if want), rather just object with layerValue.
namespace Armalia.Maps
{
    ///<summary>
    ///This will be our town, dungeon, inside a house, etc. 
    ///</summary>
    class GameLevel
    {
        private Map map;
        private Song bgMusic;
        private List<EnemyCharacter> enemies;

        public GameLevel(Map levelMap, Song bgMusic, List<EnemyCharacter> enemies) {
            this.map = levelMap;
            this.bgMusic = bgMusic;
            this.enemies = enemies;
        }

        public Map LevelMap { get { return map; } }

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
