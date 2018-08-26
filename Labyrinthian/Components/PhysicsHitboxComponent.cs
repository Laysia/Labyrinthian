using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinthian
{
	class PhysicsHitboxComponent : Component, IDrawableComponent
	{
		public static bool IsHitboxVisible {get; set;} = false;
		public PositionComponent EntityPosition { get; set; }
		public TransformComponent Transform { get; private set; }
		private Texture2D rectangleTexture;


		public int Width { get; set; }
		public int Height { get; set; }
		public Rectangle Hitbox
		{
			get
			{
				if (this.EntityPosition == null)
				{
					return Rectangle.Empty;
				}
				else
				{
					return new Rectangle(this.EntityPosition.Position.ToPoint() - new Point(this.Width / 2, this.Height / 2), new Point(this.Width, this.Height));
				}
			}
		}

		public PhysicsHitboxComponent(int Width, int Height)
		{
			this.Width = Width;
			this.Height = Height;
			this.rectangleTexture = ProgrammerArt.WhiteOutlinedRectangle;

		}

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			if (IsHitboxVisible)
			{
				spriteBatch.Draw(this.rectangleTexture, this.Hitbox, Color.White);
			}
		}

		protected override void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is PositionComponent p)
			{
				this.EntityPosition = p;
			}
			else if (e.Component is TransformComponent t)
			{
				this.Transform = t;
			}
			base.Entity_ComponentAdded(sender, e);
		}

		protected override void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is PositionComponent p)
			{
				this.EntityPosition = null;
			}
			else if (e.Component is TransformComponent t)
			{
				this.Transform = null;
			}
			base.Entity_ComponentRemoved(sender, e);
		}
	}
}
