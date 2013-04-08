using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Armalia.Exceptions;

namespace Armalia.Maps
{
   
    
    class MapHandler
    {
        private Dictionary<string, string> mapFiles;
        private Dictionary<string, GameLevel> gameLevels;
        private Game game;
        private MapMaker mapMaker;
        public MapHandler(Game gm)
        {
            mapFiles = new Dictionary<string, string>();
            gameLevels = new Dictionary<string, GameLevel>();
            mapFiles.Add("village1",  @"Maps\Village1\Village1");
           
            this.game = gm;
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
                GameLevel gl = new GameLevel(this.mapMaker.BuildLevel(map.Value));
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
