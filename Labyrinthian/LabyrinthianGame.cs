using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Labyrinthian
{
	public class LabyrinthianGame : Game
    {
		public static LabyrinthianGame Game = new LabyrinthianGame();

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
		TileSystem tileSystem;
		Player player;
		Player player2;
		LightingSystem lighting;
		PhysicsSystem physicsSystem;
		Camera camera;
		List<Torch> torches = new List<Torch>();

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

			this.tileSystem = new TileSystem();
			this.tileSystem.Initialize();

			this.player = new Player
			{
				Position = this.tileSystem.StartTile.TileRectangle.Center.ToVector2()
			};
			this.player.Initialize();

			this.player2 = new Player
			{
				Position = this.tileSystem.EndTile.TileRectangle.Center.ToVector2()
			};
			this.player.Initialize();

			this.lighting = new LightingSystem(this.GraphicsDevice);
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					Torch torch = new Torch(new Vector2(i * 64 + 16, j * 64 + 16));
					this.torches.Add(torch);
					this.lighting.AddLightSource(new LightSource(torch, 50) { Flickering = true });
				}
			}

			this.lighting.AddLightSource(new LightSource(this.player, 100));
			this.lighting.AddLightSource(new LightSource(this.player2, 100));

			this.physicsSystem = new PhysicsSystem
			{
				Player = this.player,
				Tiles = new IHitbox[this.tileSystem.Tiles.GetLength(0), this.tileSystem.Tiles.GetLength(1)]
			};
			foreach (var tile in this.tileSystem.Tiles)
			{
				if (tile is IHitbox box)
				{
					this.physicsSystem.Tiles[tile.X, tile.Y] = box;
				}
			}

			this.camera = new Camera(this.GraphicsDevice.Viewport, 1.0f, this.player);
		}

		private void window_ClientSizeChanged(object sender, System.EventArgs e)
		{
			this.lighting?.RecreateRenderTarget();
			this.camera?.SetViewport(this.GraphicsDevice.Viewport);
		}

		protected override void LoadContent()
        {
			this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

			this.Content.Load<Texture2D>(@"Textures/PlayerFemale");
			this.Content.Load<Texture2D>(@"Textures/GroundSheet");
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
			this.tileSystem.Update(gameTime);
			this.physicsSystem.Update(gameTime);
			this.camera.Update(gameTime);
			base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
			this.lighting.DrawRenderTarget(gameTime, this.spriteBatch, this.camera);

			this.GraphicsDevice.Clear(Color.CornflowerBlue);

			this.spriteBatch.Begin(transformMatrix: this.camera.TransformationMatrix);
			this.tileSystem.Draw(gameTime, this.spriteBatch);
			foreach (var torch in this.torches)
			{
				torch.Draw(gameTime, this.spriteBatch);
			}
			this.player.Draw(gameTime, this.spriteBatch);
			this.spriteBatch.End();
			this.spriteBatch.Begin();
			this.lighting.Draw(gameTime, this.spriteBatch);
			this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
