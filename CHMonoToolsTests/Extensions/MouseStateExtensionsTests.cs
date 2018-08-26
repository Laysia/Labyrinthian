using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Xunit;

namespace CHMonoToolsTests.Extensions
{
	public class MouseStateExtensionsTests
    {
		public static IEnumerable<object[]> MouseStateTransformationTestData => new List<object[]>
			{
				new object[] { new MouseState(10, 10, 0, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released), Matrix.CreateTranslation(10, 10,0), new Vector2(20,20) },
				new object[] { new MouseState(0, 0, 0, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released), Matrix.CreateTranslation(0, 0,0), new Vector2(0,0) },
				new object[] { new MouseState(-10, -10, 0, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released), Matrix.CreateTranslation(10, 10,0), new Vector2(0,0) },
				new object[] { new MouseState(100, 100, 0, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released), Matrix.CreateTranslation(10, 10,0), new Vector2(110,110) },
				new object[] { new MouseState(0, 0, 0, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released), Matrix.CreateTranslation(0, 0,10), new Vector2(0,0) },
				new object[] { new MouseState(10, 10, 0, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released), Matrix.CreateTranslation(0, 0,10), new Vector2(10,10) }
			};

		[Theory]
		[MemberData(nameof(MouseStateTransformationTestData))]
		public void TransformedPosition_Tests(MouseState mouseState, Matrix invertedMatrix, Vector2 result)
		{
			Assert.Equal<Vector2>(mouseState.TransformedPosition(invertedMatrix), result);
		}

	}
}
