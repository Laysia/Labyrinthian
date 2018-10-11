using System;
using Microsoft.Xna.Framework;

namespace CHMonoTools.ECS
{
	public interface IPositionComponent : IComponent
	{
		Vector2 LastTickPosition { get; }
		Vector2 ActualPosition { get; set; }
		Vector2 PreviousPosition { get; }
		event PositionChangedHandler PositionChanged;
	}

	public delegate void PositionChangedHandler(IPositionComponent sender, PositionEventArgs e);

	public class PositionEventArgs : EventArgs
	{
		public Vector2 PreviousPosition { get; private set; }
		public Vector2 NewPosition { get; private set; }


		public PositionEventArgs(Vector2 PreviousPosition, Vector2 NewPosition)
		{
			this.PreviousPosition = PreviousPosition;
			this.NewPosition = NewPosition;
		}
	}
}