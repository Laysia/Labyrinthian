using Microsoft.Xna.Framework;

namespace CHMonoTools.ECS
{
	public class TilePositionComponent : PositionComponent, ITilePositionComponent
	{
		public static int TILE_HEIGHT = 32;
		public static int TILE_WIDTH = 32;

		private Point tilePosition;
		public Point PreviousTilePosition { get; private set; }
		public Point TilePosition
		{
			get
			{
				return this.tilePosition;
			}
			private set
			{
				if (value != this.tilePosition)
				{
					this.PreviousTilePosition = this.tilePosition;
					this.tilePosition = value;
					TilePositionChanged?.Invoke(this, new TilePositionEventArgs(this.PreviousTilePosition, value));
				}
			}
		}

		public event TilePositionChangedHandler TilePositionChanged;

		public TilePositionComponent(Vector2 position) : base(position)
		{
			this.TilePosition = GetTilePosition(this.ActualPosition);
			this.PositionChanged += tilePositionComponent_PositionChanged;
		}

		private void tilePositionComponent_PositionChanged(IPositionComponent sender, PositionEventArgs e)
		{
			this.TilePosition = GetTilePosition(e.NewPosition);
		}

		public static Point GetTilePosition(Vector2 Position)
		{
			return new Point((int)(0.5f + Position.X / TILE_WIDTH), (int)(0.5f + Position.Y / TILE_HEIGHT));
		}

		public static Vector2 GetCenterPosition(Point TilePosition)
		{
			return new Vector2(TilePosition.X * 32, TilePosition.Y * 32);
		}

		public static Vector2 GetTopLeftPosition(Point TilePosition)
		{
			return new Vector2(TilePosition.X * 32 - 16, TilePosition.Y * 32 - 16);
		}
	}
}
