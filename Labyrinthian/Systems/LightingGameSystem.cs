using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Labyrinthian
{
	class LightingGameSystem : DrawableGameSystem
	{
		List<LightSourceComponent> lightSourceComponents = new List<LightSourceComponent>();
		GraphicsDevice graphicsDevice;

		private RenderTarget2D lightfilter;

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

		public LightingGameSystem(EntityContainer entityContainer, GraphicsDevice graphicsDevice) : base(entityContainer)
		{
			this.graphicsDevice = graphicsDevice;
			RecreateRenderTarget();
		}

		public void RecreateRenderTarget()
		{
			if (this.CameraComponent != null)
			{
				this.CameraComponent.Viewport = this.graphicsDevice.Viewport;
			}
			this.lightfilter?.Dispose();
			this.lightfilter = new RenderTarget2D(
				this.graphicsDevice, 
				this.graphicsDevice.PresentationParameters.BackBufferWidth, 
				this.graphicsDevice.PresentationParameters.BackBufferHeight, 
				false, 
				this.graphicsDevice.PresentationParameters.BackBufferFormat, 
				DepthFormat.Depth24);
		}

		public void DrawRenderTarget(GameTime gameTime, SpriteBatch spriteBatch)
		{
			this.graphicsDevice.SetRenderTarget(this.lightfilter);
			this.graphicsDevice.Clear(new Color(0, 0, 0, 255));
			spriteBatch.Begin(blendState: this.blendstate, transformMatrix: (this.CameraComponent?.TransformationMatrix ?? Matrix.Identity));
			foreach (var lightsource in this.lightSourceComponents)
			{
				lightsource.Draw(gameTime, spriteBatch);
			}
			spriteBatch.End();

			this.graphicsDevice.SetRenderTarget(null);
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			spriteBatch.Draw(this.lightfilter, this.graphicsDevice.Viewport.Bounds, Color.White);
			spriteBatch.End();
		}

		public override void Update(GameTime gameTime)
		{
		}

		protected override void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is LightSourceComponent lComp)
			{
				this.lightSourceComponents.Add(lComp);
			}

			base.Entity_ComponentAdded(sender, e);
		}

		protected override void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is LightSourceComponent lcomp)
			{
				this.lightSourceComponents.Remove(lcomp);
			}

			base.Entity_ComponentRemoved(sender, e);
		}
	}
}
