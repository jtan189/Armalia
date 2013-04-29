// Lab Exercise 2
// CSCI 313
// Section 1
// March 7, 2013
// 
// Persei Games:
//    Anderson, Justin
//    Calvillo, Anthony
//    DeSilva, Nilmini
//    Tan, Josh

using System;

namespace AnimatedSprites
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}

