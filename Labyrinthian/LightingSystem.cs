using CHMonoTools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Labyrinthian
{
	public class LightingSystem : IGameObject
	{
		public Texture2D LightTexture;
		public Texture2D BlackTexture;

		private GraphicsDevice graphicsDevice;
		private List<Tuple<IPosition, int>> lightSources = new List<Tuple<IPosition, int>>();
		public RenderTarget2D lightfilter;

		/*
		 * (source * sourceBlendFactor) + (destination * destinationBlendFactor)  
		 * (alpha_dest) - ( 1 - alpha_source )
		 * 
		 */
		BlendState blendstate = new BlendState()
		{
			ColorBlendFunction = BlendFunction.Add,
			ColorDestinationBlend = Blend.Zero,
			ColorSourceBlend = Blend.Zero,
			AlphaBlendFunction = BlendFunction.Subtract,
			AlphaDestinationBlend = Blend.InverseSourceAlpha,
			AlphaSourceBlend = Blend.DestinationAlpha
		};


		public LightingSystem(GraphicsDevice graphicsDevice)
		{
			this.graphicsDevice = graphicsDevice;
			this.LightTexture = ProgrammerArt.GradientBlackCircle;//LabyrinthianGame.Game.Content.Load<Texture2D>(@"Textures/test");
			this.BlackTexture = ProgrammerArt.BlackPixel;
			RecreateRenderTarget();
			
		}


		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(this.lightfilter, this.graphicsDevice.Viewport.Bounds, Color.White);
		}

		public void DrawRenderTarget(SpriteBatch spriteBatch)
		{
			this.graphicsDevice.SetRenderTarget(this.lightfilter);
			this.graphicsDevice.Clear(Color.Black);
			spriteBatch.Begin(blendState: this.blendstate);
			foreach (var lightsource in this.lightSources)
			{
				Vector2 centerPosition = lightsource.Item1.Position;
				int radius = lightsource.Item2;
				spriteBatch.Draw(this.LightTexture, new Rectangle((int)centerPosition.X - radius, (int)centerPosition.Y - radius, 2 * radius, 2 * radius), Color.White);
			}
			spriteBatch.End();

			this.graphicsDevice.SetRenderTarget(null);
		}

		internal void RecreateRenderTarget()
		{
			this.lightfilter?.Dispose();
			this.lightfilter = new RenderTarget2D(this.graphicsDevice, this.graphicsDevice.PresentationParameters.BackBufferWidth, this.graphicsDevice.PresentationParameters.BackBufferHeight, false, this.graphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
		}

		/// <summary>
		/// Currently only supports single Lightsource, adding a new one overwrites the old one.
		/// </summary>
		/// <param name="sourcePosition"></param>
		/// <param name="radius"></param>
		public void AddLightSource(IPosition sourcePosition, int radius)
		{
			this.lightSources.Add(new Tuple<IPosition, int>(sourcePosition, radius));
		}

		public void Initialize()
		{
		}

		public void Update(GameTime gameTime)
		{
		}
	}
}
