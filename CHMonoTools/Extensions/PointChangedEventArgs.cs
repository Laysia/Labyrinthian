using System;

namespace Microsoft.Xna.Framework
{
	public class PointChangedEventArgs : EventArgs
	{
		public Point Previous { get; private set; }
		public Point New { get; private set; }

		public PointChangedEventArgs(Point Previous, Point New)
		{
			this.Previous = Previous;
			this.New = New;
		}
	}
}
