using CHMonoTools.ECS;

namespace Labyrinthian
{
	class LevelCompleteEventComponent : Component
	{
		public TilePositionComponent EntityPosition { get; set; }
		public BoxColliderComponent EntityHitbox { get; set; }

		public LevelCompleteEventComponent()
		{
		}

		protected override void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is TilePositionComponent p)
			{
				this.EntityPosition = p;
			}
			else if (e.Component is BoxColliderComponent b)
			{
				this.EntityHitbox = b;
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
