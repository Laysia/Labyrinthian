using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Labyrinthian
{
	class RenderGameSystem : DrawableGameSystem
	{
		List<IDrawableComponent> drawableComponents = new List<IDrawableComponent>();
		List<BoxColliderComponent> hitboxes = new List<BoxColliderComponent>();

		public RenderGameSystem(EntityContainer entityContainer) : base(entityContainer)
		{
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Begin(transformMatrix: this.CameraComponent?.TransformationMatrix ?? Matrix.Identity);
			foreach (var drawableComponent in this.drawableComponents)
			{
				drawableComponent.Draw(gameTime, spriteBatch);
			}
			foreach (var hitbox in this.hitboxes)
			{
				spriteBatch.Draw(ProgrammerArt.WhiteOutlinedRectangle, hitbox.Box, Color.White);
			}
			spriteBatch.End();
		}

		public override void Update(GameTime gameTime)
		{

		}

		protected override void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is IDrawableComponent d)
			{
				if (d is LightSourceComponent l)
				{
					return;
				}
				else
				{
					this.drawableComponents.Add(d);
				}
			}
			else if (e.Component is BoxColliderComponent h)
			{
				this.hitboxes.Add(h);
			}

			base.Entity_ComponentAdded(sender, e);
		}

		protected override void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is IDrawableComponent d)
			{
				if (d is LightSourceComponent l)
				{
					return;
				}
				else
				{
					this.drawableComponents.Remove(d);
				}
			}
			else if (e.Component is BoxColliderComponent h)
			{
				this.hitboxes.Remove(h);
			}

			base.Entity_ComponentRemoved(sender, e);
		}
	}
}
