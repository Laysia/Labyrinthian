using System;
using System.Collections.Generic;
using System.Linq;

namespace CHMonoTools.ECS
{
	public class Entity
	{
		private List<IComponent> components = new List<IComponent>();

		public delegate void ComponentEventHandler(Entity sender, ComponentEventArgs e);
		public event ComponentEventHandler ComponentAdded;
		public event ComponentEventHandler ComponentRemoved;

		public void Add(IComponent component)
		{
			if (!this.components.Contains(component))
			{
				this.components.Add(component);
				component.Entity = this;
				this.ComponentAdded?.Invoke(this, new ComponentEventArgs(component));
			}
		}

		public bool Remove(IComponent component)
		{
			if (this.components.Remove(component))
			{
				component.Entity = null;
				this.ComponentRemoved?.Invoke(this, new ComponentEventArgs(component));
				return true;
			}
			return false;
		}

		public bool Remove<T>() where T : IComponent
		{
			var comp = this.components.FirstOrDefault(x => x is T);
			if (comp != null)
			{
				if (this.components.Remove(comp))
				{
					comp.Entity = null;
					this.ComponentRemoved?.Invoke(this, new ComponentEventArgs(comp));
					return true;
				}
			}
			return false;
		}

		public T GetComponent<T>() where T : IComponent
		{
			return (T)this.components.FirstOrDefault(x => x is T);
		}

		public IEnumerable<IComponent> GetComponents()
		{
			return this.components;
		}
	}
}
