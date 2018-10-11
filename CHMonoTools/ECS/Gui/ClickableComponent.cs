using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CHMonoTools.ECS.Gui
{
	public class ClickableComponent : Component
	{
		public bool IsFocused { get; set; } = false;
		public Rectangle BoundingBox { get; set; }
		public Matrix MouseTransform { get; set; } = Matrix.Identity;

		private MouseState previousMouseState;
		private KeyboardState previousKeyboardState;

		public delegate void ClickEventHandler(Entity sender, ClickableComponent clickableComponent);
		public event ClickEventHandler Click;


		public ClickableComponent(Rectangle Size)
		{
			this.previousMouseState = Mouse.GetState();
		}

		public override void Update(GameTime gameTime)
		{
			if (this.BoundingBox.IsEmpty)
			{
				base.Update(gameTime);
				return;
			}
			MouseState mouseState = Mouse.GetState();
			KeyboardState keyboardState = Keyboard.GetState();

			if (this.IsFocused)
			{
				if(this.previousKeyboardState.IsKeyDown(Keys.Enter) && !keyboardState.IsKeyDown(Keys.Enter))
				{
					Click?.Invoke(this.Entity, this);
				}
			}

			if (this.BoundingBox.Contains(mouseState.Position))
			{
				this.IsFocused = true;

				if (this.previousMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
				{
					Click?.Invoke(this.Entity, this);
				}
			}

			this.previousMouseState = mouseState;
			this.previousKeyboardState = keyboardState;
			base.Update(gameTime);
		}
	}
}
