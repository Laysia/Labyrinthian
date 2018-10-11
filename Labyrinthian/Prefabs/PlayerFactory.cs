using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinthian.Prefabs
{
	public static class PlayerFactory
	{
		public static Entity CreatePlayerEntity(Vector2 Position)
		{
			Entity player = Entity.CreateNew();

			player.Add(new PlayerInputComponent());
			var component = new TilePositionComponent(Position);
			LabyrinthianGame.PlayerPosition = component;
			player.Add(component);
			player.Add(new TransformComponent());
			player.Add(new PlayerSpriteComponent(LabyrinthianGame.Game.Content.Load<Texture2D>(@"Textures/PlayerFemale"), new SpriteAnimator(SpriteAnimator.PlayerRectangles) { TimeBetweenAnimationInMs = 200 }));
			player.Add(new BoxColliderComponent(new Point(6,3), new Point(20,26)));
			player.Add(new RigidBodyComponent(BodyType.Dynamic));
			player.Add(new LightSourceComponent(100) { Offset = new Point(16, 16) });
			player.Add(new CameraComponent(LabyrinthianGame.Game.GraphicsDevice.Viewport));
			player.Add(new LevelCompleterComponent());

			return player;
		}
	}
}
