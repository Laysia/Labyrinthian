using System;
using System.Collections.Generic;
using System.Linq;
using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
//using System.Linq;

namespace Labyrinthian
{
	class PhysicsGameSystem : GameSystem
	{
		TileGameSystem tileGameSystem;

		public Dictionary<Entity, Tuple<ITilePositionComponent, IMovementComponent, BoxColliderComponent, RigidBodyComponent>> MovingCollisionObjects = new Dictionary<Entity, Tuple<ITilePositionComponent, IMovementComponent, BoxColliderComponent, RigidBodyComponent>>();

		public PhysicsGameSystem(EntityContainer entityContainer, TileGameSystem tileGameSystem) : base(entityContainer)
		{
			this.tileGameSystem = tileGameSystem;
		}

		public override void Update(GameTime gameTime)
		{
			foreach (var tuple in this.MovingCollisionObjects.Values)
			{
				if (!tuple.Item4.CollisionEnabled || tuple.Item4.BodyType == BodyType.Static)
				{
					continue;
				}
				var surroundingEntites = this.tileGameSystem.GetEntitiesAround(tuple.Item1.TilePosition);
				var surroundingCollisionEntities = surroundingEntites
					.Where(e => e.GetComponent<BoxColliderComponent>() != null 
					&& e.GetComponent<RigidBodyComponent>() != null 
					&& e.GetComponent<ITilePositionComponent>() != null);
				bool didCollide = false;
				foreach(var entity in surroundingCollisionEntities)
				{
					if (entity != tuple.Item1.Entity)
					{
						if (performCollision(tuple, entity))
						{
							didCollide = true;
						}
					}
				}
				if (!didCollide)
				{
					tuple.Item1.ActualPosition += tuple.Item2.Movement;
				}
			}
		}

		private bool performCollision(Tuple<ITilePositionComponent, IMovementComponent, BoxColliderComponent, RigidBodyComponent> tuple, Entity entity)
		{
			var otherRigidBody = entity.GetComponent<RigidBodyComponent>();
			if (!otherRigidBody.CollisionEnabled)
			{
				return false;
			}

			switch (otherRigidBody.BodyType)
			{
				case BodyType.Dynamic:
					return performDynamicCollision(tuple, entity);
				case BodyType.Static:
					return performStaticCollision(tuple, entity);
				default:
					break;
			}
			return false;
		}

		private bool performDynamicCollision(Tuple<ITilePositionComponent, IMovementComponent, BoxColliderComponent, RigidBodyComponent> tuple, Entity entity)
		{
			throw new NotImplementedException();
		}

		private bool performStaticCollision(Tuple<ITilePositionComponent, IMovementComponent, BoxColliderComponent, RigidBodyComponent> tuple, Entity entity)
		{
			var dynamicTilePosComp = tuple.Item1;
			var dynamicMovementComp = tuple.Item2;
			var dynamicBoxComp = tuple.Item3;
			var dynamicRigidBodyComp = tuple.Item4;
			var staticBoxComp = entity.GetComponent<BoxColliderComponent>();


			Vector2 movement = dynamicMovementComp.Movement;
			Rectangle currentDynHitbox = dynamicBoxComp.Box;
			Rectangle nextDynHitbox = currentDynHitbox.Move(movement);
			Rectangle otherHitbox = staticBoxComp.Box;
			Rectangle intersection = Rectangle.Intersect(nextDynHitbox, otherHitbox);
			if (!intersection.IsEmpty)
			{
				if (movement.X == 0)
				{
					// moving only up or down
					dynamicTilePosComp.ActualPosition = new Vector2(dynamicTilePosComp.ActualPosition.X, getNewYPosition(intersection, movement, dynamicBoxComp));
				}
				else if (movement.Y == 0)
				{
					// moving only left or right
					dynamicTilePosComp.ActualPosition = new Vector2(getNewXPosition(intersection, movement, dynamicBoxComp), dynamicTilePosComp.ActualPosition.Y);
				}
				else if (intersection.Width > intersection.Height)
				{
					// fix up/down
					dynamicTilePosComp.ActualPosition = new Vector2(dynamicTilePosComp.ActualPosition.X + dynamicMovementComp.Movement.X, getNewYPosition(intersection, movement, dynamicBoxComp));
				}
				else if (intersection.Width < intersection.Height)
				{
					// fix left/right
					dynamicTilePosComp.ActualPosition = new Vector2(getNewXPosition(intersection, movement, dynamicBoxComp), dynamicTilePosComp.ActualPosition.Y + dynamicMovementComp.Movement.Y);
				}
				else
				{
					if (Math.Abs(movement.X) > Math.Abs(movement.Y))
					{

					}
					else
					{

					}
				}
				return true;
			}
			return false;
		}

