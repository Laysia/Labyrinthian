using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinthian.Components
{
	class AnimatedSpriteComponent : IDrawableComponent
	{
		public Entity Entity { get; set; }

		public Texture2D Texture;
		protected SpriteAnimator SpriteAnimator { get; set; }

		protected PositionComponent entityPosition;

		public AnimatedSpriteComponent(Texture2D texture, SpriteAnimator spriteAnimator)
		{
			this.Texture = texture;
			this.SpriteAnimator = spriteAnimator;
		}

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			if (this.entityPosition == null || this.entityPosition.Entity != this.Entity)
			{
				this.entityPosition = this.Entity?.GetComponent<PositionComponent>();
				if (this.entityPosition == null)
				{
					return;
				}
			}

			Rectangle sourceRectangle = GetSourceRectangle();
			spriteBatch.Draw(this.Texture, this.entityPosition.Position - sourceRectangle.Size.ToVector2() / 2, sourceRectangle, Color.White);
		}

		protected virtual Rectangle GetSourceRectangle()
		{
			return new Rectangle();
		}

		public void Initialize()
		{

		}

		public void Update(GameTime gameTime)
		{
			this.SpriteAnimator.Update(gameTime);
		}
	}
}
