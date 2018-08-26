using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CHMonoTools.Gui
{
	public abstract class MonoControl : IGameObject
	{
		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			throw new NotImplementedException();
		}

		public void Initialize()
		{
			throw new NotImplementedException();
		}

		public void Update(GameTime gameTime)
		{
			throw new NotImplementedException();
		}
	}
}
