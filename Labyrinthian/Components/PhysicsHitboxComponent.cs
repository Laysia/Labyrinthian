using CHMonoTools.ECS;
using Microsoft.Xna.Framework;

namespace Labyrinthian.Components
{
	class PhysicsHitboxComponent : IComponent
	{
		public Entity Entity { get; set; }
		private PositionComponent entityPosition;

		public Rectangle Hitbox { get; set; }

		public void Initialize()
		{
		}

		public void Update(GameTime gameTime)
		{
			if (this.entityPosition == null || this.entityPosition.Entity != this.Entity)
			{
				this.entityPosition = this.Entity?.GetComponent<PositionComponent>();
				if (this.entityPosition == null)
				{
					return;
				}
			}

			if (this.entityPosition.Position != this.entityPosition.LastTickPosition)
			{
				this.Hitbox = new Rectangle((int)this.entityPosition.Position.X - 12, (int)this.entityPosition.Position.Y - 12, 24, 24);
			}
		}
	}
}