		private int getNewXPosition(Rectangle intersection, Vector2 movement, BoxColliderComponent currentHitbox)
		{
			if (movement.X > 0)
			{
				// moving right
				return intersection.Left - currentHitbox.Box.Width - currentHitbox.Offset.X;
			}
			else
			{
				// moving left
				return intersection.Right - currentHitbox.Offset.X;
			}
		}

		private int getNewYPosition(Rectangle intersection, Vector2 movement, BoxColliderComponent currentHitbox)
		{
			if (movement.Y > 0)
			{
				// moving down
				return intersection.Top - currentHitbox.Box.Height - currentHitbox.Offset.Y;
			}
			else
			{
				// moving up
				return intersection.Bottom - currentHitbox.Offset.Y;
			}
		}

		protected override void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		{
			if (!this.MovingCollisionObjects.ContainsKey(sender))
			{
				var pos = sender.GetComponent<ITilePositionComponent>();
				var trans = sender.GetComponent<IMovementComponent>();
				var box = sender.GetComponent<BoxColliderComponent>();
				var body = sender.GetComponent<RigidBodyComponent>();

				if (pos != null && trans != null && box != null && body != null)
				{
					var tuple = Tuple.Create(pos, trans, box, body);
					this.MovingCollisionObjects.Add(sender, tuple);
				}
			}
		}

		protected override void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e)
		{
			if (this.MovingCollisionObjects.ContainsKey(sender))
			{
				if (e.Component is ITilePositionComponent || e.Component is IMovementComponent || e.Component is BoxColliderComponent || e.Component is RigidBodyComponent)
				{
					this.MovingCollisionObjects.Remove(sender);
				}
			}
		}


		//		private TileGameSystem tileGameSystem;

		//		private HashSet<PhysicsHitboxComponent> movingBodies = new HashSet<PhysicsHitboxComponent>();
		//		private HashSet<PhysicsHitboxComponent> allBodies = new HashSet<PhysicsHitboxComponent>();

		//		public PhysicsGameSystem(EntityContainer entityContainer, TileGameSystem tileGameSystem) : base(entityContainer)
		//		{
		//			this.tileGameSystem = tileGameSystem;
		//		}

		//		public override void Update(GameTime gameTime)
		//		{
		//			foreach (var mover in this.movingBodies)
		//			{
		//				if (!mover.CanCollide || mover.EntityPosition == null)
		//				{
		//					continue;
		//				}
		//				Rectangle currentHitbox = mover.Hitbox;
		//				if (currentHitbox.Size == Point.Zero)
		//				{
		//					continue;
		//				}
		//				TransformComponent transformComponent = mover.Transform;
		//				Vector2 movement = transformComponent.Transform.Translation.ToVector2();
		//				if (movement == Vector2.Zero)
		//				{
		//					continue;
		//				}
		//				Rectangle nextHitbox = currentHitbox.Move(movement);

		//				var surroundingEntities = this.tileGameSystem.GetEntitiesAround(mover.EntityPosition.TilePosition, 1);
		//				var bodies = surroundingEntities.Select(x => x.GetComponent<PhysicsHitboxComponent>()).Where(x => x != null).ToList();
		//				foreach (var body in bodies)
		//				{
		//					if (body == null || !body.CanCollide || body == mover)
		//					{
		//						continue;
		//					}

		//					Rectangle otherCurrentHitbox = body.Hitbox;

		//					if (otherCurrentHitbox.Size == Point.Zero)
		//					{
		//						continue;
		//					}
		//					if (body.Transform == null || body.Transform.Transform.Translation == Vector3.Zero)
		//					{
		//						// it can't move or is not moving, so it will not change position in case of a collision

		//						if (movement.X != 0)
		//						{
		//							var intersection = Rectangle.Intersect(nextHitbox, otherCurrentHitbox);
		//							if (!intersection.IsEmpty)
		//							{
		//								float backMovementX = 0;
		//								if (movement.X > 0)
		//								{
		//									// originally moving right
		//									backMovementX -= Math.Min(intersection.Width + 3, movement.X);
		//								}
		//								else if (movement.X < 0)
		//								{
		//									// originally moving left
		//									backMovementX += Math.Min(intersection.Width + 3, -movement.X);
		//								}
		//								transformComponent.Transform = transformComponent.Transform * Matrix.CreateTranslation(backMovementX, 0, 0);
		//							}
		//							movement = transformComponent.Transform.Translation.ToVector2();
		//							nextHitbox = currentHitbox.Move(movement);
		//						}

