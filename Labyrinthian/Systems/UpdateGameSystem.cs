using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Labyrinthian
{
	/// <summary>
	/// Ist das überhaupt nötig oder updates in den jeweliigen systemen aufrufen?
	/// </summary>
	class UpdateGameSystem : GameSystem
	{
		List<PositionComponent> positions = new List<PositionComponent>();
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
			foreach (var pcomp in this.positions)
			{
				pcomp.Update(gameTime);
			}
		}

		protected override void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is PositionComponent p)
			{
				this.positions.Add(p);
			}
			else
			{
				this.components.Add(e.Component);
			}
		}

		protected override void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is PositionComponent p)
			{
				this.positions.Remove(p);
			}
			else
			{
				this.components.Remove(e.Component);
			}
		}
	}
}
