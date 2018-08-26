using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CHMonoTools
{
	public abstract class GameObject : IGameObject
	{
		public Game Game { get; set; }

		public GameObject(Game game)
		{
			this.Game = game;
		}

		public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
		public abstract void Initialize();
		public abstract void Update(GameTime gameTime);
	}
}
