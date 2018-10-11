using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinthian.Prefabs
{
	public static class TileFactory
	{
		static Rectangle groundTileSource = new Rectangle(96, 128, 32, 32);
		static Rectangle wallTileSource = new Rectangle(0, 160, 32, 32);


		public static Entity CreateWallTile(Vector2 Position)
		{
			var entity = Entity.CreateNew();
			entity.Add(new TilePositionComponent(Position));
			entity.Add(new BoxColliderComponent(Point.Zero, new Point(32, 32)));
			entity.Add(new RigidBodyComponent(BodyType.Static));
			entity.Add(new SpriteComponent(LabyrinthianGame.Game.Content.Load<Texture2D>(@"Textures/GroundSheet"), wallTileSource));

			return entity;
		}

		public static Entity CreateFloorTile(Vector2 Position)
		{
			var entity = Entity.CreateNew();
			entity.Add(new TilePositionComponent(Position));
			entity.Add(new SpriteComponent(LabyrinthianGame.Game.Content.Load<Texture2D>(@"Textures/GroundSheet"), groundTileSource));

			return entity;
		}

		public static Entity CreateStartTile(Vector2 Position)
		{
			var entity = Entity.CreateNew();
			entity.Add(new TilePositionComponent(Position));
			entity.Add(new SpriteComponent(LabyrinthianGame.Game.Content.Load<Texture2D>(@"Textures/GroundSheet"), groundTileSource) { Color = Color.Green });

			return entity;
		}

		public static Entity CreateEndTile(Vector2 Position)
		{
			var entity = Entity.CreateNew();
			entity.Add(new TilePositionComponent(Position));
			entity.Add(new SpriteComponent(LabyrinthianGame.Game.Content.Load<Texture2D>(@"Textures/GroundSheet"), groundTileSource) { Color = Color.Red });
			entity.Add(new BoxColliderComponent(new Point(8, 8), new Point(16, 16)));
			entity.Add(new LevelCompleteEventComponent());

			return entity;
		}
	}
}
