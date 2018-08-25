using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinthian
{
	abstract class DrawableGameSystem : GameSystem, CHMonoTools.IDrawable
	{
		protected CameraComponent CameraComponent { get; set; }


		public DrawableGameSystem(EntityContainer entityContainer) : base(entityContainer)
		{
		}

		public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

		protected override void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is CameraComponent cComp)
			{
				this.CameraComponent = cComp;
			}
		}

		protected override void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is CameraComponent cComp)
			{
				this.CameraComponent = null;
			}
		}
	}
}
