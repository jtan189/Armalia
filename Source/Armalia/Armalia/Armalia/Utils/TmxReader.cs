using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

// TODO: replace this with the type you want to read.
using TRead = Armalia.Maps.GameLevel;

namespace Armalia.Utils
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content
    /// Pipeline to read the specified data type from binary .xnb format.
    /// 
    /// Unlike the other Content Pipeline support classes, this should
    /// be a part of your main game project, and not the Content Pipeline
    /// Extension Library project.
    /// </summary>
    public class TmxReader : ContentTypeReader<TRead>
    {
        protected override TRead Read(ContentReader input, TRead existingInstance)
        {
            // TODO: read a value from the input ContentReader.
            int mapSizeX = input.ReadInt32();
            int mapSizeY = input.ReadInt32();
            int numbObjcs = input.ReadInt32();
            /*
             * output.Write(value.MapSize.X);
            output.Write(value.MapSize.Y);
            output.Write(value.objects.GetLength(0));
            foreach (ObjectSprite os in value.objects)
            {
                output.Write(os.position.X);
                output.Write(os.position.Y);
                output.Write(os.dimensions.X);
                output.Write(os.dimensions.Y);
                output.Write(os.boundingBox.X);
                output.Write(os.boundingBox.Y);
                output.Write(os.boundingBox.Height);
                output.Write(os.boundingBox.Width);
            }
             */
            return new Game(null, 0, 0, null, new Point(0, 0));
        }
    }
}
