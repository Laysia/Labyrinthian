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
		 * 3. PositionComponents (To Set LastTTickPosition)
		 * 4. PhysicsSystem
		 * 
		 */

		List<PlayerInputComponent> input = new List<PlayerInputComponent>();
		List<IComponent> components = new List<IComponent>();
		List<ITilePositionComponent> positions = new List<ITilePositionComponent>();

		public UpdateGameSystem(EntityContainer entityContainer) : base(entityContainer)
		{

		}

		public void FirstUpdate(GameTime gameTime)
		{
			foreach (var input in this.input)
			{
				input.Update(gameTime);
			}
			foreach(var comp in this.components)
			{
				comp.Update(gameTime);
			}
			foreach (var pcomp in this.positions)
			{
				pcomp.Update(gameTime);
			}
		}

		public override void Update(GameTime gameTime)
		{
		}

		public void Lastupdate(GameTime gameTime)
		{

		}

		protected override void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is ITilePositionComponent p)
			{
				this.positions.Add(p);
			}
			else if (e.Component is PlayerInputComponent i)
			{
				this.input.Add(i);
			}
			else
			{
				this.components.Add((IComponent)e.Component);
			}
		}

		protected override void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is ITilePositionComponent p)
			{
				this.positions.Remove(p);
			}
			else
			{
				this.components.Remove((IComponent)e.Component);
			}
		}
	}
}
