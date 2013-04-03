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
using Armalia.Spriting;

namespace Armalia.Mapping
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MapMaker 
    {
        private XmlReader reader;
        private const int tileHeight = 16;
        private const int tileWidth = 16;
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

      public Map[] buildMap()
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
                           tileSet =  XmlReader.Create(content.RootDirectory + "\\TileSets\\" + src + ".tsx");
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
                        int spX = (int)image.Width / tileWidth;
                        int spY = (int)image.Height / tileHeight;
                        int xpos = ((gid - 1) % spX) * tileHeight;
                        int ypos = ((gid - 1) / spY) * tileWidth;
                        bool isSolid = false;
                        tiles[x, y] = new ObjectSprite(new Vector2(xpos, ypos), isSolid);
                        x++;
                    break;

                     case "object":
                       int xindex = Convert.ToInt32(reader.GetAttribute("x") ) / tileWidth;
                       int yindex = Convert.ToInt32(reader.GetAttribute("y") ) / tileHeight;
                        gid =  Convert.ToInt32(reader.GetAttribute("gid") );
                        spX = (int)image.Width / tileWidth;
                        spY = (int)image.Height / tileHeight;
                        xpos = ((gid - 1) % spX) * tileHeight;
                        ypos = ((gid - 1) / spY) * tileWidth;
                        isSolid = true;
                        yindex--;
                        objects[xindex, yindex] = new ObjectSprite(new Vector2(xpos, ypos), isSolid);
                     break;
                }
            }
            Map[] ret = new Map[2];
            ret[0] = new Layer(width, height, image, objects);
            ret[1] = new Layer(width, height, image, tiles);
           
            return ret;
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
                 
                    image = content.Load<Texture2D>(@"SpriteImages\" + imageFile);
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
