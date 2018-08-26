using Microsoft.Xna.Framework;

namespace CHMonoTools.ECS
{
	public abstract class Component : IComponent
	{
		private Entity entity;
		public Entity Entity
		{
			get
			{
				return this.entity;
			}
			set
			{
				if (this.entity != null)
				{
					this.entity.ComponentAdded -= Entity_ComponentAdded;
					this.entity.ComponentRemoved -= Entity_ComponentRemoved;
					foreach (var comp in this.entity.GetComponents())
					{
						Entity_ComponentRemoved(this.entity, new ComponentEventArgs(comp));
					}
				}
				this.entity = value;
				value.ComponentAdded += Entity_ComponentAdded;
				value.ComponentRemoved += Entity_ComponentRemoved;
				foreach (var comp in value.GetComponents())
				{
					Entity_ComponentAdded(this.entity, new ComponentEventArgs(comp));
				}
			}
		}

		public virtual void Initialize()
		{
		}

		public virtual void Update(GameTime gameTime)
		{
		}

		protected virtual void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		{

		}

		protected virtual void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e)
		{

		}
	}
}
