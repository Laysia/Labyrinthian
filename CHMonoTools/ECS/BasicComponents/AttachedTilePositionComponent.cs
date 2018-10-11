using Microsoft.Xna.Framework;

namespace CHMonoTools.ECS
{
	public class AttachedTilePositionComponent : AttachedPositionComponent
	{
		private ITilePositionComponent tilePositionComponent;
		public ITilePositionComponent TilePositionComponent
		{
			get
			{
				return this.tilePositionComponent;
			}
			set
			{
				if (this.tilePositionComponent == value)
				{
					return;
				}

				if (this.tilePositionComponent != null)
				{
					this.tilePositionComponent.TilePositionChanged -= tilePositionComponent_TilePositionChanged;
				}

				this.tilePositionComponent = value ?? new TilePositionComponent(Vector2.Zero);
				this.PositionComponent = this.tilePositionComponent;
				this.tilePositionComponent.TilePositionChanged += tilePositionComponent_TilePositionChanged;
			}
		}

		private void tilePositionComponent_TilePositionChanged(ITilePositionComponent sender, TilePositionEventArgs e)
		{
			TilePositionChanged?.Invoke(sender, e);
		}

		public Point PreviousTilePosition
		{
			get
			{
				return this.TilePositionComponent.PreviousTilePosition;
			}
		}
		public Point TilePosition
		{
			get
			{
				return this.TilePositionComponent.TilePosition;
			}
		}

		public event TilePositionChangedHandler TilePositionChanged;

		public AttachedTilePositionComponent(ITilePositionComponent tilePositionComponent) : base(tilePositionComponent)
		{
			this.TilePositionComponent = tilePositionComponent;
		}
	}
}
