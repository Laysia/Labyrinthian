using System;

namespace CHMonoTools
{
	public struct Point3D : IEquatable<Point3D>
    {
		public int X { get; set; }
		public int Y { get; set; }
		public int Z { get; set; }

		public Point3D(int x, int y, int z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public bool Equals(Point3D other)
		{
			return other.X == this.X && other.Y == this.Y && other.Z == this.Z;
		}


		public override bool Equals(object obj)
		{
			return obj is Point3D point && Equals(point);
		}

		public override int GetHashCode()
		{
			return (new ValueTuple<int, int, int>(this.X, this.Y, this.Z)).GetHashCode();
		}

		public override string ToString()
		{
			return $"({this.X},{this.Y},{this.Z})";
		}

		public static Point3D operator +(Point3D value1, Point3D value2)
		{
			return new Point3D(value1.X + value2.X, value1.Y + value2.Y, value1.Z + value2.Z);
		}

		public static Point3D operator -(Point3D value1, Point3D value2)
		{
			return new Point3D(value1.X - value2.X, value1.Y - value2.Y, value1.Z - value2.Z);
		}

		public static Point3D operator *(Point3D value1, Point3D value2)
		{
			return new Point3D(value1.X * value2.X, value1.Y * value2.Y, value1.Z * value2.Z);
		}

		public static Point3D operator *(Point3D value1, int value2)
		{
			return new Point3D(value1.X * value2, value1.Y * value2, value1.Z * value2);
		}
		public static Point3D operator *(Point3D value1, float value2)
		{
			return new Point3D((int)(value1.X * value2), (int)(value1.Y * value2), (int)(value1.Z * value2));
		}
		public static Point3D operator *(Point3D value1, double value2)
		{
			return new Point3D((int)(value1.X * value2), (int)(value1.Y * value2), (int)(value1.Z * value2));
		}

		public static Point3D operator /(Point3D source, Point3D divisor)
		{
			return new Point3D(source.X / divisor.X, source.Y / divisor.Y, source.Z / divisor.Z);
		}
		public static Point3D operator /(Point3D source, int divisor)
		{
			return new Point3D((int)(source.X / divisor), (int)(source.Y / divisor), (int)(source.Z / divisor));
		}
		public static Point3D operator /(Point3D source, float divisor)
		{
			return new Point3D((int)(source.X / divisor), (int)(source.Y / divisor), (int)(source.Z / divisor));
		}
		public static Point3D operator /(Point3D source, double divisor)
		{
			return new Point3D((int)(source.X / divisor), (int)(source.Y / divisor), (int)(source.Z / divisor));
		}
		public static bool operator ==(Point3D a, Point3D b)
		{
			return a.Equals(b);
		}
		public static bool operator !=(Point3D a, Point3D b)
		{
			return !(a == b);
		}
	}
}
