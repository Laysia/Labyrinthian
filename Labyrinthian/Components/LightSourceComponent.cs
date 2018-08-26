using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Labyrinthian
{
	class LightSourceComponent : Component, IDrawableComponent
	{
		static Random rng = new Random();

		private PositionComponent entityPosition;

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
		private bool isFlickering = false;

		public LightSourceComponent(int Radius)
		{
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
			if (this.entityPosition != null)
			{
				spriteBatch.Draw(
					this.currentTexture,
					new Rectangle((int)this.entityPosition.Position.X - this.Radius, (int)this.entityPosition.Position.Y - this.Radius, 2 * this.Radius, 2 * this.Radius),
					Color.White);
			}
		}
		protected override void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is PositionComponent p)
			{
				this.entityPosition = p;
			}
			base.Entity_ComponentAdded(sender, e);
		}
		protected override void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e)
		{
			if (e.Component == this.entityPosition)
			{
				this.entityPosition = null;
			}
			base.Entity_ComponentRemoved(sender, e);
		}
	}
}
