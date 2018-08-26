using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CHMonoTools
{
	public interface IDrawable
	{
		void Draw(GameTime gameTime, SpriteBatch spriteBatch);
	}
}
