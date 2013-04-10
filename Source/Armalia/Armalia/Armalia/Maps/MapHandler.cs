using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Armalia.Exceptions;
using Microsoft.Xna.Framework.Media;

namespace Armalia.Maps
{
   
    
    class MapHandler
    {
        private Dictionary<string, string> mapFiles;
        private Dictionary<string, GameLevel> gameLevels;
        private Game game;
        private MapMaker mapMaker;
        private Dictionary<string, string> songFiles;
        public MapHandler(Game gm)
        {
            mapFiles = new Dictionary<string, string>();
            gameLevels = new Dictionary<string, GameLevel>();
            songFiles = new Dictionary<string, string>();
            mapFiles.Add("village1",  @"Maps\Village1\Village0");
            songFiles.Add("village1", @"Music\Home");
            //Song villageBgMusic = game.Content.Load<Song>(@"Music\Home");
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
                  Song song = null;
                if(songFiles.ContainsKey(map.Key) )
                {
                   song = this.game.Content.Load<Song>(songFiles[map.Key]);
                }
               
                GameLevel gl = new GameLevel(this.mapMaker.BuildLevel(map.Value), song, null);
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
