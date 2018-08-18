using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace Labyrinthian
{
	public static class ProgrammerArt
	{
		public static Texture2D GradientBlackCircle { get; private set; }

		public static Texture2D BlackPixel { get; private set; }

		static ProgrammerArt()
		{
			GradientBlackCircle = createGreadientBlackCircle(100);
			BlackPixel = createBlackPixel();
		}

		private static Texture2D createBlackPixel()
		{
			Texture2D texture = new Texture2D(LabyrinthianGame.Game.GraphicsDevice, 1, 1);
			texture.SetData(new[] { new Color(0, 0, 0, 255) });
			return texture;
		}

		private static Texture2D createGreadientBlackCircle(int radius)
		{
			Texture2D texture = new Texture2D(LabyrinthianGame.Game.GraphicsDevice, 2 * radius, 2 * radius);

			Color[] colorData = new Color[2 * radius * 2 * radius];


			for (int x = 0; x < 2 * radius; ++x)
			{
				for (int y = 0; y < 2 * radius; ++y)
				{
					int index = x * 2 * radius + y;

					double distx = radius - x;
					double disty = radius - y;

					int alpha;
					double distSquared = distx * distx + disty * disty;
					if (distSquared >= radius * radius)
					{
						alpha = 255;
					}
					else
					{
						alpha = (int)(255 * Math.Pow(Math.Sqrt(distSquared) / radius, 1.0d));
					}
					colorData[index] = new Color(0, 0, 0, alpha);
					
				}
			}
			texture.SetData(colorData);
			//using (var stream = new FileStream(@"C:\Temp\test.png", FileMode.OpenOrCreate, FileAccess.ReadWrite))
			//{
			//	texture.SaveAsPng(stream, texture.Width, texture.Height);
			//}
			return texture;
		}
	}
}
