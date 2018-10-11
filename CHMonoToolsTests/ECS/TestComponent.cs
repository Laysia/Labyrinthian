using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CHMonoToolsTests.ECS
{
	class TestComponent : IComponent
	{
		public Entity Entity { get; set; }
		public bool Enabled { get; set; } = true;

		public void Initialize()
		{
		}

		public void Update(GameTime gameTime)
		{
		}
	}
}