		//						if (movement.Y != 0)
		//						{
		//							var intersection = Rectangle.Intersect(nextHitbox, otherCurrentHitbox);
		//							if (!intersection.IsEmpty)
		//							{
		//								float backMovementY = 0;
		//								if (movement.Y > 0)
		//								{
		//									// originally moving down
		//									backMovementY -= Math.Min(intersection.Height + 3, movement.Y);
		//								}
		//								else if (movement.Y < 0)
		//								{
		//									// originally moving up
		//									backMovementY += Math.Min(intersection.Height + 3, -movement.Y);
		//								}
		//								transformComponent.Transform = transformComponent.Transform * Matrix.CreateTranslation(0, backMovementY, 0);
		//							}
		//							movement = transformComponent.Transform.Translation.ToVector2();
		//							nextHitbox = currentHitbox.Move(movement);
		//						}
		//					}
		//					else
		//					{
		//						Vector2 otherMovement = body.Transform.Transform.Translation.ToVector2();
		//						Rectangle nextOtherHitbox = otherCurrentHitbox.Move(otherMovement);
		//						var intersection = Rectangle.Intersect(nextHitbox, nextOtherHitbox);

		//						if (!intersection.IsEmpty)
		//						{
		//							float collisionLengthSquared = intersection.Size.ToVector2().LengthSquared();
		//							float MovementLength = movement.LengthSquared();
		//							float otherMovementLength = otherMovement.LengthSquared();

		//							float factor = MovementLength / (MovementLength + otherMovementLength);

		//							Vector2 displacement = movement * -1 * factor;
		//							Vector2 otherDisplacement = otherMovement * -1 * (1 - factor);

		//							transformComponent.Transform = transformComponent.Transform * Matrix.CreateTranslation(displacement.ToVector3());
		//							body.Transform.Transform = body.Transform.Transform * Matrix.CreateTranslation(otherDisplacement.ToVector3());

		//							movement = transformComponent.Transform.Translation.ToVector2();
		//							nextHitbox = currentHitbox.Move(movement);
		//							otherMovement = body.Transform.Transform.Translation.ToVector2();
		//							nextOtherHitbox = otherCurrentHitbox.Move(otherMovement);
		//						}
		//					}
		//				}
		//				foreach (var movingBody in this.movingBodies)
		//				{
		//					if (mover == movingBody)
		//					{
		//						continue;
		//					}
		//					Rectangle otherHitbox = movingBody.Hitbox;
		//					if (otherHitbox.Size == Point.Zero)
		//					{
		//						continue;
		//					}
		//					TransformComponent otherTransformComponent = movingBody.Entity.GetComponent<TransformComponent>();
		//					Matrix otherMoveDirection = otherTransformComponent.Transform;
		//					Rectangle nextOtherHitbox = movingBody.Hitbox.Move(otherMoveDirection.Translation.ToVector2());

		//					var intersection = Rectangle.Intersect(nextHitbox, nextOtherHitbox);
		//					if (intersection.Size != Point.Zero)
		//					{
		//						// make em meet in the middle
		//						// TODO

		//					}
		//				}
		//			}

		//			foreach (var mover in this.movingBodies)
		//			{
		//				mover.EntityPosition.Position = Vector2.Transform(mover.EntityPosition.Position, mover.Transform.Transform);
		//				mover.Transform.Transform = Matrix.Identity;
		//			}
		//		}

		//		protected override void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		//		{
		//			if (e.Component is PhysicsHitboxComponent p)
		//			{
		//				if (sender.GetComponent<TransformComponent>() != null)
		//				{
		//					this.movingBodies.Add(p);
		//				}
		//				this.allBodies.Add(p);
		//			}
		//			else if (e.Component is TransformComponent t)
		//			{
		//				var hitbox = sender.GetComponent<PhysicsHitboxComponent>();
		//				if (hitbox != null)
		//				{
		//					this.movingBodies.Add(hitbox);
		//				}
		//			}
		//		}

		//		protected override void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e)
		//		{
		//			if (e.Component is PhysicsHitboxComponent p)
		//			{
		//				this.movingBodies.Remove(p);
		//				this.allBodies.Remove(p);
		//			}
		//			else if (e.Component is TransformComponent t)
		//			{
		//				var hitbox = sender.GetComponent<PhysicsHitboxComponent>();
		//				if (hitbox != null && this.movingBodies.Contains(hitbox))
		//				{
		//					this.movingBodies.Remove(hitbox);
		//				}
		//			}
		//		}

	}
}
