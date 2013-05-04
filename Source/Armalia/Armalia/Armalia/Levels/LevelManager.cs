using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Armalia.Exceptions;
using Microsoft.Xna.Framework.Media;
using Armalia.Characters;
using Armalia.GameScreens;

namespace Armalia.Levels
{
    class LevelManager
    {
        private ArmaliaGame game;
        private MapMaker mapMaker;
        private MainCharacter playerCharacter;
        private GameplayScreen gameplayScreen;

        private Dictionary<string, string> mapFiles;
        private Dictionary<string, string> songFiles;
        private Dictionary<string, GameLevel> gameLevels;

        public LevelManager(ArmaliaGame game, MainCharacter playerCharacter, GameplayScreen gameplayScreen)
        {
            this.game = game;
            this.gameplayScreen = gameplayScreen;
            this.mapMaker = new MapMaker(game);
            this.playerCharacter = playerCharacter;

            mapFiles = new Dictionary<string, string>();
            songFiles = new Dictionary<string, string>();
            gameLevels = new Dictionary<string, GameLevel>();

            mapFiles.Add("village1", @"Maps\Village1\Village0");
            songFiles.Add("village1", @"Music\Home");

            LoadLevels();
        }

        private void LoadLevels()
        {
            foreach (KeyValuePair<string, string> mapPair in this.mapFiles)
            {
                Song song = null;
                if (songFiles.ContainsKey(mapPair.Key))
                {
                    song = this.game.Content.Load<Song>(songFiles[mapPair.Key]);
                }

                List<EnemyCharacter> enemies = this.mapMaker.GetEnemies(mapPair.Value, this.playerCharacter, this.gameplayScreen);
                GameLevel level = new GameLevel(this.mapMaker.BuildMap(mapPair.Value), song, enemies);
                gameLevels.Add(mapPair.Key, level);
            }
        }

        public GameLevel GetLevel(string mapName)
        {
            if (this.gameLevels.ContainsKey(mapName))
            {
                return this.gameLevels[mapName];
            }
            else
            {
                throw new LevelDoesNotExistException();
            }
        }
    }
}
