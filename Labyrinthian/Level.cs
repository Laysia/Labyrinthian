using CHMonoTools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinthian
{
	public class Level : GameObject
	{
		public event EventHandler LevelComplete;



		Player player;
		TileSystem tileSystem;
		LightingSystem lighting;
		PhysicsSystem physicsSystem;
		Camera camera;

		List<Torch> torches = new List<Torch>();

		public Level(Game game) : base(game)
		{
			this.Game.Window.ClientSizeChanged += window_ClientSizeChanged;
		}

		private void window_ClientSizeChanged(object sender, EventArgs e)
		{
			this.lighting?.RecreateRenderTarget();
			this.camera?.SetViewport(this.Game.GraphicsDevice.Viewport);
		}

		public override void Initialize()
		{
			this.tileSystem = new TileSystem();
			this.tileSystem.Initialize();

			this.player = new Player
			{
				Position = this.tileSystem.StartTile.TileRectangle.Center.ToVector2()
			};
			this.player.Initialize();

			this.lighting = new LightingSystem(this.Game.GraphicsDevice);
			Random rng = new Random();
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 10; j++)
				{
					if (rng.NextDouble() > 0.9)
					{
						Torch torch = new Torch(new Vector2(i * 64 + 16, j * 64 + 16));
						this.torches.Add(torch);
						this.lighting.AddLightSource(new LightSource(torch, 50) { Flickering = true });
					}
				}
			}

			this.lighting.AddLightSource(new LightSource(this.player, 100));

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

			this.camera = new Camera(this.Game.GraphicsDevice.Viewport, 1.0f, this.player);
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			this.lighting.DrawRenderTarget(gameTime, spriteBatch, this.camera);


			spriteBatch.Begin(transformMatrix: this.camera.TransformationMatrix);
			this.tileSystem.Draw(gameTime, spriteBatch);
			foreach (var torch in this.torches)
			{
				torch.Draw(gameTime, spriteBatch);
			}
			this.player.Draw(gameTime, spriteBatch);
			spriteBatch.End();

			spriteBatch.Begin();
			this.lighting.Draw(gameTime, spriteBatch);
			spriteBatch.End();
		}


		public override void Update(GameTime gameTime)
		{
			this.player.Update(gameTime);
			this.tileSystem.Update(gameTime);
			this.physicsSystem.Update(gameTime);
			this.camera.Update(gameTime);

			if (this.tileSystem.EndTile.TileRectangle.Contains(this.player.Position))
			{
				this.LevelComplete?.Invoke(this, EventArgs.Empty);
			}
		}
	}
}
