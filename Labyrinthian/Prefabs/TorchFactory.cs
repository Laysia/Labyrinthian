using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinthian.Prefabs
{
	public static class TorchFactory
	{
		public static Entity CreateTorch(int Radius, Vector2 Position)
		{
			var entity = new Entity();
			entity.Add(new PositionComponent() { Position = Position });
			entity.Add(new TorchSpriteComponent(LabyrinthianGame.Game.Content.Load<Texture2D>(@"Textures/Torch"), new SpriteAnimator(SpriteAnimator.TorchRectangles) { TimeBetweenAnimationInMs = 200 }));
			entity.Add(new LightSourceComponent(Radius) { Flickering = true });
			entity.Add(new PhysicsHitboxComponent(10, 20));

			return entity;
		}
	}
}
