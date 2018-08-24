using CHMonoTools;
using System;

namespace Labyrinthian.Maze
{
	class MazeGenerator
	{
		private int layerCount = 1;
		private int width = 2;
		private int height = 2;
		private Point3D startposition = new Point3D(1,0,0);
		private Point3D endposition;

		public int LayerCount
		{
			get { return this.layerCount; }
			set
			{
				if (value < 1)
				{
					throw new ArgumentException($"The {nameof(this.LayerCount)} needs to be 1 or higher!", nameof(this.LayerCount));
				}
				this.layerCount = value;
			}
		}
		public int Width
		{
			get { return this.width; }
			set
			{
				if (value < 2)
				{
					throw new ArgumentException($"The {nameof(this.Width)} needs to be 2 or higher!", nameof(this.Width));
				}
				this.width = value;
			}
		}
		public int Height
		{
			get { return this.height; }
			set
			{
				if (value < 2)
				{
					throw new ArgumentException($"The {nameof(this.Width)} needs to be 2 or higher!", nameof(this.Width));
				}
				this.height = value;
			}
		}
		public Point3D Startposition
		{
			get { return this.startposition; }
			set
			{
				// first check for bounds
				if (value.X < 0 || value.X > this.Width)
				{
					throw new ArgumentOutOfRangeException($"The {nameof(value.X)}-Value has to be on the side of the cube!", nameof(this.Startposition));
				}
				if (value.Y < 0 || value.Y > this.Height)
				{
					throw new ArgumentOutOfRangeException($"The {nameof(value.Y)}-Value has to be on the side of the cube!", nameof(this.Startposition));
				}
				if (value.Z < 0 || value.Z > this.LayerCount)
				{
					throw new ArgumentOutOfRangeException($"The {nameof(value.Z)}-Value has to be on the side of the cube!", nameof(this.Startposition));
				}
				// Can start on any Layer, but on the edge (not corner)
				if (value.X == 0 || value.X == this.Width)
				{
					if (value.Y == 0 || value.Y == this.Height)
					{
						throw new ArgumentException("The Startposition has to be on the side of the cube!", nameof(this.Startposition));
					}
					this.startposition = value;
				}
				else if (value.Y == 0 || value.Y == this.Width)
				{
					if (value.X == 0 || value.X == this.Width)
					{
						throw new ArgumentException("The Startposition has to be on the side of any Layer (not the corners)!", nameof(this.Startposition));
					}
					this.startposition = value;
				}
				else
				{
					throw new ArgumentException("The Startposition has to be on the side of the cube!", nameof(this.Startposition));
				}
			}
		}
		private Point3D Endposition
		{
			get { return this.endposition; }
			set
			{
				this.endposition = value;
			}
		}

		public Maze CreateNewMaze()
		{
			/*
			int[][][] maze = new int[this.width][][];
			for (int i = 0; i < this.Width; i++)
			{
				maze[i] = new int[this.Width][];
				for (int j = 0; j < this.LayerCount; j++)
					maze[i][j] = new int[this.LayerCount];
			}
			*/

			int[,,] maze = new int[this.Width, this.Height, this.LayerCount];




			var returnMaze = new Maze();
			returnMaze.MazeLayers = new MazeLayer[this.LayerCount];
			for (int z = 0; z < maze.GetLength(2); ++z)
			{
				MazeLayer layer = new MazeLayer(); //TODO Tiles = maze.GetValue()}
				returnMaze.MazeLayers[z] = layer;
			}

			// TODO add StartPoint and Endpoint
			return returnMaze;
		}
	}
}
