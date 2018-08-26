using CHMonoTools.ECS;
using Microsoft.Xna.Framework;

namespace Labyrinthian
{
	class PositionComponent : Component
	{
		private TransformComponent transformComponent;

		public Vector2 LastTickPosition { get; private set; }
		public Vector2 Position { get; set; }

		public PositionComponent() { }

		public PositionComponent(Vector2 Position)
		{
			this.Position = Position;
			this.LastTickPosition = Position;
		}

		public override void Update(GameTime gameTime)
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
