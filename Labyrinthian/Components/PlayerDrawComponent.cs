using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Labyrinthian.Components
{
	class PlayerDrawComponent : IDrawableComponent
	{
		public Entity Entity { get; set; }

		public Texture2D Texture;
		private SpriteAnimator spriteAnimator;

		private PositionComponent entityPosition;
		//private PlayerInputComponent entityInput;

		public PlayerDrawComponent()
		{
			this.Texture = LabyrinthianGame.Game.Content.Load<Texture2D>(@"Textures/PlayerFemale");
			this.spriteAnimator = new SpriteAnimator(AnimationRectangles) { TimeBetweenAnimationInMs = 200};
		}

		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			if (this.entityPosition == null || this.entityPosition.Entity != this.Entity)
			{
				this.entityPosition = this.Entity?.GetComponent<PositionComponent>();
				if (this.entityPosition == null)
				{
					return;
				}
			}

			//if (this.entityInput == null || this.entityInput.Entity != this.Entity)
			//{
			//	this.entityInput = this.Entity?.GetComponent<PlayerInputComponent>();
			//	if (this.entityInput == null)
			//	{
			//		return;
			//	}
			//}

			string animationName = "";
			if (this.entityPosition.LastTickPosition != this.entityPosition.Position)
			{
				switch (this.entityPosition.Orientation)
				{
					case Orientation.Up:
						animationName = "walk_up";
						break;
					case Orientation.Left:
						animationName = "walk_left";
						break;
					case Orientation.Down:
						animationName = "walk_down";
						break;
					case Orientation.Right:
						animationName = "walk_right";
						break;
				}
			}
			else
			{
				switch (this.entityPosition.Orientation)
				{
					case Orientation.Up:
						animationName = "up";
						break;
					case Orientation.Left:
						animationName = "left";
						break;
					case Orientation.Down:
						animationName = "down";
						break;
					case Orientation.Right:
						animationName = "right";
						break;
				}
			}
			Rectangle sourceRectangle = this.spriteAnimator.GetSourceRectangle(animationName);
			spriteBatch.Draw(this.Texture, this.entityPosition.Position - sourceRectangle.Size.ToVector2() / 2, sourceRectangle, Color.White);
		}

		public void Initialize()
		{

		}

		public void Update(GameTime gameTime)
		{
			this.spriteAnimator.Update(gameTime);
		}

		public static Dictionary<string, Rectangle[]> AnimationRectangles = new Dictionary<string, Rectangle[]>
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
	}
}
