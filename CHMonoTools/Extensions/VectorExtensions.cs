namespace Microsoft.Xna.Framework
{
	public static class VectorExtensions
    {
		public static Vector3 ToVector3(this Vector2 v, float z = 0)
		{
			return new Vector3(v.X, v.Y, z);
		}

		public static Vector2 ToVector2(this Vector3 v)
		{
			return new Vector2(v.X, v.Y);
		}
    }
}
