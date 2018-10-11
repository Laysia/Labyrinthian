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

			if (this.entityTransform != null)
			{
				Vector2 movementDirection = Vector2.Zero;
				var state = Keyboard.GetState();
				if (state.IsKeyDown(Keys.W))
				{
					movementDirection += new Vector2(0, -0.95f);
					this.Orientation = Orientation.Up;
				}
				if (state.IsKeyDown(Keys.A))
				{
					this.Orientation = Orientation.Left;
					movementDirection += new Vector2(-1, 0);
				}
				if (state.IsKeyDown(Keys.S))
				{
					this.Orientation = Orientation.Down;
					movementDirection += new Vector2(0, 0.95f);
				}
				if (state.IsKeyDown(Keys.D))
				{
					this.Orientation = Orientation.Right;
					movementDirection += new Vector2(1, 0);
				}
				if (movementDirection != Vector2.Zero)
				{
					movementDirection.Normalize();
					this.entityTransform.Movement += movementDirection * this.speed * percentage;
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
