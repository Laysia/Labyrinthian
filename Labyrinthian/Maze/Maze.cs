using CHMonoTools;

namespace Labyrinthian.Maze
{
	class Maze
	{
		/// <summary>
		/// A Maze consists of atleast one Layer (2D-Maze) or more (3D-Maze)
		/// </summary>
		public MazeLayer[] MazeLayers { get; set; }

		public Point3D Startpoint { get; set;}
		public Point3D EndPoint { get; set; }
	}
}
