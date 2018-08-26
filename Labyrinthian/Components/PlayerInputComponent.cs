using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Labyrinthian
{
	class PlayerInputComponent : IComponent
	{
		public Entity Entity { get; set; }
		float speed = 2.0f;

		public Orientation Orientation { get; private set; }

		public void Initialize()
		{

		}

		public void Update(GameTime gameTime)
		{
			float percentage = (float)gameTime.ElapsedGameTime.TotalSeconds * Config.TickRate;
			TransformComponent trans = this.Entity?.GetComponent<TransformComponent>();

			if (trans != null)
			{
				var state = Keyboard.GetState();
				if (state.IsKeyDown(Keys.W))
				{
					this.Orientation = Orientation.Up;
					trans.Transform = trans.Transform * Matrix.CreateTranslation((new Vector2(0, -1) * this.speed * percentage).ToVector3());
				}
				else if (state.IsKeyDown(Keys.A))
				{
					this.Orientation = Orientation.Left;
					trans.Transform = trans.Transform * Matrix.CreateTranslation((new Vector2(-1, 0) * this.speed * percentage).ToVector3());
				}
				else if (state.IsKeyDown(Keys.S))
				{
					this.Orientation = Orientation.Down;
					trans.Transform = trans.Transform * Matrix.CreateTranslation((new Vector2(0, 1) * this.speed * percentage).ToVector3());
				}
				else if (state.IsKeyDown(Keys.D))
				{
					this.Orientation = Orientation.Right;
					trans.Transform = trans.Transform * Matrix.CreateTranslation((new Vector2(1, 0) * this.speed * percentage).ToVector3());
				}
			}
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
