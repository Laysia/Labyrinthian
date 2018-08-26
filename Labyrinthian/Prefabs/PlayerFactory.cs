using CHMonoTools.ECS;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinthian.Prefabs
{
	public static class PlayerFactory
	{
		public static Entity CreatePlayerEntity()
		{
			Entity player = new Entity();

			player.Add(new PlayerInputComponent());
			var component = new PositionComponent() { Position = new Microsoft.Xna.Framework.Vector2(200, 200) };
			LabyrinthianGame.PlayerPosition = component;
			player.Add(component);
			player.Add(new TransformComponent());
			player.Add(new PlayerSpriteComponent(LabyrinthianGame.Game.Content.Load<Texture2D>(@"Textures/PlayerFemale"), new SpriteAnimator(SpriteAnimator.PlayerRectangles) { TimeBetweenAnimationInMs = 200 }));
			player.Add(new PhysicsHitboxComponent(20, 26));
			player.Add(new LightSourceComponent(100));
			player.Add(new CameraComponent());

			return player;
		}
	}
}
