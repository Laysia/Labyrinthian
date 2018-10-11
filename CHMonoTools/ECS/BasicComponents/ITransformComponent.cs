using Microsoft.Xna.Framework;

namespace CHMonoTools.ECS
{
	public interface IMovementComponent : IComponent
	{
		Vector2 Movement { get; set; }
	}
}