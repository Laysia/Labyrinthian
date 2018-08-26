using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinthian
{
	class PlayerSpriteComponent : AnimatedSpriteComponent
	{
		private PlayerInputComponent entityInput;
		private PhysicsHitboxComponent entityHitbox;

		public PlayerSpriteComponent(Texture2D texture, SpriteAnimator spriteAnimator) : base(texture, spriteAnimator)
		{
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			base.Draw(gameTime, spriteBatch);

			if (this.entityHitbox == null)
			{
				this.entityHitbox = this.Entity?.GetComponent<PhysicsHitboxComponent>();
				if (this.entityHitbox == null)
				{
					return;
				}
			}


		}

		protected override Rectangle GetSourceRectangle()
		{
			if (this.entityInput == null)
			{
				this.entityInput = this.Entity?.GetComponent<PlayerInputComponent>();
				if (this.entityInput == null)
				{
					return new Rectangle();
				}
			}
			if (this.entityPosition == null)
			{
				this.entityPosition = this.Entity?.GetComponent<PositionComponent>();
				if (this.entityPosition == null)
				{
					return new Rectangle();
				}
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
	}
}
