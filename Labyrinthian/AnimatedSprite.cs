using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Labyrinthian
{
	class SpriteAnimator : CHMonoTools.IUpdateable
	{
		private Dictionary<string, Rectangle[]> animation;
		public double TimeBetweenAnimationInMs { get; set; } = 200.0;
		private double timeSinceLastAnimationSwitch = 0.0;
		string lastSpriteName = "";
		int lastSpriteCount = 0;

		public SpriteAnimator(Dictionary<string, Rectangle[]> animation)
		{
			this.animation = animation;
		}

		public Rectangle GetSourceRectangle(string spriteName)
		{
			if (this.lastSpriteName != spriteName)
			{
				this.lastSpriteName = spriteName;
				this.timeSinceLastAnimationSwitch = 0;
				this.lastSpriteCount = 0;
				return this.animation[spriteName][0];
			}

			var sprites = this.animation[spriteName];
			
			if (this.timeSinceLastAnimationSwitch - this.TimeBetweenAnimationInMs < 0)
			{
				return sprites[this.lastSpriteCount];
			}
			else
			{
				while (this.timeSinceLastAnimationSwitch - this.TimeBetweenAnimationInMs > 0)
				{
					this.timeSinceLastAnimationSwitch -= this.TimeBetweenAnimationInMs;
					if (this.lastSpriteCount == sprites.Length - 1)
					{
						this.lastSpriteCount = 0;
					}
					else
					{
						this.lastSpriteCount++;
					}
				}
				return sprites[this.lastSpriteCount];
			}


		}

		public void Update(GameTime gameTime)
		{
			this.timeSinceLastAnimationSwitch += gameTime.ElapsedGameTime.TotalMilliseconds;
		}
	}
}
