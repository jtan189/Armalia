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
   
    
 public class MapHandler
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
            mapFiles.Add("Building1", @"Maps\Building1\Building1"); // this needs to come before Village (TODO: don't require that)
            mapFiles.Add("Village0",  @"Maps\Village0\Village0");
            mapFiles.Add("Forest1", @"Maps\Forest1\Forest1");
            songFiles.Add("Village0", @"Music\Home");
            //Song villageBgMusic = game.Content.Load<Song>(@"Music\Home");
            this.gs = gs;
            this.game = gm;
            this.playerChar = pc;
            this.mapMaker = new MapMaker(gm, this);
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
                List<LevelObject> objs = this.mapMaker.GetObjects(map.Value);
               
                GameLevel gl = new GameLevel(map.Key, this.mapMaker.BuildMap(map.Value), song,  enemies, objs);

                gameLevels.Add(map.Key, gl);
                x++;
            }
        }

        public GameLevel getLevel(string mapName)
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


     //end class
    }
}
