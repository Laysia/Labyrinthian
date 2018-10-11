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
			entity.Disposed += onEntity_Disposed;
		}

		private void onEntity_Disposed(Entity sender)
		{
			this.Remove(sender);
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

		public void Clear()
		{
			for (int i = this.entities.Count - 1; i >= 0; --i)
			{
				this.Remove(this.entities[i]);
			}
		}

		public IReadOnlyList<Entity> GetEntities()
		{
			return this.entities;
		}
	}
}
