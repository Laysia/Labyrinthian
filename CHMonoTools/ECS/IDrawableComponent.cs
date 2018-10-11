using Microsoft.Xna.Framework.Graphics;

namespace CHMonoTools.ECS
{
	public interface IDrawableComponent : IComponent, IDrawable
	{
		bool Visible { get; set; }
	}
}
