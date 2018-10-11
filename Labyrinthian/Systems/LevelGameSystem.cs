using CHMonoTools.ECS;
using Labyrinthian.Prefabs;
using Microsoft.Xna.Framework;
using System;

namespace Labyrinthian
{
	class LevelGameSystem : GameSystem
	{
		private EntityContainer entityContainer;
		private LevelCompleteEventComponent levelCompleteEventComponent;
		private LevelCompleterComponent levelCompleterComponent;
		static Random rng = new Random();

		public LevelGameSystem(EntityContainer entityContainer) : base(entityContainer)
		{
			this.entityContainer = entityContainer;
		}

		public void SetupNewLevel()
		{
			this.entityContainer.Clear();

			var startPosition = createTiles();

			this.entityContainer.Add(PlayerFactory.CreatePlayerEntity(startPosition));
		}

		private Vector2 createTiles()
		{
			Vector2 returnPosition = Vector2.Zero;

			int width = 10;
			int height = 10;

			int scale = 2;

			MazeGenerator gen = new MazeGenerator(width, height, new Point(1, 1), false);
			byte[,] maze = gen.CreateMaze();
			bool hasStartTile = false;
			bool hasEndTile = false;
			for (int x = 0; x < width; ++x)
			{
				for (int y = 0; y < height; ++y)
				{
					for (int i = 0; i < scale; ++i)
					{
						for (int j = 0; j < scale; ++j)
						{
							if (maze[x, y] == 1)
							{
								this.entityContainer.Add(TileFactory.CreateWallTile(new Vector2(x * scale + i, y * scale + j) * 32));
							}
							else
							{
								if (gen.FurthestPoint == new Point(x, y) && !hasEndTile)
								{
									Entity endTile = TileFactory.CreateEndTile(new Vector2(x * scale + i, y * scale + j) * 32);
									this.entityContainer.Add(endTile);
									hasEndTile = true;
								}
								else if ((x == 1 && y == 1) && !hasStartTile)
								{
									returnPosition = new Vector2(x * scale + i, y * scale + j) * 32;
									Entity startTile = TileFactory.CreateStartTile(returnPosition);
									this.entityContainer.Add(startTile);
									hasStartTile = true;
								}
								else
								{
									this.entityContainer.Add(TileFactory.CreateFloorTile(new Vector2(x * scale + i, y * scale + j) * 32));
								}
								if (rng.NextDouble() > 0.9d)
								{
									this.entityContainer.Add(TorchFactory.CreateTorch(50, new Vector2(x * scale + i, y * scale + j) * 32));
								}
							}
						}
					}
				}
			}
			return returnPosition;
		}

		public override void Update(GameTime gameTime)
		{
			if (this.levelCompleterComponent != null && this.levelCompleteEventComponent != null)
			{
				if ((this.levelCompleterComponent.EntityHitbox?.Box ?? Rectangle.Empty).Intersects(this.levelCompleteEventComponent.EntityHitbox?.Box ?? Rectangle.Empty))
				{
					levelComplete();
				}
			}
		}

		private void levelComplete()
		{
			SetupNewLevel();
			LabyrinthianGame.Game.levelCounter++;
		}

		protected override void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is LevelCompleteEventComponent l)
			{
				this.levelCompleteEventComponent = l;
			}
			else if (e.Component is LevelCompleterComponent player)
			{
				this.levelCompleterComponent = player;
			}
		}

		protected override void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e)
		{
			if (e.Component == this.levelCompleteEventComponent)
			{
				this.levelCompleteEventComponent = null;
			}
			else if (e.Component == this.levelCompleterComponent)
			{
				this.levelCompleterComponent = null;
			}
		}
	}
}
