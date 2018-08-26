using System.Collections.Generic;

namespace CHMonoTools.ECS
{
	public class EntityContainer
	{
		private List<Entity> entities = new List<Entity>();

		public delegate void EntityEventHandler(EntityContainer sender, EntityEventArgs e);
		public event EntityEventHandler EntityAdded;
		public event EntityEventHandler EntityRemoved;

		public void Add(Entity entity)
		{
			this.entities.Add(entity);
			this.EntityAdded?.Invoke(this, new EntityEventArgs(entity));
		}

		public bool Remove(Entity entity)
		{
			if (this.entities.Remove(entity))
			{
				this.EntityRemoved?.Invoke(this, new EntityEventArgs(entity));
				return true;
			}
			return false;
		}

		public IReadOnlyList<Entity> GetEntities()
		{
			return this.entities;
		}
	}
}
