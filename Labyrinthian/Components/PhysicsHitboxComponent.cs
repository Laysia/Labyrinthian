using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinthian
{
	class PhysicsHitboxComponent : IDrawableComponent
	{
		public static bool IsHitboxVisible {get; set;} = false;
		public Entity Entity { get; set; }
		public PositionComponent EntityPosition { get; set; }
		private Texture2D rectangleTexture;


		public int Width { get; set; }
		public int Height { get; set; }
		public Rectangle Hitbox
		{
			get
			{
				return new Rectangle(this.EntityPosition?.Position.ToPoint() - new Point(this.Width / 2, this.Height / 2) ?? Vector2.Zero.ToPoint(), new Point(this.Width, this.Height));
			}
		}

		public PhysicsHitboxComponent(int Width, int Height)
		{
			this.Width = Width;
			this.Height = Height;
			this.EntityPosition = this.Entity?.GetComponent<PositionComponent>();
			this.rectangleTexture = ProgrammerArt.WhiteOutlinedRectangle;

		}

		public void Initialize()
		{
		}

		public void Update(GameTime gameTime)
		{
			if (this.EntityPosition == null || this.EntityPosition.Entity != this.Entity)
			{
				this.EntityPosition = this.Entity?.GetComponent<PositionComponent>();
				if (this.EntityPosition == null)
				{
					return;
				}
			}
		}

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			if (IsHitboxVisible)
			{
				spriteBatch.Draw(this.rectangleTexture, this.Hitbox, Color.White);
			}
		}
	}
}
