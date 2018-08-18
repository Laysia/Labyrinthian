using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CHMonoTools;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using System;

namespace Labyrinthian
{
	public class Player : IGameObject, IPosition, IHitbox
	{
		public Vector2 Position { get; set; }
		public float speed = 2.0f;
		public Texture2D Texture;
		private SpriteAnimator spriteAnimator;
		public int orientation = 1;
		private bool isWalking = false;
		public Rectangle Hitbox
		{
			get
			{
				return new Rectangle((int)this.Position.X - 12, (int)this.Position.Y - 12, 24, 24);
			}
		}


		public Player()
		{
			this.Position = LabyrinthianGame.Game.GraphicsDevice.Viewport.Bounds.Center.ToVector2();
			this.Texture = LabyrinthianGame.Game.Content.Load<Texture2D>(@"Textures/PlayerFemale");
		}

		public void Initialize()
		{
			this.spriteAnimator = new SpriteAnimator(getAnimationRectangles()) { TimeBetweenAnimationInMs = 600 / this.speed };
		}

		public void Update(GameTime gameTime)
		{
			float percentage = (float)gameTime.ElapsedGameTime.TotalSeconds * 60.0f;
			this.isWalking = true;
			var state = Keyboard.GetState();
			if (state.IsKeyDown(Keys.W))
			{
				this.orientation = 1;
				this.Position += new Vector2(0, -1) * this.speed * percentage;
			}
			else if (state.IsKeyDown(Keys.A))
			{
				this.orientation = 2;
				this.Position += new Vector2(-1, 0) * this.speed * percentage;
			}
			else if (state.IsKeyDown(Keys.S))
			{
				this.orientation = 3;
				this.Position += new Vector2(0, 1) * this.speed * percentage;
			}
			else if (state.IsKeyDown(Keys.D))
			{
				this.orientation = 4;
				this.Position += new Vector2(1, 0) * this.speed * percentage;
			}
			else
			{
				this.isWalking = false;
			}
		}

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			string animationName = "";
			if (this.isWalking)
			{
				switch (this.orientation)
				{
					case 1:
						animationName = "walk_up";
						break;
					case 2:
						animationName = "walk_left";
						break;
					case 3:
						animationName = "walk_down";
						break;
					case 4:
						animationName = "walk_right";
						break;
				}
			}
			else
			{
				switch (this.orientation)
				{
					case 1:
						animationName = "up";
						break;
					case 2:
						animationName = "left";
						break;
					case 3:
						animationName = "down";
						break;
					case 4:
						animationName = "right";
						break;
				}
			}
			spriteBatch.Draw(this.Texture, this.Position - this.spriteAnimator.GetSourceRectangle(gameTime, animationName).Size.ToVector2() / 2, this.spriteAnimator.GetSourceRectangle(gameTime, animationName), Color.White);
		}

		private Dictionary<string, Rectangle[]> getAnimationRectangles()
		{
			Dictionary<string, Rectangle[]> animation = new Dictionary<string, Rectangle[]>
			{
				{ "up", new Rectangle[] { new Rectangle(0, 0, 32, 32) } },
				{ "left", new Rectangle[] { new Rectangle(0, 32, 32, 32) } },
				{ "down", new Rectangle[] { new Rectangle(0, 64, 32, 32) } },
				{ "right", new Rectangle[] { new Rectangle(0, 96, 32, 32) } },
				{ "walk_up", new Rectangle[] { new Rectangle(32, 0, 32, 32), new Rectangle(0, 0, 32, 32), new Rectangle(64, 0, 32, 32), new Rectangle(0, 0, 32, 32) } },
				{ "walk_left", new Rectangle[] { new Rectangle(32, 32, 32, 32), new Rectangle(0, 32, 32, 32), new Rectangle(64, 32, 32, 32), new Rectangle(0, 32, 32, 32) } },
				{ "walk_down", new Rectangle[] { new Rectangle(32, 64, 32, 32), new Rectangle(0, 64, 32, 32), new Rectangle(64, 64, 32, 32), new Rectangle(0, 64, 32, 32) } },
				{ "walk_right", new Rectangle[] { new Rectangle(32, 96, 32, 32), new Rectangle(0, 96, 32, 32), new Rectangle(64, 96, 32, 32), new Rectangle(0, 96, 32, 32) } }
			};
			return animation;
		}
	}
}
