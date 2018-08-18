using CHMonoTools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinthian
{
	public class GroundTile : Tile
	{
		Rectangle sourceRectangle = new Rectangle(96, 128, 32, 32);
		public Color TestColor { get; set; } = Color.White;

		public GroundTile(int X, int Y) : base(X, Y)
		{
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(
				this.Texture, 
				this.TileRectangle, 
				this.sourceRectangle, 
				this.TestColor);
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
