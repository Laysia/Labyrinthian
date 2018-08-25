using System;
using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinthian
{
	class AnimatedSpriteComponent : SpriteComponent
	{
		protected SpriteAnimator SpriteAnimator { get; set; }

		public AnimatedSpriteComponent(Texture2D texture, SpriteAnimator spriteAnimator) : base(texture)
		{
			this.SpriteAnimator = spriteAnimator;
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			this.SourceRectangle = GetSourceRectangle();
			base.Draw(gameTime, spriteBatch);
		}

		protected virtual Rectangle GetSourceRectangle()
		{
			return new Rectangle();
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void Update(GameTime gameTime)
		{
			this.SpriteAnimator.Update(gameTime);
			base.Update(gameTime);
		}
	}
}
