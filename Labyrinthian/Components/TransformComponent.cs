using CHMonoTools.ECS;
using Microsoft.Xna.Framework;

namespace Labyrinthian.Components
{
	class TransformComponent : IComponent
	{
		public Matrix Transform { get; set; } = Matrix.Identity;

		public Entity Entity { get; set; }

		public void Initialize()
		{

		}

		public void Update(GameTime gameTime)
		{

		}
	}
}
