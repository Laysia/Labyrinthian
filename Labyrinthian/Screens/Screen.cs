using CHMonoTools;
using CHMonoTools.ECS;
using Labyrinthian.Prefabs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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


		public Screen(Game game)
		{
			this.Game = game;
			this.Game.Window.ClientSizeChanged += window_ClientSizeChanged;
			setupGame();

		}

		private void setupGame()
		{
			Random rng = new Random();

			this.EntityContainer = new EntityContainer();
			this.LightingGameSystem = new LightingGameSystem(this.EntityContainer, this.Game.GraphicsDevice);
			this.RenderGameSystem = new RenderGameSystem(this.EntityContainer);
			this.UpdateGameSystem = new UpdateGameSystem(this.EntityContainer);
			this.PhysicsGameSystem = new PhysicsGameSystem(this.EntityContainer);

			createTiles();
			for (int i = 0; i < 20; i++)
			{
				for (int j = 0; j < 20; j++)
				{
					if (rng.NextDouble() > 0.9)
					{
						Entity torch = TorchFactory.CreateTorch(50, new Vector2(i * 64 + 16, j * 64 + 16));
						this.EntityContainer.Add(torch);
					}
				}
			}
			this.EntityContainer.Add(PlayerFactory.CreatePlayerEntity());

		}

		private void createTiles()
		{
			int width = 10;
			int height = 10;

			int scale = 1;

			MazeGenerator gen = new MazeGenerator(width, height, new Point(1, 1), false);
			byte[,] maze = gen.CreateMaze();

			for (int x = 0; x < width; ++x)
			{
				for (int y = 0; y < height; ++y)
				{
					for (int i = 0; i < scale; ++i)
					{
						for (int j = 0; j < scale; ++j)
						{
							Entity tile;
							if (maze[x, y] == 1)
							{
								tile = TileFactory.CreateWallTile(new Vector2(x * scale + i, y * scale + j) * 32);
							}
							else
							{
								if (gen.FurthestPoint == new Point(x, y)/* && this.EndTile == null*/)
								{
									tile = TileFactory.CreateFloorTile(new Vector2(x * scale + i, y * scale + j) * 32); // { TestColor = Color.LightGreen };
									//this.EndTile = tile;
								}
								else if ((x == 1 && y == 1) /*&& this.StartTile == null*/)
								{
									tile = TileFactory.CreateFloorTile(new Vector2(x * scale + i, y * scale + j) * 32); // { TestColor = Color.Black };
									//this.StartTile = tile;
								}
								else
								{
									tile = TileFactory.CreateFloorTile(new Vector2(x * scale + i, y * scale + j) * 32);
								}
							}
							this.EntityContainer.Add(tile);
						}
					}
				}
			}
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
			this.PhysicsGameSystem.Update(gameTime);
			this.UpdateGameSystem.Update(gameTime);
		}
	}
}
