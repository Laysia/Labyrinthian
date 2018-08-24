using CHMonoTools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Labyrinthian
{
	public class TileSystem : IGameObject
	{
		public Tile[,] Tiles { get; set; }
		public Tile StartTile { get; set; }
		public Tile EndTile { get; set; }

		public TileSystem()
		{

		}

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			foreach (var tile in this.Tiles)
			{
				tile.Draw(gameTime, spriteBatch);
			}
		}

		public void Initialize()
		{
			createTiles();
		}

		private void createTiles()
		{
			int width = 10;
			int height = 10;

			int scale = 2;

			this.Tiles = new Tile[width * scale, height * scale];
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
							Tile tile;
							if (maze[x,y] == 1)
							{
								tile = new WallTile(x * scale + i, y * scale + j);
							}
							else
							{
								if (gen.FurthestPoint == new Point(x,y) && this.EndTile == null)
								{
									tile = new GroundTile(x * scale + i, y * scale + j) { TestColor = Color.LightGreen };
									this.EndTile = tile;
								}
								else if ((x == 1 && y == 1) && this.StartTile == null)
								{
									tile = new GroundTile(x * scale + i, y * scale + j) { TestColor = Color.Black };
									this.StartTile = tile;
								}
								else
								{
									tile = new GroundTile(x * scale + i, y * scale + j);
								}
							}
							this.Tiles[x * scale + i, y * scale + j] = tile;
						}
					}
				}
			}
		}

		public void Update(GameTime gameTime)
		{
			foreach (var tile in this.Tiles)
			{
				tile.Update(gameTime);
			}
		}
	}
}
