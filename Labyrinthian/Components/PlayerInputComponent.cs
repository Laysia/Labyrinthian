using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Labyrinthian
{
	class PlayerInputComponent : Component
	{
		private TransformComponent entityTransform;
		float speed = 2.0f;
		public Orientation Orientation { get; private set; }

		public override void Update(GameTime gameTime)
		{
			float percentage = (float)gameTime.ElapsedGameTime.TotalSeconds * Config.TickRate;

			if (entityTransform != null)
			{
				var state = Keyboard.GetState();
				if (state.IsKeyDown(Keys.W))
				{
					this.Orientation = Orientation.Up;
					entityTransform.Transform = entityTransform.Transform * Matrix.CreateTranslation((new Vector2(0, -1) * this.speed * percentage).ToVector3());
				}
				else if (state.IsKeyDown(Keys.A))
				{
					this.Orientation = Orientation.Left;
					entityTransform.Transform = entityTransform.Transform * Matrix.CreateTranslation((new Vector2(-1, 0) * this.speed * percentage).ToVector3());
				}
				else if (state.IsKeyDown(Keys.S))
				{
					this.Orientation = Orientation.Down;
					entityTransform.Transform = entityTransform.Transform * Matrix.CreateTranslation((new Vector2(0, 1) * this.speed * percentage).ToVector3());
				}
				else if (state.IsKeyDown(Keys.D))
				{
					this.Orientation = Orientation.Right;
					entityTransform.Transform = entityTransform.Transform * Matrix.CreateTranslation((new Vector2(1, 0) * this.speed * percentage).ToVector3());
				}
			}
			base.Update(gameTime);
		}
		protected override void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is TransformComponent t)
			{
				this.entityTransform = t;
			}
			base.Entity_ComponentAdded(sender, e);
		}
		protected override void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e)
		{
			if (e.Component == this.entityTransform)
			{
				this.entityTransform = null;
			}
			base.Entity_ComponentRemoved(sender, e);
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
