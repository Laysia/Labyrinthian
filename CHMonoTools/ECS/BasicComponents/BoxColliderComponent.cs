using Microsoft.Xna.Framework;

namespace CHMonoTools.ECS
{
	public class BoxColliderComponent : Component
	{
		private IPositionComponent positionComponent;
		public IPositionComponent PositionComponent
		{
			get
			{
				if (this.positionComponent != null)
				{
					if (this.positionComponent.Entity != this.Entity)
					{
						this.positionComponent = this.Entity.GetComponent<IPositionComponent>();
					}
				}
				else
				{
					this.positionComponent = this.Entity.GetComponent<IPositionComponent>();
				}
				return this.positionComponent;
			}
		}

		public bool Visible { get; set; }
		public Point Offset { get; set; }
		public Point Size { get; set; }
		public Rectangle Box
		{
			get
			{
				if (this.PositionComponent == null)
				{
					return Rectangle.Empty;
				}
				else
				{
					return new Rectangle(this.PositionComponent.ActualPosition.ToPoint() + this.Offset, this.Size);
				}
			}
		}

		public Rectangle GetBox(Vector2 Position)
		{
			return new Rectangle(Position.ToPoint() + this.Offset, this.Size);
		}

		public BoxColliderComponent(Point offset, Point size)
		{
			this.Offset = offset;
			this.Size = size;
		}
	}
}
