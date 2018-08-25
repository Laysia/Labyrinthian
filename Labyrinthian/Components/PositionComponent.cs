using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using System;

namespace Labyrinthian
{
	class PositionComponent : IComponent
	{
		private Vector2 position;

		public Vector2 LastTickPosition { get; private set; }

		public Orientation Orientation { get; private set; }

		private TransformComponent transformComponent;

		public Vector2 Position
		{
			get
			{
				return this.position;
			}
			set
			{
				if (this.position != value)
				{
					var diff = value - this.position;
					if (Math.Abs(diff.X) > Math.Abs(diff.Y))
					{
						if (diff.X > 0)
						{
							this.Orientation = Orientation.Right;
						}
						else
						{
							this.Orientation = Orientation.Left;
						}
					}
					else
					{
						if (diff.Y > 0)
						{
							this.Orientation = Orientation.Down;
						}
						else
						{
							this.Orientation = Orientation.Up;
						}
					}
					this.position = value;
				}
			}
		}
		
		public PositionComponent() { }

		public PositionComponent(Vector2 Position)
		{
			this.Position = Position;
			this.LastTickPosition = Position;
		}

		public Entity Entity { get; set; }

		public void Initialize()
		{

		}

		public void Update(GameTime gameTime)
		{
			this.LastTickPosition = this.Position;

			if (this.transformComponent == null || this.transformComponent.Entity != this.Entity)
			{
				this.transformComponent = this.Entity?.GetComponent<TransformComponent>();
				if (this.transformComponent == null)
				{
					return;
				}
			}

			this.Position = Vector2.Transform(this.Position, this.transformComponent.Transform);
			this.transformComponent.Transform = Matrix.Identity;
		}
	}

	public enum Orientation
	{
		Up,
		Right,
		Down,
		Left
	}
}
