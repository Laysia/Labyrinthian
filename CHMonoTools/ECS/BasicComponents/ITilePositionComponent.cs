using System;
using Microsoft.Xna.Framework;

namespace CHMonoTools.ECS
{
	public interface ITilePositionComponent : IPositionComponent
	{
		Point PreviousTilePosition { get; }
		Point TilePosition { get; }
		event TilePositionChangedHandler TilePositionChanged;
	}

	public delegate void TilePositionChangedHandler(ITilePositionComponent sender, TilePositionEventArgs e);

	public class TilePositionEventArgs : EventArgs
	{
		public Point PreviousPosition { get; private set; }
		public Point NewPosition { get; private set; }

		public TilePositionEventArgs(Point PreviousPosition, Point NewPosition)
		{
			this.PreviousPosition = PreviousPosition;
			this.NewPosition = NewPosition;
		}
	}
}