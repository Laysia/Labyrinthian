namespace Microsoft.Xna.Framework
{
	public static class RectangleExtensions
	{
		public static Rectangle Move(this Rectangle rectangle, Vector2 vector)
		{
			return new Rectangle(rectangle.X + (int)vector.X, rectangle.Y + (int)vector.Y, rectangle.Width, rectangle.Height);
		}

		public static Rectangle Move(this Rectangle rectangle, Point point)
		{
			return new Rectangle(rectangle.X + point.X, rectangle.Y + point.Y, rectangle.Width, rectangle.Height);
		}

		public static bool IsTopAboveBottom(this Rectangle rectangle, Rectangle other)
		{
			return rectangle.Top < other.Bottom;
		}

		public static bool IsRightOfLeft(this Rectangle rectangle, Rectangle other)
		{
			return rectangle.Right > other.Left;
		}

		public static bool IsLeftOfRight(this Rectangle rectangle, Rectangle other)
		{
			return rectangle.Left < other.Right;
		}

		public static bool IsBottomBelowTop(this Rectangle rectangle, Rectangle other)
		{
			return rectangle.Bottom > other.Top;
		}
	}
}
