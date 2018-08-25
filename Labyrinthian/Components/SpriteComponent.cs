using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinthian
{
	class SpriteComponent : IDrawableComponent
	{
		public Entity Entity { get; set; }

		public Texture2D Texture { get; set; }
		protected PositionComponent entityPosition;
		protected Rectangle SourceRectangle { get; set; }

		public SpriteComponent(Texture2D texture)
		{
			this.Texture = texture;
		}

		public SpriteComponent(Texture2D texture, Rectangle SourceRectangle)
		{
			this.Texture = texture;
			this.SourceRectangle = SourceRectangle;
		}

		public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			if (this.entityPosition == null || this.entityPosition.Entity != this.Entity)
			{
				this.entityPosition = this.Entity?.GetComponent<PositionComponent>();
				if (this.entityPosition == null)
				{
					return;
				}
			}
			spriteBatch.Draw(this.Texture, this.entityPosition.Position - this.SourceRectangle.Size.ToVector2() / 2, this.SourceRectangle, Color.White);
		}

		public virtual void Initialize()
		{
		}

		public virtual void Update(GameTime gameTime)
		{
		}
	}
}
