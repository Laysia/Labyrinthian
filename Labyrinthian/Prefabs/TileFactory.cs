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
			var Entity = new Entity();
			Entity.Add(new PositionComponent(Position));
			Entity.Add(new PhysicsHitboxComponent(32, 32));
			Entity.Add(new SpriteComponent(LabyrinthianGame.Game.Content.Load<Texture2D>(@"Textures/GroundSheet"), wallTileSource));

			return Entity;
		}

		public static Entity CreateFloorTile(Vector2 Position)
		{
			var Entity = new Entity();
			Entity.Add(new PositionComponent(Position));
			Entity.Add(new SpriteComponent(LabyrinthianGame.Game.Content.Load<Texture2D>(@"Textures/GroundSheet"), groundTileSource));

			return Entity;
		}
	}
}
