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
			this.spriteAnimator = new SpriteAnimator(SpriteAnimator.PlayerRectangles) { TimeBetweenAnimationInMs = 600 / this.speed };
		}

		public void Update(GameTime gameTime)
		{
			this.spriteAnimator.Update(gameTime);
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
			spriteBatch.Draw(this.Texture, this.Position - this.spriteAnimator.GetSourceRectangle(animationName).Size.ToVector2() / 2, this.spriteAnimator.GetSourceRectangle(animationName), Color.White);
		}
	}
}
