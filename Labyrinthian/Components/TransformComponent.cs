using CHMonoTools.ECS;
using Microsoft.Xna.Framework;

namespace Labyrinthian
{
	class TransformComponent : Component
	{
		public Matrix Transform { get; set; } = Matrix.Identity;
	}
}
