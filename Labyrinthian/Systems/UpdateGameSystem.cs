using CHMonoTools.ECS;
using CHMonoTools;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Labyrinthian.Systems
{
	/// <summary>
	/// Ist das überhaupt nötig oder updates in den jeweliigen systemen aufrufen?
	/// </summary>
	class UpdateGameSystem : GameSystem
	{
		List<IComponent> components = new List<IComponent>();

		public UpdateGameSystem(EntityContainer entityContainer) : base(entityContainer)
		{

		}

		public override void Update(GameTime gameTime)
		{
			foreach(var comp in this.components)
			{
				comp.Update(gameTime);
			}
		}

		protected override void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		{
			this.components.Add(e.Component);
		}

		protected override void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e)
		{
			this.components.Remove(e.Component);
		}
	}
}
