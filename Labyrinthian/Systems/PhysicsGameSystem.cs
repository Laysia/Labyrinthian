using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Labyrinthian
{
	class PhysicsGameSystem : GameSystem
	{
		private HashSet<PhysicsHitboxComponent> movingBodies = new HashSet<PhysicsHitboxComponent>();
		private HashSet<PhysicsHitboxComponent> allBodies = new HashSet<PhysicsHitboxComponent>();

		public PhysicsGameSystem(EntityContainer entityContainer) : base(entityContainer)
		{
		}

		public override void Update(GameTime gameTime)
		{
			foreach (var mover in this.movingBodies)
			{
				if (!mover.CanCollide)
				{
					continue;
				}
				if (mover.EntityPosition == null)
				{
					continue;
				}
				Rectangle currentHitbox = mover.Hitbox;
				if (currentHitbox.Size == Point.Zero)
				{
					continue;
				}
				TransformComponent transformComponent = mover.Transform;
				Vector2 movement = transformComponent.Transform.Translation.ToVector2();
				if (movement == Vector2.Zero)
				{
					continue;
				}
				Rectangle nextHitbox = currentHitbox.Move(movement);

				foreach (var body in this.allBodies)
				{
					if (!body.CanCollide || body == mover)
					{
						continue;
					}

					Rectangle otherCurrentHitbox = body.Hitbox;

					if (otherCurrentHitbox.Size == Point.Zero)
					{
						continue;
					}
					if (body.Transform == null || body.Transform.Transform.Translation == Vector3.Zero)
					{
						// it can't move or is not moving, so it will not change position in case of a collision
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

							movement = transformComponent.Transform.Translation.ToVector2();
							nextHitbox = currentHitbox.Move(movement);
						}
					}
					else
					{
						Vector2 otherMovement = body.Transform.Transform.Translation.ToVector2();
						Rectangle nextOtherHitbox = otherCurrentHitbox.Move(otherMovement);
						var intersection = Rectangle.Intersect(nextHitbox, nextOtherHitbox);

						if (!intersection.IsEmpty)
						{
							float collisionLengthSquared = intersection.Size.ToVector2().LengthSquared();
							float MovementLength = movement.LengthSquared();
							float otherMovementLength = otherMovement.LengthSquared();

							float factor = MovementLength / (MovementLength + otherMovementLength);

							Vector2 displacement = movement * -1 * factor;
							Vector2 otherDisplacement = otherMovement * -1 * (1 - factor);

							transformComponent.Transform = transformComponent.Transform * Matrix.CreateTranslation(displacement.ToVector3());
							body.Transform.Transform = body.Transform.Transform * Matrix.CreateTranslation(otherDisplacement.ToVector3());

							movement = transformComponent.Transform.Translation.ToVector2();
							nextHitbox = currentHitbox.Move(movement);
							otherMovement = body.Transform.Transform.Translation.ToVector2();
							nextOtherHitbox = otherCurrentHitbox.Move(otherMovement);
						}
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
					TransformComponent otherTransformComponent = movingBody.Entity.GetComponent<TransformComponent>();
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
				this.allBodies.Add(p);
			}
			else if (e.Component is TransformComponent t)
			{
				var hitbox = sender.GetComponent<PhysicsHitboxComponent>();
				if (hitbox != null)
				{
					this.movingBodies.Add(hitbox);
				}
			}
		}

		protected override void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is PhysicsHitboxComponent p)
			{
				this.movingBodies.Remove(p);
				this.allBodies.Remove(p);
			}
			else if (e.Component is TransformComponent t)
			{
				var hitbox = sender.GetComponent<PhysicsHitboxComponent>();
				if (hitbox != null && this.movingBodies.Contains(hitbox))
				{
					this.movingBodies.Remove(hitbox);
				}
			}
		}
	}
}
