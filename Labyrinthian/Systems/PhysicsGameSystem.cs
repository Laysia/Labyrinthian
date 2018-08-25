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
		private HashSet<PhysicsHitboxComponent> allBodies = new HashSet<PhysicsHitboxComponent>();
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
				Rectangle srcHitbox = mover.Hitbox;
				if (srcHitbox.Size == Point.Zero)
				{
					continue;
				}
				TransformComponent transformComponent = mover.Entity.GetComponent<TransformComponent>();
				Matrix moveDirection = transformComponent.Transform;
				Rectangle nextSrcHitbox = srcHitbox.Move(moveDirection.Translation.ToVector2());

				foreach (var staticBody in this.staticBodies)
				{
					Rectangle otherHitbox = staticBody.Hitbox;

					if (otherHitbox.Size == Point.Zero)
					{
						continue;
					}

					if (nextSrcHitbox.Intersects(otherHitbox))
					{
						Vector2 diff = Vector2.Zero;
						switch (mover.EntityPosition?.Orientation)
						{
							case Orientation.Up:
								diff = new Vector2(0, otherHitbox.Bottom - nextSrcHitbox.Top);
								break;
							case Orientation.Down:
								diff = new Vector2(0, otherHitbox.Top - nextSrcHitbox.Bottom);
								break;
							case Orientation.Left:
								diff = new Vector2(otherHitbox.Right - nextSrcHitbox.Left, 0);
								break;
							case Orientation.Right:
								diff = new Vector2(otherHitbox.Left - nextSrcHitbox.Right, 0);
								break;
						}
						transformComponent.Transform = transformComponent.Transform *  Matrix.CreateTranslation(diff.ToVector3());
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


					if (nextSrcHitbox.Intersects(nextOtherHitbox))
					{
						// make em meet in the middle
						Vector2 diffSrc = Vector2.Zero;
						switch (mover.EntityPosition?.Orientation)
						{
							case Orientation.Up:
								diffSrc = new Vector2(0, nextOtherHitbox.Bottom - nextSrcHitbox.Top);
								break;
							case Orientation.Down:
								diffSrc = new Vector2(0, nextOtherHitbox.Top - nextSrcHitbox.Bottom);
								break;
							case Orientation.Left:
								diffSrc = new Vector2(nextOtherHitbox.Right - nextSrcHitbox.Left, 0);
								break;
							case Orientation.Right:
								diffSrc = new Vector2(nextOtherHitbox.Left - nextSrcHitbox.Right, 0);
								break;
						}

						Vector2 diffOther = Vector2.Zero;
						switch (movingBody.EntityPosition?.Orientation)
						{
							case Orientation.Up:
								diffOther = new Vector2(0, nextOtherHitbox.Bottom - nextSrcHitbox.Top);
								break;
							case Orientation.Down:
								diffOther = new Vector2(0, nextOtherHitbox.Top - nextSrcHitbox.Bottom);
								break;
							case Orientation.Left:
								diffOther = new Vector2(nextOtherHitbox.Right - nextSrcHitbox.Left, 0);
								break;
							case Orientation.Right:
								diffOther = new Vector2(nextOtherHitbox.Left - nextSrcHitbox.Right, 0);
								break;
						}

						if (Math.Abs(Vector2.Dot(diffSrc, diffOther)) < 0.01)
						{
							// Both move in different dimensions, so we can move them back seperatly to where they meet
							transformComponent.Transform = transformComponent.Transform * Matrix.CreateTranslation(diffSrc.ToVector3());
							otherTransformComponent.Transform = otherTransformComponent.Transform * Matrix.CreateTranslation(diffOther.ToVector3());
						}
						else
						{
							// Both move in the same dimension, so we move them both half the distance they overlap
							transformComponent.Transform = transformComponent.Transform * Matrix.CreateTranslation(diffSrc.ToVector3() / 2);
							otherTransformComponent.Transform = otherTransformComponent.Transform * Matrix.CreateTranslation(diffOther.ToVector3() / 2);
						}
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
				this.allBodies.Add(p);
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
				this.allBodies.Remove(p);
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
