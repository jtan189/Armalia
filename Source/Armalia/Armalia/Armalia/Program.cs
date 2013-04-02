using System;

namespace Armalia
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ArmaliaGame game = new ArmaliaGame())
           {
                game.Run();
            }
        }
    }
#endif
}

