using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

// TODO: replace these with the processor input and output types.
using TInput = System.Xml.XmlDocument;
using TOutput = Armalia.Maps.GameLevel;
using System.Xml;
using Armalia.Maps;
using Armalia.Sprites;

namespace Armalia.Utils
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to apply custom processing to content data, converting an object of
    /// type TInput to TOutput. The input and output types may be the same if
    /// the processor wishes to alter data without changing its type.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentProcessor attribute to specify the correct
    /// display name for this processor.
    /// </summary>
    [ContentProcessor(DisplayName = "TMX Processor")]
    public class TmxProcessor : ContentProcessor<XmlDocument, GameLevel>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            // TODO: process the input object, and return the modified data.
            
            XmlNodeList nodeList = input.GetElementsByTagName("Object");
            ObjectSprite[] objs = new ObjectSprite[nodeList.Count];
            int x = 0;
            foreach (XmlNode node in nodeList)
            {
                int xPos = Convert.ToInt32(node.Attributes["x"].Value);
                int yPos = Convert.ToInt32(node.Attributes["y"].Value);
                int height = Convert.ToInt32(node.Attributes["height"].Value);
                int width = Convert.ToInt32(node.Attributes["width"].Value);
                objs[x] = new ObjectSprite(new Point(xPos, yPos), new Point(height, width));
            }
            return new GameLevel(objs, 50, 50, new Point(50,50));
           // throw new NotImplementedException();
        }
    }
}