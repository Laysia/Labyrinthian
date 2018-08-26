namespace Microsoft.Xna.Framework.Input
{
	public static class MouseStateExtensions
	{
		/// <summary>
		/// Transforms the position of the mouse according to the inverted transformation matrix
		/// </summary>
		/// <param name="MouseState"></param>
		/// <param name="InvertedMatrix">The inverted transformation matrix that should be applied to the mouse position</param>
		/// <returns></returns>
		public static Vector2 TransformedPosition(this MouseState MouseState, Matrix InvertedMatrix)
		{
			return Vector2.Transform(MouseState.Position.ToVector2(), InvertedMatrix);
		}
	}
}
