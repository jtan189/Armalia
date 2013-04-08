using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

// TODO: replace this with the type you want to write out.
using TWrite = Armalia.Maps.GameLevel;
using Armalia.Sprites;

namespace Armalia.Utils
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to write the specified data type into binary .xnb format.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    /// </summary>
    [ContentTypeWriter]
    public class TmxWriter : ContentTypeWriter<TWrite>
    {
        protected override void Write(ContentWriter output, TWrite value)
        {
            // TODO: write the specified value to the output ContentWriter.
          //  output.Write(value.MapImage.Name);
            output.Write(value.MapSize.X);
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
         //   throw new NotImplementedException();
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            // TODO: change this to the name of your ContentTypeReader
            // class which will be used to load this data.
            return "Utils.TmxReader, Utils";
        }
    }
}
