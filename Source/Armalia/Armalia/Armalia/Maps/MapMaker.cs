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
using System.Xml.Linq;
using System.IO;
using Armalia.Sprites;
using System.Text.RegularExpressions;
using Armalia.Characters;
using Armalia.GameScreens;
using Armalia.Object;

namespace Armalia.Maps
{

    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MapMaker 
    {
       
        /// <summary>
        /// The Game object
        /// </summary>
        private Game game;
        /// <summary>
        /// Constructor 1
        /// </summary>
        /// <param name="game"> ArmaliaGame Object</param>
        public MapMaker(ArmaliaGame game)
        {
            this.game = game;
        }
        /// <summary>
        /// Constructor 2
        /// </summary>
        /// <param name="gm">The GAme object</param>
        public MapMaker(Game gm)
        {
            // TODO: Complete member initialization
            this.game = gm;
        }
        /// <summary>
        /// This will build a map object form XML filecode
        /// </summary>
        /// <param name="mapFilename">The TMX file of the map to be made</param>
        /// <returns></returns>
        public Map BuildLevel(String mapFilename)
        {
            // load texture
            Texture2D mapTexture = game.Content.Load<Texture2D>(mapFilename);

            // load boundaries
            List<Rectangle> boundaries = GetBoundaries(mapFilename);

            // load enemies, NPCs, etc.

            return new Map(mapTexture, boundaries);
        }

        
        
        /// <summary>
        /// This will load the boundaries from the TMX file
        /// sources: http://stackoverflow.com/questions/2439636/xna-best-way-to-load-and-read-a-xml-file
        /// </summary>
        /// <param name="mapFilename">The TMX file name</param>
        /// <returns></returns>
        public List<Rectangle> GetBoundaries(String mapFilename)
        {
            var mapXML = XElement.Load(game.Content.RootDirectory + "\\" + mapFilename + ".tmx");
            var objectElements = mapXML.Elements("objectgroup").Elements().ToList();

            // convert boundaries to Rectangles; store in list
            List<Rectangle> boundaries = new List<Rectangle>();
            foreach (var obj in objectElements)
            {
                int xCoord = 0;
                int yCoord = 0;
                int width = 0;
                int height = 0;
                if(obj.Attribute("type") == null)
                {
                    xCoord = Convert.ToInt32(obj.Attribute("x").Value);
                    yCoord = Convert.ToInt32(obj.Attribute("y").Value);
                    if (obj.Attribute("width") != null)
                        width = Convert.ToInt32(obj.Attribute("width").Value);
                    if (obj.Attribute("height") != null)
                        height = Convert.ToInt32(obj.Attribute("height").Value);
                    Rectangle boundaryRect = new Rectangle(xCoord, yCoord,
                        width, height);
                    boundaries.Add(boundaryRect);
                }
               
            }

            return boundaries;
        }

        public List<EnemyCharacter> GetEnemies(String mapFilename, MainCharacter pc, GameplayScreen gs)
        {
            var mapXML = XElement.Load(game.Content.RootDirectory + "\\" + mapFilename + ".tmx");
            var objectElements = mapXML.Elements("objectgroup").Elements().ToList();

            // convert boundaries to Rectangles; store in list
            List<EnemyCharacter> enemies = new List<EnemyCharacter>();
            foreach (var obj in objectElements)
            {
                string type = null;
                if (obj.Attribute("type") != null)
                {
                    type = obj.Attribute("type").Value.ToString().ToLower();
                    Console.WriteLine(type);
                }
                if (type != null && type.Equals("enemy"))
                {
                    var properties = obj.Elements("properties").Elements().ToList();
                    string name = obj.Attribute("name").Value.ToString().ToLower();
                    int hp = 100;
                    int mp = 100;
                    int xp = 0;
                    int strength = 10;
                    int defense = 10;

                    int xcoord = Convert.ToInt32(obj.Attribute("x").Value);
                    int ycoord = Convert.ToInt32(obj.Attribute("y").Value);
                    foreach (var prop in properties)
                    {
                        string propName = prop.Attribute("name").ToString().ToLower();
                        switch (propName)
                        {
                            case "strength":
                                strength = Convert.ToInt32(prop.Attribute("value").Value);
                            break;
                            case "hp":
                              hp = Convert.ToInt32(prop.Attribute("value").Value);
                            break;
                            case "mp":
                                mp = Convert.ToInt32(prop.Attribute("value").Value);
                            break;
                            case "defense":
                              defense = Convert.ToInt32(prop.Attribute("value").Value);
                            break;
                            case "xp":
                               xp = Convert.ToInt32(prop.Attribute("value").Value);
                            break;

                        }
                    }

                    switch (name.ToLower())
                    {
                    default:
                        case "knight":
                            Texture2D knightTexture = game.Content.Load<Texture2D>(@"Characters\charchip01-2-1");
                            Point knightTextureFrameSize = new Point(32, 32);
                            int knightCollisionOffset = 0;
                            Point knightInitialFrame = new Point(1, 0);
                            Point knightSheetSize = new Point(3, 4);
                            Vector2 knightSpeed = new Vector2(1, 1);
                            Vector2 initialKnightPos = new Vector2(xcoord, ycoord);

                            AnimatedSprite knightSprite = new AnimatedSprite(
                                knightTexture, knightTextureFrameSize, knightCollisionOffset, knightInitialFrame, knightSheetSize);

                            List<Point> knightTargets = new List<Point>() {
                                new Point((int)(xcoord - 100), ycoord), new Point((int)(xcoord+100), ycoord) };

                              EnemyCharacter knightEnemy = new Knight(knightSprite, initialKnightPos, hp, mp, xp,
                strength, defense, knightSpeed, gs, knightTargets, pc);
                        enemies.Add(knightEnemy);

              
                        break;

                    }
                }
            }

            return enemies;
        }












        public List<GameObject> GetObjects(String mapFilename)
        {
            var mapXML = XElement.Load(game.Content.RootDirectory + "\\" + mapFilename + ".tmx");
            var objectElements = mapXML.Elements("objectgroup").Elements().ToList();

            // convert boundaries to Rectangles; store in list
            List<GameObject> gameObjects = new List<GameObject>();
            foreach (var obj in objectElements)
            {
                string type = null;
                if (obj.Attribute("type") != null)
                    type = obj.Attribute("type").Value.ToString().ToLower();
                if (type != null && !type.Equals("enemy"))
                {
                    var properties = obj.Elements("properties").Elements().ToList();
                    string name = obj.Attribute("name").Value.ToString().ToLower();
                    int xcoord = Convert.ToInt32(obj.Attribute("x").Value);
                    int ycoord = Convert.ToInt32(obj.Attribute("y").Value);
                    int width = Convert.ToInt32(obj.Attribute("width").Value);
                    int height = Convert.ToInt32(obj.Attribute("height").Value);
                    Console.WriteLine("===============> " + name);
                    switch (name)
                    {
                        default:
                        case "portal":
                            string mapTo = "";
                            string mapFrom = mapFilename.Split('\\')[2];
                            foreach (var prop in properties)
                            {
                                string propName = prop.Attribute("name").Value.ToString().ToLower();
                                switch (propName)
                                {
                                    default:
                                    case "level":
                                        mapTo = prop.Attribute("value").Value;
                                        Console.WriteLine(" LEVEL NAME = " + mapTo);
                                    break;
                                }
                            }
                            Console.WriteLine("------------====================================");
                            gameObjects.Add(new Portal(xcoord, ycoord, mapTo, mapFrom, width, height));
                        break;

                    }
                }
            }

            return gameObjects;
        }




//END OF CLASS
    }
}
