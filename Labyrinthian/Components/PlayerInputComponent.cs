using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Labyrinthian.Components
{
	class PlayerInputComponent : IComponent
	{
		public Entity Entity { get; set; }
		public Keys LastPressedMovementKey { get; private set; }
		float speed = 2.0f;

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
					this.LastPressedMovementKey = Keys.W;
					trans.Transform = trans.Transform * Matrix.CreateTranslation((new Vector2(0, -1) * this.speed * percentage).ToVector3());
				}
				else if (state.IsKeyDown(Keys.A))
				{
					this.LastPressedMovementKey = Keys.A;
					trans.Transform = trans.Transform * Matrix.CreateTranslation((new Vector2(-1, 0) * this.speed * percentage).ToVector3());
				}
				else if (state.IsKeyDown(Keys.S))
				{
					this.LastPressedMovementKey = Keys.S;

					trans.Transform = trans.Transform * Matrix.CreateTranslation((new Vector2(0, 1) * this.speed * percentage).ToVector3());
				}
				else if (state.IsKeyDown(Keys.D))
				{
					this.LastPressedMovementKey = Keys.D;
					trans.Transform = trans.Transform * Matrix.CreateTranslation((new Vector2(1, 0) * this.speed * percentage).ToVector3());
				}
			}
		}
	}
}
