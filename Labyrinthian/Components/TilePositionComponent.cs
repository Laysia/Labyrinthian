using CHMonoTools.ECS;
using Microsoft.Xna.Framework;

namespace Labyrinthian
{
	class TilePositionComponent : Component
	{
		public const int TILE_HEIGHT = 32;
		public const int TILE_WIDTH = 32;
		public static Point TILE_SIZE = new Point(TILE_WIDTH, TILE_HEIGHT);

		public delegate void PointChangedHandler(TilePositionComponent sender, PointChangedEventArgs e);
		public event PointChangedHandler TilePositionChanged;

		protected TransformComponent transformComponent;
		private Vector2 position;
		private Point tilePosition;

		public Vector2 LastTickPosition { get; private set; }
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
					Point previous = this.tilePosition;
					this.tilePosition = value;
					TilePositionChanged?.Invoke(this, new PointChangedEventArgs(previous, value));
				}
			}
		}
		public Vector2 Position
		{
			get
			{
				return this.position;
			}
			protected set
			{
				this.LastTickPosition = this.position;
				if (value != this.position)
				{
					this.TilePosition = new Point((int)(0.5f + value.X / TILE_WIDTH), (int)(0.5f + value.Y / TILE_HEIGHT));
					this.position = value;
				}
			}
		}

		public TilePositionComponent() { }

		public TilePositionComponent(Vector2 Position)
		{
			this.Position = Position;
		}

		public override void Update(GameTime gameTime)
		{
			if (this.transformComponent == null)
			{
				base.Update(gameTime);
				return;
			}
			this.Position = Vector2.Transform(this.Position, this.transformComponent.Transform);
			this.transformComponent.Transform = Matrix.Identity;
			base.Update(gameTime);
		}

		protected override void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is TransformComponent t)
			{
				this.transformComponent = t;
			}
			base.Entity_ComponentAdded(sender, e);
		}
		protected override void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e)
		{
			if (e.Component == this.transformComponent)
			{
				this.transformComponent = null;
			}
			base.Entity_ComponentRemoved(sender, e);
		}
	}
}
