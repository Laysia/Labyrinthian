using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinthian
{
	class TorchSpriteComponent : AnimatedSpriteComponent
	{
		public TorchSpriteComponent(Texture2D texture, SpriteAnimator spriteAnimator) : base(texture, spriteAnimator)
		{
		}

		protected override Rectangle GetSourceRectangle()
		{
			return this.SpriteAnimator.GetSourceRectangle("visible");
		}
	}
}
