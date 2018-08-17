using CHMonoTools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinthian
{
	class GroundTile : IGameObject, IPosition
	{
		public Vector2 Position { get; set; }
		public Texture2D Texture;

		public GroundTile(Vector2 Position, Texture2D Texture)
		{
			this.Position = Position;
			this.Texture = Texture;
		}

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(this.Texture, this.Position, Color.White);
		}

		public void Initialize()
		{
		
		}

		public void Update(GameTime gameTime)
		{
			
		}
	}
}
