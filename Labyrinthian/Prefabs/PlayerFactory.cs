using CHMonoTools.ECS;
using Labyrinthian.Components;

namespace Labyrinthian.Prefabs
{
	public static class PlayerFactory
	{
		public static Entity CreatePlayerEntity()
		{
			Entity player = new Entity();

			player.Add(new PlayerInputComponent());
			var component = new PositionComponent() { Position = new Microsoft.Xna.Framework.Vector2(200, 200) };
			LabyrinthianGame.playerPosition = component;
			player.Add(component);
			player.Add(new TransformComponent());
			player.Add(new PlayerDrawComponent());
			player.Add(new PhysicsHitboxComponent());
			player.Add(new LightSourceComponent(100));
			player.Add(new CameraComponent());

			return player;
		}
	}
}
