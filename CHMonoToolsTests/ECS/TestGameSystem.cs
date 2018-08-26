using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CHMonoToolsTests.ECS
{
	class TestGameSystem : GameSystem
	{
		public int componentsAdded = 0;
		public int componentsRemoved = 0;

		public TestGameSystem(EntityContainer entityContainer) : base(entityContainer)
		{
		}

		public override void Update(GameTime gameTime)
		{
		}

		protected override void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		{
			this.componentsAdded++;
		}

		protected override void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e)
		{
			this.componentsRemoved++;
		}
	}
}
