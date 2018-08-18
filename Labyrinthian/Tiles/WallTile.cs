using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Labyrinthian
{
	class WallTile : Tile, IHitbox
	{
		Rectangle sourceRectangle = new Rectangle(0, 160, 32, 32);

		public WallTile(int X, int Y) : base(X, Y)
		{
		}

		public Rectangle Hitbox
		{
			get
			{
				return this.TileRectangle;
			}
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(
				this.Texture, 
				this.TileRectangle,
				this.sourceRectangle, 
				Color.White);
			base.Draw(gameTime, spriteBatch);
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}
	}
}
