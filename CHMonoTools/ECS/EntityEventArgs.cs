using System;
using System.Collections.Generic;
using System.Text;

namespace CHMonoTools.ECS
{
	public class EntityEventArgs : EventArgs
	{
		public Entity Entity { get; set; }

		public EntityEventArgs() { }
		public EntityEventArgs(Entity Entity)
		{
			this.Entity = Entity;
		}
	}
}
