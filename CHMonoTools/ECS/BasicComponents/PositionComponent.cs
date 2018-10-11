using Microsoft.Xna.Framework;

namespace CHMonoTools.ECS
{
	public class PositionComponent : Component, IPositionComponent
	{
		private Vector2 position;

		public event PositionChangedHandler PositionChanged;

		public Vector2 LastTickPosition { get; private set; }
		public Vector2 PreviousPosition { get; private set; }
		public Vector2 ActualPosition
		{
			get
			{
				return this.position;
			}
			set
			{
				if (this.position != value)
				{
					this.PreviousPosition = this.position;
					this.position = value;
					PositionChanged?.Invoke(this, new PositionEventArgs(this.PreviousPosition, value));
				}
			}
		}

		public PositionComponent(Vector2 position)
		{
			this.PreviousPosition = position;
			this.LastTickPosition = position;
			this.ActualPosition = position;
		}


		public override void Update(GameTime gameTime)
		{
			this.LastTickPosition = this.ActualPosition;
			base.Update(gameTime);
		}
	}
}
