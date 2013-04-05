using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Text;
using System.Xml;
using System.IO;
using Armalia.Sprites;

namespace Armalia.Maps
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MapMaker 
    {
        private XmlReader reader;
        public const int TILE_HEIGHT = 32;
        public const int TILE_WIDTH = 32;
        private Dictionary<int, bool> solidTiles = new Dictionary<int, bool>();
        private Texture2D image;
        private ContentManager content;
        ObjectSprite[,] tiles = new ObjectSprite[0, 0];
        ObjectSprite[,] objects = new ObjectSprite[0, 0];
        int firstgid = 0;

        public MapMaker(String xmlFile, ContentManager cm)
        {
            this.reader = XmlReader.Create(cm.RootDirectory + "\\" + xmlFile);
            content = cm;
            // TODO: Construct any child components here
        }

      public GameLevel buildLevel()
        {
            int width = 0;
            int height = 0;
            
           
            int x = 0;
            int y = 0;
     
            XmlReader tileSet;

            while (reader.Read())
            {
                switch (reader.Name.ToLower())
                {
                        
                    case "map":
                        if (reader.IsStartElement())
                        {
                            width = Convert.ToInt32(reader.GetAttribute("width"));
                            height = Convert.ToInt32(reader.GetAttribute("height"));
                            tiles = new ObjectSprite[width, height];
                            objects = new ObjectSprite[width, height];
                            
                        }
                    break;

                    case "tileset":
                        if (reader.IsStartElement())
                        {
                            firstgid = Convert.ToInt32(reader.GetAttribute("firstgid"));
                           string src =  Path.GetFileNameWithoutExtension(reader.GetAttribute("source"));
                          //  tileSet = XmlReader.Create(src);
                           Console.WriteLine("ERROR " + src);
                           tileSet =  XmlReader.Create(content.RootDirectory + "\\Maps\\" + src + "\\" + src + ".tsx");
                            buildTileProperties(tileSet);
                        }
                    break;
                    case "layer":
                    if (reader.IsStartElement())
                    {
                        
                    }
                    break;

                    case "tile":
                        if(x == width)
                        {
                            x = 0;
                            y++;
                        }
                        // new Rectangle((int)(gid % spX) * tileHeight, (int)(gid / spX) * tileWidth, tileHeight, tileWidth);
                        int gid = Convert.ToInt32(reader.GetAttribute("gid") );
                        int spX = (int)image.Width / TILE_WIDTH;
                        int spY = (int)image.Height / TILE_HEIGHT;
                        int xpos = ((gid - 1) % spX) * TILE_HEIGHT;
                        int ypos = ((gid - 1) / spY) * TILE_WIDTH;
                        bool isSolid = false;
                        tiles[x, y] = new ObjectSprite(new Vector2(xpos, ypos), isSolid);
                        x++;
                    break;

                     case "object":
                       int xindex = Convert.ToInt32(reader.GetAttribute("x") ) / TILE_WIDTH;
                       int yindex = Convert.ToInt32(reader.GetAttribute("y") ) / TILE_HEIGHT;
                        gid =  Convert.ToInt32(reader.GetAttribute("gid") );
                        spX = (int)image.Width / TILE_WIDTH;
                        spY = (int)image.Height / TILE_HEIGHT;
                        xpos = ((gid - 1) % spX) * TILE_HEIGHT;
                        ypos = ((gid - 1) / spY) * TILE_WIDTH;
                        isSolid = true;
                        yindex--;
                        objects[xindex, yindex] = new ObjectSprite(new Vector2(xpos, ypos), isSolid);
                     break;
                }
            }
            Map[] ret = new Map[2];
            ret[0] = new Layer(width, height, image, objects);
            ret[1] = new Layer(width, height, image, tiles);

            return new GameLevel(ret, 25, 25, new Point(image.Width, image.Height));
        }


    private void buildTileProperties(XmlReader reader)
    {
        while (reader.Read())
        {
            int id = 0;
            switch (reader.Name.ToLower())
            {
                case "image":
                    string imageFile = Path.GetFileNameWithoutExtension( reader.GetAttribute("source") );
                 
                    image = content.Load<Texture2D>(@"Maps\" + imageFile + "\\" + imageFile);
                    Console.WriteLine("img width: " + image.Width);
                break;
                case "tile":
                    if (reader.IsStartElement())
                    {
                        id = Convert.ToInt32(reader.GetAttribute("id"));
                        reader.Read();
                        if (reader.Name.ToLower().Equals("properties") && reader.IsStartElement())
                        {
                            reader.Read();
                            if (reader.Name.ToLower().Equals("property"))
                            {
                                if (Convert.ToInt32(reader.GetAttribute("value")) == 1 && reader.GetAttribute("name").ToLower().Equals("issolid"))
                                {
                                    solidTiles.Add(id, true);
                                }
                                else
                                {
                                    solidTiles.Add(id, false);
                                }

                            }
                        }
                       
                    }
                break;

            }
        }
        
    }



    private void InitArray(int[,] arr)
    {
        for (int x = 0; x < arr.GetLength(0); x++)
        {
            for (int y = 0; y < arr.GetLength(1); y++)
            {
                arr[x, y] = -1;
            }
        }
    }

    private void printArr(int[,] arr, string fileN)
    {
       // StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Justin\Documents\School\Csci313\Armalia\armalia\Resources\" + fileN);
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                Console.Write(arr[i, j] + ", ");
            }
            Console.Write("\r\n");
        }
    }


//END OF CLASS
    }
}
