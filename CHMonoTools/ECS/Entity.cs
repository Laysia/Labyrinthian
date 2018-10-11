using System;
using System.Collections.Generic;
using System.Linq;

namespace CHMonoTools.ECS
{
	public class Entity : IDisposable
	{
		private List<IComponent> components = new List<IComponent>();

		public delegate void ComponentEventHandler(Entity sender, ComponentEventArgs e);
		public delegate void EntityDisposedHandler(Entity sender);
		public event ComponentEventHandler ComponentAdded;
		public event ComponentEventHandler ComponentRemoved;
		public event EntityDisposedHandler Disposed;

		protected Entity() { }

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


		public static Entity CreateNew()
		{
			return new Entity();
		}

		public void Dispose()
		{
			for (int i = this.components.Count - 1; i >= 0; --i)
			{
				var comp = this.components[i];
				this.components.RemoveAt(i);
				this.ComponentRemoved?.Invoke(this, new ComponentEventArgs(comp));
			}
			this.Disposed?.Invoke(this);
		}
	}
}
