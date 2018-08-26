using System;

namespace CHMonoTools.ECS
{
	public class ComponentEventArgs : EventArgs
	{
		public IComponent Component { get; set; }

		public ComponentEventArgs() { }
		public ComponentEventArgs(IComponent component)
		{
			this.Component = component;
		}
	}
}
