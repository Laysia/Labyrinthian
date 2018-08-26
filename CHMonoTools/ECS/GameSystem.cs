using Microsoft.Xna.Framework;

namespace CHMonoTools.ECS
{
	public abstract class GameSystem : IUpdateable
	{
		public GameSystem(EntityContainer entityContainer)
		{
			entityContainer.EntityAdded += entityContainer_EntityAdded;
			entityContainer.EntityRemoved += entityContainer_EntityRemoved;

			foreach (var entity in entityContainer.GetEntities())
			{
				entity.ComponentAdded += Entity_ComponentAdded;
				entity.ComponentRemoved += Entity_ComponentRemoved;

				foreach (var comp in entity.GetComponents())
				{
					Entity_ComponentAdded(entity, new ComponentEventArgs(comp));
				}
			}
		}

		private void entityContainer_EntityRemoved(EntityContainer sender, EntityEventArgs e)
		{
			e.Entity.ComponentAdded -= Entity_ComponentAdded;
			e.Entity.ComponentRemoved -= Entity_ComponentRemoved;

			foreach (var comp in e.Entity.GetComponents())
			{
				Entity_ComponentRemoved(e.Entity, new ComponentEventArgs(comp));
			}
		}

		private void entityContainer_EntityAdded(EntityContainer sender, EntityEventArgs e)
		{
			e.Entity.ComponentAdded += Entity_ComponentAdded;
			e.Entity.ComponentRemoved += Entity_ComponentRemoved;

			foreach (var comp in e.Entity.GetComponents())
			{
				Entity_ComponentAdded(e.Entity, new ComponentEventArgs(comp));
			}
		}

		protected abstract void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e);

		protected abstract void Entity_ComponentAdded(Entity sender, ComponentEventArgs e);

		public abstract void Update(GameTime gameTime);
	}
}
