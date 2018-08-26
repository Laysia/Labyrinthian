using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinthian
{
	class PlayerSpriteComponent : AnimatedSpriteComponent
	{
		private PlayerInputComponent entityInput;

		public PlayerSpriteComponent(Texture2D texture, SpriteAnimator spriteAnimator) : base(texture, spriteAnimator)
		{
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			base.Draw(gameTime, spriteBatch);
		}

		protected override Rectangle GetSourceRectangle()
		{
			if (this.entityInput == null || this.entityInput == null)
			{
				return Rectangle.Empty;
			}

			string animationName = "";
			if (this.entityPosition.LastTickPosition != this.entityPosition.Position)
			{
				switch (this.entityInput.Orientation)
				{
					case Orientation.Up:
						animationName = "walk_up";
						break;
					case Orientation.Left:
						animationName = "walk_left";
						break;
					case Orientation.Down:
						animationName = "walk_down";
						break;
					case Orientation.Right:
						animationName = "walk_right";
						break;
				}
			}
			else
			{
				switch (this.entityInput.Orientation)
				{
					case Orientation.Up:
						animationName = "up";
						break;
					case Orientation.Left:
						animationName = "left";
						break;
					case Orientation.Down:
						animationName = "down";
						break;
					case Orientation.Right:
						animationName = "right";
						break;
				}
			}
			Rectangle sourceRectangle = this.SpriteAnimator.GetSourceRectangle(animationName);
			return sourceRectangle;
		}

		protected override void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is PlayerInputComponent p)
			{
				this.entityInput = p;
			}
			base.Entity_ComponentAdded(sender, e);
		}
		protected override void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e)
		{
			if (e.Component == this.entityInput)
			{
				this.entityInput = null;
			}
			base.Entity_ComponentRemoved(sender, e);
		}
	}
}
