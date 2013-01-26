using System;

namespace Heart_Beat
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GameHeartBeat game = new GameHeartBeat())
            {
                game.Run();
            }
        }
    }
#endif
}

