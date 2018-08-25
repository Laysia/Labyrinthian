using CHMonoTools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinthian
{
	public class Torch : IGameObject, IPosition
	{
		private SpriteAnimator animator = new SpriteAnimator(AnimationRectangles) { TimeBetweenAnimationInMs = 200 };
		public Texture2D Texture;
		public Vector2 Position { get; set; }

		public Torch(Vector2 Position)
		{
			this.Position = Position;
			this.Texture = LabyrinthianGame.Game.Content.Load<Texture2D>(@"Textures/Torch");
		}


		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			this.animator.Update(gameTime);
			Rectangle sourceRectangle = this.animator.GetSourceRectangle("visible");
			spriteBatch.Draw(this.Texture, this.Position - new Vector2(5, 5), sourceRectangle, Color.White);
		}

		public void Initialize()
		{
		}

		public void Update(GameTime gameTime)
		{
		}

		public static Dictionary<string, Rectangle[]> AnimationRectangles = new Dictionary<string, Rectangle[]>
		{
			{ "visible", new Rectangle[] { new Rectangle(0, 0, 10, 23), new Rectangle(10, 0, 10, 23), new Rectangle(20, 0, 10, 23) } },
			{ "invisible", new Rectangle[] { new Rectangle(0, 0, 1, 1) } }
		};
	}
}
