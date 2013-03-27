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
        int[,] tiles = new int[0, 0];
        int[,] objects = new int[0, 0];
        int firstgid = 0;
        public MapMaker(String xmlFile, ContentManager cm)
        {
            this.reader = XmlReader.Create(xmlFile);
            content = cm;
            // TODO: Construct any child components here
        }

        public void buildMap()
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
                           tiles = new int[width, height];
                           objects = new int[width, height];
                           InitArray(tiles);
                           InitArray(objects);
  
                          //  Console.WriteLine("Width: " + width + " | height: " + height);
                        }
                    break;

                    case "tileset":
                        if (reader.IsStartElement())
                        {
                            firstgid = Convert.ToInt32(reader.GetAttribute("firstgid"));
                            tileSet = XmlReader.Create(reader.GetAttribute("source"));
                            buildTileProperties(tileSet);
                            //Console.WriteLine("TileSet: " + tileSet.Read());
                            //Console.WriteLine("firstgid: " + firstgid);
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
                        tiles[x,y] = Convert.ToInt32(reader.GetAttribute("gid") );
                        x++;
                    break;

                     case "object":
                        int xpos = Convert.ToInt32(reader.GetAttribute("x") ) / tileWidth;
                        int ypos = Convert.ToInt32(reader.GetAttribute("y") ) / tileHeight;
                       // xpos--;
                        ypos--;
                     //   Console.WriteLine("Object - xpos: " + xpos + "\r\nObject - ypos: " + ypos);
                        objects[xpos, ypos] = Convert.ToInt32(reader.GetAttribute("gid") );
                     break;
                }
            }
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


    public void DrawMap(SpriteBatch sb)
    {
      //  flatten();
        int spX = (int)image.Width / tileWidth;
        int spY = (int)image.Height / tileHeight;
     //   printArr(tiles, "tiles.txt");
     //   printArr(objects, "objects.txt");
        Vector2 curPos = new Vector2(0, 0);
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y <tiles.GetLength(1); y++)
            {
               
                int gid = tiles[x, y] - firstgid;
                //  Console.Write(gid + ",   
                    Rectangle r = new Rectangle((int)(gid % spX) * tileHeight, (int)(gid / spX) * tileWidth, tileHeight, tileWidth);
                  sb.Draw(image, curPos,
                            r,
                            Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.3f);
                
                curPos.Y += tileWidth;
            }
            curPos.X += tileHeight;
            curPos.Y = 0;
            // Console.Write("\r\n");
        }
        
        curPos = new Vector2(0, 0);
        for (int x = 0; x < objects.GetLength(0); x++)
        {
            for (int y = 0; y < objects.GetLength(1); y++)
            {

                int gid = objects[x, y] - firstgid;
                //  Console.Write(gid + ", ");
                if(objects[x,y] > -1)
                {
                Rectangle r = new Rectangle((int)(gid % spX) * tileHeight, (int)(gid / spX) * tileWidth, tileHeight, tileWidth);
                      sb.Draw(image, curPos,
                              r,
                          Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.2f);
                }
                curPos.Y += tileWidth;

            }
            curPos.X += tileHeight;
            curPos.Y = 0;
            // Console.Write("\r\n");
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
    private void flatten()
    {
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                if (objects[x, y] > -1)
                {
                    tiles[x, y] = objects[x, y];
                }
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
