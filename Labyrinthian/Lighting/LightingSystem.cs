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
		private List<LightSource> lightSources = new List<LightSource>();
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
			
			this.BlackTexture = ProgrammerArt.BlackPixel;
			RecreateRenderTarget();
			
		}


		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(this.lightfilter, this.graphicsDevice.Viewport.Bounds, Color.White);
		}

		public void RecreateRenderTarget()
		{
			this.lightfilter?.Dispose();
			this.lightfilter = new RenderTarget2D(this.graphicsDevice, this.graphicsDevice.PresentationParameters.BackBufferWidth, this.graphicsDevice.PresentationParameters.BackBufferHeight, false, this.graphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
		}

		public void DrawRenderTarget(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
		{
			this.graphicsDevice.SetRenderTarget(this.lightfilter);
			this.graphicsDevice.Clear(new Color(0,0,0,255));
			spriteBatch.Begin(blendState: this.blendstate, transformMatrix: (camera.TransformationMatrix));
			foreach (var lightsource in this.lightSources)
			{
				lightsource.Draw(gameTime, spriteBatch);
			}
			spriteBatch.End();

			this.graphicsDevice.SetRenderTarget(null);
			
			
			//this.graphicsDevice.SetRenderTarget(this.lightfilter);
			//this.graphicsDevice.Clear(Color.Green);
			//spriteBatch.Begin(blendState: this.blendstate);
			//foreach (var lightsource in this.lightSources)
			//{
			//	Vector2 centerPosition = lightsource.Item1.Position;
			//	int radius = lightsource.Item2;
			//	spriteBatch.Draw(this.LightTexture, new Rectangle((int)centerPosition.X - radius, (int)centerPosition.Y - radius, 2 * radius, 2 * radius), Color.White);
			//}
			//spriteBatch.End();

			//this.graphicsDevice.SetRenderTarget(null);
		}

		public void AddLightSource(LightSource lightSource)
		{
			this.lightSources.Add(lightSource);
		}

		public void Initialize()
		{
		}

		public void Update(GameTime gameTime)
		{
		}
	}
}
