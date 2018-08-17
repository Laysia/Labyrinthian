using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Labyrinthian
{
	public class LabyrinthianGame : Game
    {
		public static LabyrinthianGame Game = new LabyrinthianGame();

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

		Player player;
		Player player2;
		GroundTile[,] tiles;
		LightingSystem lighting;

        public LabyrinthianGame()
        {
			this.graphics = new GraphicsDeviceManager(this);
			this.Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
			this.Window.Title = "Labyrinthian";
			this.IsMouseVisible = true;
			this.Window.ClientSizeChanged += window_ClientSizeChanged;
			this.Window.AllowUserResizing = true;
            base.Initialize();
			this.player = new Player();
			this.player.Initialize();
			this.player2 = new Player() { Position = new Vector2(200, 200), speed = 1.0f };
			this.player2.Initialize();
			this.tiles = new GroundTile[20, 20];
			for (int i = 0; i < this.tiles.GetLength(0); ++i)
			{
				for (int j = 0; j < this.tiles.GetLength(1); ++j)
				{
					Vector2 position = new Vector2(32 * i, 32 * j);
					this.tiles[i, j] = new GroundTile(position, this.Content.Load<Texture2D>(@"Textures/Ground/groundTile"));
				}
			}
			this.lighting = new LightingSystem(this.GraphicsDevice);
			this.lighting.AddLightSource(this.player, 100);
			this.lighting.AddLightSource(this.player2, 200);
		}

		private void window_ClientSizeChanged(object sender, System.EventArgs e)
		{
			this.lighting?.RecreateRenderTarget();
		}

		protected override void LoadContent()
        {
			this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

			this.Content.Load<Texture2D>(@"Textures/PlayerFemale");
			this.Content.Load<Texture2D>(@"Textures/Ground/groundTile");
			this.Content.Load<Texture2D>(@"Textures/test");
		}

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}

			this.player.Update(gameTime);
			this.player2.Update(gameTime);
			base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
			this.lighting.DrawRenderTarget(this.spriteBatch);

			this.GraphicsDevice.Clear(Color.CornflowerBlue);

			this.spriteBatch.Begin();
			foreach (var tile in this.tiles)
			{
				tile.Draw(gameTime, this.spriteBatch);
			}
			this.player.Draw(gameTime, this.spriteBatch);
			this.player2.Draw(gameTime, this.spriteBatch);
			this.lighting.Draw(gameTime, this.spriteBatch);
			this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
