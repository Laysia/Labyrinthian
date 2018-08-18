using Microsoft.Xna.Framework;
using System;

namespace Labyrinthian
{
	public class PointEventArgs : EventArgs
	{
		public Point Point { get; set; }
		public PointEventArgs(Point p)
		{
			this.Point = p;
		}
	}
}
