using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Labyrinthian
{
	class PhysicsGameSystem : GameSystem
	{
		private HashSet<PhysicsHitboxComponent> staticBodies = new HashSet<PhysicsHitboxComponent>();
		private HashSet<PhysicsHitboxComponent> movingBodies = new HashSet<PhysicsHitboxComponent>();
		//private HashSet<PhysicsHitboxComponent> allBodies = new HashSet<PhysicsHitboxComponent>();

		public PhysicsGameSystem(EntityContainer entityContainer) : base(entityContainer)
		{
		}

		public override void Update(GameTime gameTime)
		{
			foreach (var mover in this.movingBodies)
			{
				if (mover.EntityPosition == null)
				{
					continue;
				}
				Rectangle currentHitbox = mover.Hitbox;
				if (currentHitbox.Size == Point.Zero)
				{
					continue;
				}
				TransformComponent transformComponent = mover.Entity.GetComponent<TransformComponent>();
				Vector2 moveDirection = transformComponent.Transform.Translation.ToVector2();
				Rectangle nextHitbox = currentHitbox.Move(moveDirection);

				foreach (var staticBody in this.staticBodies)
				{
					Rectangle otherCurrentHitbox = staticBody.Hitbox;

					if (otherCurrentHitbox.Size == Point.Zero)
					{
						continue;
					}
					var intersection = Rectangle.Intersect(nextHitbox, otherCurrentHitbox);

					if (!intersection.IsEmpty)
					{
						if (currentHitbox.IsBelow(otherCurrentHitbox))
						{
							// move down
							transformComponent.Transform = transformComponent.Transform * Matrix.CreateTranslation(0, intersection.Height + 1, 0);
						}
						else if (currentHitbox.IsAbove(otherCurrentHitbox))
						{
							// move up
							transformComponent.Transform = transformComponent.Transform * Matrix.CreateTranslation(0, -intersection.Height - 1, 0);
						}

						if (currentHitbox.IsLeftOf(otherCurrentHitbox))
						{
							// move left
							transformComponent.Transform = transformComponent.Transform * Matrix.CreateTranslation(-intersection.Width - 1, 0, 0);
						}
						else if (currentHitbox.IsRightOf(otherCurrentHitbox))
						{
							// move right
							transformComponent.Transform = transformComponent.Transform * Matrix.CreateTranslation(intersection.Width + 1, 0, 0);
						}

						moveDirection = transformComponent.Transform.Translation.ToVector2();
						nextHitbox = currentHitbox.Move(moveDirection);
					}
				}
				foreach (var movingBody in this.movingBodies)
				{
					if (mover == movingBody)
					{
						continue;
					}
					Rectangle otherHitbox = movingBody.Hitbox;
					if (otherHitbox.Size == Point.Zero)
					{
						continue;
					}
					TransformComponent otherTransformComponent = mover.Entity.GetComponent<TransformComponent>();
					Matrix otherMoveDirection = otherTransformComponent.Transform;
					Rectangle nextOtherHitbox = movingBody.Hitbox.Move(otherMoveDirection.Translation.ToVector2());

					var intersection = Rectangle.Intersect(nextHitbox, nextOtherHitbox);
					if (intersection.Size != Point.Zero)
					{
						// make em meet in the middle
						// TODO

					}
				}
			}
		}

		protected override void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is PhysicsHitboxComponent p)
			{
				if (sender.GetComponent<TransformComponent>() != null)
				{
					this.movingBodies.Add(p);
				}
				else
				{
					this.staticBodies.Add(p);
				}
				//this.allBodies.Add(p);
			}
			else if (e.Component is TransformComponent t)
			{
				var hitbox = sender.GetComponent<PhysicsHitboxComponent>();
				if (hitbox != null && this.staticBodies.Contains(hitbox))
				{
					this.staticBodies.Remove(hitbox);
					this.movingBodies.Add(hitbox);
				}
			}
		}

		protected override void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is PhysicsHitboxComponent p)
			{
				this.staticBodies.Remove(p);
				this.movingBodies.Remove(p);
				//this.allBodies.Remove(p);
			}
			else if (e.Component is TransformComponent t)
			{
				var hitbox = sender.GetComponent<PhysicsHitboxComponent>();
				if (hitbox != null && this.movingBodies.Contains(hitbox))
				{
					this.movingBodies.Remove(hitbox);
					this.staticBodies.Add(hitbox);
				}
			}
		}
	}
}
