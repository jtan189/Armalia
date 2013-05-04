using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Armalia.Characters;
using Armalia.GameObjects;

namespace Armalia.Levels
{
    ///<summary>
    ///This will be our town, dungeon, inside a house, etc. 
    ///</summary>
    class GameLevel
    {
        private string name;
        private Song bgMusic;
        private List<EnemyCharacter> enemies;

        /// <summary>
        /// This just returns the map object.
        /// </summary>
        public Map LevelMap { get; set; }

        public List<LevelObject> LevelObjects { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="name">Name of the level.</param>
        /// <param name="levelMap">The map object to instantiate the level with.</param>
        /// <param name="bgMusic">Background music for the level.</param>
        /// <param name="enemies">Enemies located in the level.</param>
        /// <param name="levelObjects">Objects located in the level.</param>
        public GameLevel(string name, Map levelMap, Song bgMusic, List<EnemyCharacter> enemies, List<LevelObject> levelObjects)
        {
            this.LevelMap = levelMap;
            this.bgMusic = bgMusic;
            this.enemies = enemies;
            this.name = name;
            this.LevelObjects = levelObjects;
        }

        /// <summary>
        /// Play the level's background music.
        /// </summary>
        public void PlayBgMusic()
        {
            MediaPlayer.Play(bgMusic);
        }

        public void Update(GameTime gameTime)
        {
            // update enemies
            foreach (EnemyCharacter enemy in enemies)
            {
                if (enemy.HitPoints <= 0)
                {
                    enemies.Remove(enemy);
                }
                else
                {
                    enemy.Update(gameTime, LevelMap);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle mapWindow, Rectangle cameraView)
        {
            // draw map
            LevelMap.Draw(spriteBatch, mapWindow, cameraView);

            // draw enemies, NPCs, etc
            foreach (EnemyCharacter enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }

    }
}
