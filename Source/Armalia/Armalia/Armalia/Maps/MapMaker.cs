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

namespace Armalia.Maps
{
    // TODO: determine if is this class necessary

    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MapMaker 
    {
        private ArmaliaGame game;

        public MapMaker(ArmaliaGame game)
        {
            this.game = game;
        }

        public Map BuildLevel(String mapFilename)
        {
            // load texture
            Texture2D mapTexture = game.Content.Load<Texture2D>(mapFilename);

            // load boundaries
            List<Rectangle> boundaries = GetBoundaries(mapFilename);

            // load enemies, NPCs, etc.

            return new Map(mapTexture, boundaries);
        }

        // sources: http://stackoverflow.com/questions/2439636/xna-best-way-to-load-and-read-a-xml-file
        public List<Rectangle> GetBoundaries(String mapFilename)
        {
            var mapXML = XElement.Load(game.Content.RootDirectory + "\\" + mapFilename + ".tmx");
            var boundaryElements = mapXML.Elements("objectgroup").Elements().ToList();

            // convert boundaries to Rectangles; store in list
            List<Rectangle> boundaries = new List<Rectangle>();
            foreach (var boundary in boundaryElements)
            {
                int xCoord = Convert.ToInt32(boundary.Attribute("x").Value);
                int yCoord = Convert.ToInt32(boundary.Attribute("y").Value);
                int width = Convert.ToInt32(boundary.Attribute("width").Value);
                int height = Convert.ToInt32(boundary.Attribute("height").Value);
                Rectangle boundaryRect = new Rectangle(xCoord, yCoord,
                   width, height);
                boundaries.Add(boundaryRect);
            }

            return boundaries;
        }

    }
}
