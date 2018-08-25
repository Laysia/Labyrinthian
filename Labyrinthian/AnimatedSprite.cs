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


		public static Dictionary<string, Rectangle[]> PlayerRectangles = new Dictionary<string, Rectangle[]>
		{
			{ "up", new Rectangle[] { new Rectangle(0, 0, 32, 32) } },
			{ "left", new Rectangle[] { new Rectangle(0, 32, 32, 32) } },
			{ "down", new Rectangle[] { new Rectangle(0, 64, 32, 32) } },
			{ "right", new Rectangle[] { new Rectangle(0, 96, 32, 32) } },
			{ "walk_up", new Rectangle[] { new Rectangle(32, 0, 32, 32), new Rectangle(0, 0, 32, 32), new Rectangle(64, 0, 32, 32), new Rectangle(0, 0, 32, 32) } },
			{ "walk_left", new Rectangle[] { new Rectangle(32, 32, 32, 32), new Rectangle(0, 32, 32, 32), new Rectangle(64, 32, 32, 32), new Rectangle(0, 32, 32, 32) } },
			{ "walk_down", new Rectangle[] { new Rectangle(32, 64, 32, 32), new Rectangle(0, 64, 32, 32), new Rectangle(64, 64, 32, 32), new Rectangle(0, 64, 32, 32) } },
			{ "walk_right", new Rectangle[] { new Rectangle(32, 96, 32, 32), new Rectangle(0, 96, 32, 32), new Rectangle(64, 96, 32, 32), new Rectangle(0, 96, 32, 32) } }
		};

		public static Dictionary<string, Rectangle[]> TorchRectangles = new Dictionary<string, Rectangle[]>
		{
			{ "visible", new Rectangle[] { new Rectangle(0, 0, 10, 23), new Rectangle(10, 0, 10, 23), new Rectangle(20, 0, 10, 23) } },
			{ "invisible", new Rectangle[] { new Rectangle(0, 0, 1, 1) } }
		};
	}
}
