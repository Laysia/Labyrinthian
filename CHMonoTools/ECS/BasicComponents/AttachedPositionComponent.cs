using Microsoft.Xna.Framework;

namespace CHMonoTools.ECS
{
	public class AttachedPositionComponent : Component, IPositionComponent
	{
		private IPositionComponent positionComponent;
		public IPositionComponent PositionComponent
		{
			get
			{
				return this.positionComponent;
			}
			set
			{
				if (this.positionComponent == value)
				{
					return;
				}

				if (this.positionComponent != null)
				{
					this.positionComponent.PositionChanged -= positionProvider_PositionChanged;
				}

				this.positionComponent = value ?? new PositionComponent(Vector2.Zero);

				this.positionComponent.PositionChanged += positionProvider_PositionChanged;
			}
		}

		public Vector2 LastTickPosition
		{
			get
			{
				return this.PositionComponent.LastTickPosition;
			}
		}
		public Vector2 ActualPosition
		{
			get
			{
				return this.PositionComponent.ActualPosition;
			}
			set
			{
				return;
			}
		}
		public Vector2 PreviousPosition
		{
			get
			{
				return this.PositionComponent.PreviousPosition;
			}
		}

		public event PositionChangedHandler PositionChanged;

		public AttachedPositionComponent(IPositionComponent positionProvider)
		{
			this.PositionComponent = positionProvider;
		}

		private void positionProvider_PositionChanged(IPositionComponent sender, PositionEventArgs e)
		{
			PositionChanged?.Invoke(sender, e);
		}
	}
}
