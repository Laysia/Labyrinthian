using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Labyrinthian
{
	class UpdateGameSystem : GameSystem
	{
		/*
		 * Update Order:
		 * 1. PlayerInput
		 * 2. Everything else
		 * 3. PhysicsSystem
		 * 4. PositionComponents
		 */

		List<PlayerInputComponent> input = new List<PlayerInputComponent>();
		List<Component> components = new List<Component>();
		List<PositionComponent> positions = new List<PositionComponent>();

		public UpdateGameSystem(EntityContainer entityContainer) : base(entityContainer)
		{

		}

		public void StartUpdating(GameTime gameTime)
		{
			foreach (var input in this.input)
			{
				input.Update(gameTime);
			}
			foreach(var comp in this.components)
			{
				comp.Update(gameTime);
			}
		}

		public override void Update(GameTime gameTime)
		{
		}

		public void EndUpdating(GameTime gameTime)
		{
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
			else if (e.Component is PlayerInputComponent i)
			{
				this.input.Add(i);
			}
			else
			{
				this.components.Add((CHMonoTools.ECS.Component)e.Component);
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
				this.components.Remove((CHMonoTools.ECS.Component)e.Component);
			}
		}
	}
}
