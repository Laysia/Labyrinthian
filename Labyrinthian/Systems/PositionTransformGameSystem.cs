using System;
using System.Collections.Generic;
using CHMonoTools.ECS;
using Microsoft.Xna.Framework;

namespace Labyrinthian
{
	class PositionTransformGameSystem : GameSystem
	{
		public IDictionary<Entity, Tuple<IPositionComponent, IMovementComponent>> MovableEntities = new Dictionary<Entity, Tuple<IPositionComponent, IMovementComponent>>();

		public PositionTransformGameSystem(EntityContainer entityContainer) : base(entityContainer)
		{
		}

		public void ResetAllTransforms()
		{
			foreach (var tuple in this.MovableEntities.Values)
			{
				tuple.Item2.Movement = Vector2.Zero;
			}
		}

		public override void Update(GameTime gameTime)
		{
			foreach (var tuple in this.MovableEntities.Values)
			{
				tuple.Item1.ActualPosition = tuple.Item1.ActualPosition + tuple.Item2.Movement;
			}
		}

		protected override void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		{
			if (!this.MovableEntities.ContainsKey(sender))
			{
				if (e.Component is IPositionComponent p)
				{
					var t = sender.GetComponent<IMovementComponent>();
					if (t != null)
					{
						var tuple = Tuple.Create(p, t);
						this.MovableEntities.Add(sender, tuple);
					}
				}
				else if (e.Component is IMovementComponent t)
				{
					var pos = sender.GetComponent<IPositionComponent>();
					if (pos != null)
					{
						var tuple = Tuple.Create(pos, t);
						this.MovableEntities.Add(sender, tuple);
					}
				}
			}
		}

		protected override void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e)
		{
			if (this.MovableEntities.ContainsKey(sender))
			{
				if (e.Component is IMovementComponent || e.Component is IPositionComponent)
				{
					this.MovableEntities.Remove(sender);
				}
			}
		}
	}
}
