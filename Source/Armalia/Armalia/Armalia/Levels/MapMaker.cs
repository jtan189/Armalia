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
using Armalia.GameObjects;

namespace Armalia.Levels
{

    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    class MapMaker
    {
        private const int spawnXOffset = 0;
        private const int spawnYOffset = -70;

        /// <summary>
        /// The Game object
        /// </summary>
        private Game game;
        private LevelManager levelManager;

        /// <summary>
        /// Constructor 1
        /// </summary>
        /// <param name="game"> ArmaliaGame Object</param>
        /// <param name="levelManager">Level manager for this game.</param>
        public MapMaker(ArmaliaGame game, LevelManager levelManager)
        {
            this.game = game;
            this.levelManager = levelManager;
        }

        /// <summary>
        /// This will build a map object form XML filecode
        /// </summary>
        /// <param name="mapFilename">The TMX file of the map to be made</param>
        /// <returns></returns>
        public Map BuildMap(String mapFilename)
        {
            // load texture
            Texture2D mapTexture = game.Content.Load<Texture2D>(mapFilename);

            // load boundaries
            List<Rectangle> boundaries = GetBoundaries(mapFilename);

            return new Map(mapTexture, boundaries);
        }

        /// <summary>
        /// This will load the boundaries from the TMX file
        /// source: http://stackoverflow.com/questions/2439636/xna-best-way-to-load-and-read-a-xml-file
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
                if (obj.Attribute("type") == null)
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

            List<EnemyCharacter> enemies = new List<EnemyCharacter>();
            foreach (var obj in objectElements)
            {
                string type = null;
                if (obj.Attribute("type") != null)
                {
                    type = obj.Attribute("type").Value.ToString().ToLower();
                }
                if (type != null && type.Equals("enemy"))
                {
                    var properties = obj.Elements("properties").Elements().ToList();
                    string name = obj.Attribute("name").Value.ToString().ToLower();

                    // default attributes
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

        public List<LevelObject> GetObjects(String mapFilename)
        {
            var mapXML = XElement.Load(game.Content.RootDirectory + "\\" + mapFilename + ".tmx");
            var objectElements = mapXML.Elements("objectgroup").Elements().ToList();

            List<LevelObject> gameObjects = new List<LevelObject>();
            foreach (var obj in objectElements)
            {
                string type = null;
                if (obj.Attribute("type") != null)
                    type = obj.Attribute("type").Value.ToString().ToLower();
                if (type != null && !type.Equals("enemy"))
                {
                    var properties = obj.Elements("properties").Elements().ToList();
                    string name = obj.Attribute("name").Value.ToString();
                    int xcoord = Convert.ToInt32(obj.Attribute("x").Value);
                    int ycoord = Convert.ToInt32(obj.Attribute("y").Value);
                    int width = Convert.ToInt32(obj.Attribute("width").Value);
                    int height = Convert.ToInt32(obj.Attribute("height").Value);
                    switch (name)
                    {

                        case "Door":
                            string destinationMapFilename = "";
                            Vector2 charStartPosition = Vector2.Zero;
                            foreach (var prop in properties)
                            {
                                string propName = prop.Attribute("name").Value.ToString();
                                switch (propName)
                                {
                                    case "level":
                                        destinationMapFilename = prop.Attribute("value").Value;
                                        Console.WriteLine("==============\r\n" + prop.Attribute("value").Value);
                                        break;
                                    case "startPositionX":
                                        charStartPosition.X = Convert.ToInt32(prop.Attribute("value").Value);
                                        break;
                                    case "startPositionY":
                                        charStartPosition.Y = Convert.ToInt32(prop.Attribute("value").Value);
                                        break;
                                }
                            }

                            gameObjects.Add(new Portal(new Rectangle(xcoord, ycoord, width, height), destinationMapFilename, charStartPosition, levelManager));
                            break;

                    }
                }
            }

            return gameObjects;
        }

    }
}
