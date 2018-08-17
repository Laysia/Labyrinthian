using System;

namespace Labyrinthian
{
	/// <summary>
	/// The main class.
	/// </summary>
	public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            using (var game = LabyrinthianGame.Game)
			{
				game.Run();
			}
		}
    }
}
