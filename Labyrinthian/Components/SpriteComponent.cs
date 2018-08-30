using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinthian
{
	class SpriteComponent : Component, IDrawableComponent
	{
		public Texture2D Texture { get; set; }
		public Color Color { get; set; } = Color.White;
		protected TilePositionComponent entityPosition;
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
			if (this.entityPosition == null)
			{
				return;
			}
			spriteBatch.Draw(this.Texture, this.entityPosition.Position - this.SourceRectangle.Size.ToVector2() / 2, this.SourceRectangle, this.Color);
		}
		protected override void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is TilePositionComponent p)
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
