using CHMonoTools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Labyrinthian
{
	public class LightSource : CHMonoTools.IDrawable
	{
		public IPosition Position { get; set; }
		public static Texture2D CircularLightTexture { get; set; } = ProgrammerArt.CreateGradientBlackCircle(100, 1.0);
		public static List<Texture2D> CircularLightTextureAlternate { get; set; } = new List<Texture2D>()
		{
			//ProgrammerArt.createGradientBlackCircle(100, 3.0),
			ProgrammerArt.CreateGradientBlackCircle(100, 1.3),
			ProgrammerArt.CreateGradientBlackCircle(100, .7),
			ProgrammerArt.CreateGradientBlackCircle(100, .5),
			ProgrammerArt.CreateGradientBlackCircle(100, .6),
			ProgrammerArt.CreateGradientBlackCircle(100, .9)
		};
		public int Radius { get; set; }
		public bool Flickering { get; set; } = false;

		private double flickerTimerInMS = 40.0;
		private double flickerTimerProgress = 0.0;
		private Texture2D currentTexture;
		bool isFlickering = false;

		static Random rng = new Random();

		public LightSource(IPosition Position, int Radius)
		{
			this.Position = Position;
			this.Radius = Radius;
			this.currentTexture = CircularLightTexture;
		}

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			if (this.isFlickering)
			{
				this.flickerTimerProgress += gameTime.ElapsedGameTime.TotalMilliseconds;
				if (this.flickerTimerProgress > this.flickerTimerInMS)
				{
					// Flicker ended
					this.flickerTimerProgress = 0.0;
					this.currentTexture = CircularLightTexture;
					this.isFlickering = false;
				}
			}
			else if (this.Flickering)
			{
				if (rng.NextDouble() > 0.95)
				{
					this.isFlickering = true;
					this.currentTexture = CircularLightTextureAlternate[rng.Next(0, CircularLightTextureAlternate.Count - 1)];
				}
			}
				


			Vector2 position = this.Position.Position;
			spriteBatch.Draw(
				this.currentTexture, 
				new Rectangle((int)this.Position.Position.X - this.Radius, (int)position.Y - this.Radius, 2 * this.Radius, 2 * this.Radius),
				Color.White);
		}
	}
}
