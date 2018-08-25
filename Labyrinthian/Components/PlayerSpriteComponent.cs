using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinthian.Components
{
	class PlayerSpriteComponent : AnimatedSpriteComponent
	{
		public PlayerSpriteComponent(Texture2D texture, SpriteAnimator spriteAnimator) : base(texture, spriteAnimator)
		{
		}

		protected override Rectangle GetSourceRectangle()
		{
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
			Rectangle sourceRectangle = this.SpriteAnimator.GetSourceRectangle(animationName);
			return sourceRectangle;
		}
	}
}
