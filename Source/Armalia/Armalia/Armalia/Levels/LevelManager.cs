using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Armalia.Exceptions;
using Microsoft.Xna.Framework.Media;
using Armalia.Characters;
using Armalia.GameScreens;
using Armalia.GameObjects;

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
            this.mapMaker = new MapMaker(game, this);
            this.playerCharacter = playerCharacter;

            mapFiles = new Dictionary<string, string>();
            songFiles = new Dictionary<string, string>();
            gameLevels = new Dictionary<string, GameLevel>();

            mapFiles.Add("Building1", @"Maps\Building1\Building1");
            mapFiles.Add("Village0", @"Maps\Village0\Village0");
            mapFiles.Add("Forest1", @"Maps\Forest1\Forest1");
            mapFiles.Add("Building2", @"Maps\Building2\Building2");
            mapFiles.Add("Building3", @"Maps\Building3\Building3");
            songFiles.Add("Village0", @"Music\Home");

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
                List<LevelObject> levelObjects = this.mapMaker.GetObjects(mapPair.Value);
                GameLevel level = new GameLevel(mapPair.Key, this.mapMaker.BuildMap(mapPair.Value), song, playerCharacter, enemies, levelObjects);
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
