using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Armalia.Exceptions;
using Microsoft.Xna.Framework.Media;
using Armalia.Characters;
using Armalia.GameScreens;
using Armalia.Object;

namespace Armalia.Maps
{
   
    
 public   class MapHandler
    {
        private Dictionary<string, string> mapFiles;
        private Dictionary<string, GameLevel> gameLevels;
        private Game game;
        private MapMaker mapMaker;
        private MainCharacter playerChar;
        private Dictionary<string, string> songFiles;
        private GameplayScreen gs;
        public MapHandler(Game gm, MainCharacter pc, GameplayScreen gs)
        {
            mapFiles = new Dictionary<string, string>();
            gameLevels = new Dictionary<string, GameLevel>();
            songFiles = new Dictionary<string, string>();
            mapFiles.Add("village1",  @"Maps\Village1\Village0");
            mapFiles.Add("building1", @"Maps\Building1\building1");
            songFiles.Add("village1", @"Music\Home");
            //Song villageBgMusic = game.Content.Load<Song>(@"Music\Home");
            this.gs = gs;
            this.game = gm;
            this.playerChar = pc;
            this.mapMaker = new MapMaker(gm);
            this.loadMaps();
            //Map villageMap = mapMaker.BuildLevel(villageMapFilename);
            //level = new GameLevel(villageMap);
        }

        private void loadMaps()
        {
            int x = 0;
            foreach (KeyValuePair<string, string> map in this.mapFiles)
            {
                  Song song = null;
                if(songFiles.ContainsKey(map.Key) )
                {
                   song = this.game.Content.Load<Song>(songFiles[map.Key]);
                }
                List <EnemyCharacter> enemies = this.mapMaker.GetEnemies(map.Value, this.playerChar, this.gs);
                List<GameObject> objs = this.mapMaker.GetObjects(map.Value);
                GameLevel gl = new GameLevel(this.mapMaker.BuildLevel(map.Value), song,  enemies, objs);

                gameLevels.Add(map.Key, gl);
                x++;
            }
        }

        public GameLevel getMap(string mapName)
        {
            if(this.gameLevels.ContainsKey(mapName) )
            {
                return this.gameLevels[mapName];
            }
            else
            {
                throw new MapDoesNotExistException();
            }
        }
    }
}
