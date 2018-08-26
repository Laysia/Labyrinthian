using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using System;

namespace Labyrinthian
{
	class PositionComponent : IComponent
	{
		public Vector2 LastTickPosition { get; private set; }

		private TransformComponent transformComponent;

		public Vector2 Position { get; set; }

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
}
