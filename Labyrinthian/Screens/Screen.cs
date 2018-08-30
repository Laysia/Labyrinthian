using CHMonoTools;
using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinthian
{
	class Screen : IGameObject
	{
		public Game Game { get; set; }
		public EntityContainer EntityContainer { get; set; }

		public LightingGameSystem LightingGameSystem { get; set; }
		public RenderGameSystem RenderGameSystem { get; set; }
		public UpdateGameSystem UpdateGameSystem { get; set; }
		public PhysicsGameSystem PhysicsGameSystem { get; set; }
		public LevelGameSystem LevelGameSystem { get; set; }
		public TileGameSystem TileGameSystem { get; set; }

		public Screen(Game game)
		{
			this.Game = game;
			this.Game.Window.ClientSizeChanged += window_ClientSizeChanged;
			setupGame();

		}

		private void setupGame()
		{
			this.EntityContainer = new EntityContainer();
			this.LightingGameSystem = new LightingGameSystem(this.EntityContainer, this.Game.GraphicsDevice);
			this.RenderGameSystem = new RenderGameSystem(this.EntityContainer);
			this.UpdateGameSystem = new UpdateGameSystem(this.EntityContainer);
			this.PhysicsGameSystem = new PhysicsGameSystem(this.EntityContainer);
			this.LevelGameSystem = new LevelGameSystem(this.EntityContainer);
			this.TileGameSystem = new TileGameSystem(this.EntityContainer);

			this.LevelGameSystem.SetupNewLevel();

		}

		private void window_ClientSizeChanged(object sender, System.EventArgs e)
		{
			this.LightingGameSystem.RecreateRenderTarget();
		}

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			this.LightingGameSystem.DrawRenderTarget(gameTime, spriteBatch);
			this.Game.GraphicsDevice.Clear(Color.CornflowerBlue);
			this.RenderGameSystem.Draw(gameTime, spriteBatch);

			this.LightingGameSystem.Draw(gameTime, spriteBatch);
		}

		public void Initialize()
		{
		}

		public void Update(GameTime gameTime)
		{
			this.UpdateGameSystem.StartUpdating(gameTime);
			this.UpdateGameSystem.Update(gameTime);
			this.PhysicsGameSystem.Update(gameTime);
			this.UpdateGameSystem.EndUpdating(gameTime);
			this.LevelGameSystem.Update(gameTime);
		}
	}
}
