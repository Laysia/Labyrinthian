using CHMonoTools.ECS;

namespace Labyrinthian
{
	class LevelCompleterComponent : Component
	{
		public PositionComponent EntityPosition { get; set; }
		public PhysicsHitboxComponent EntityHitbox { get; set; }

		public LevelCompleterComponent()
		{
		}

		protected override void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is PositionComponent p)
			{
				this.EntityPosition = p;
			}
			else if (e.Component is PhysicsHitboxComponent h)
			{
				this.EntityHitbox = h;
			}
			base.Entity_ComponentAdded(sender, e);
		}

		protected override void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e)
		{
			if (e.Component == this.EntityPosition)
			{
				this.EntityPosition = null;
			}
			else if (e.Component == this.EntityHitbox)
			{
				this.EntityHitbox = null;
			}
			base.Entity_ComponentRemoved(sender, e);
		}
	}
}
