using Microsoft.Xna.Framework;

namespace CHMonoTools.ECS
{
	public class TransformComponent : Component, IMovementComponent
	{
		public Vector2 Movement { get; set; } = Vector2.Zero;
	}
}
