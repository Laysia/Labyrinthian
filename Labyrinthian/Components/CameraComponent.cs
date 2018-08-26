using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinthian
{
	class CameraComponent : Component
	{
		private PositionComponent entityPosition;

		public Viewport Viewport
		{
			get
			{
				return this.viewport;
			}
			set
			{
				this.viewport = value;
				if (this.entityPosition != null)
				{
					updateCamera();
				}
			}
		}

		public Matrix TransformationMatrix { get; private set; }
		public Matrix World { get; private set; }
		public Matrix View { get; private set; }

		public const float SCROLLSPEED = 9.0f;

		private const float zoomUpperLimit = 1.5f;
		private const float zoomLowerLimit = .5f;

		private float _zoom = 1.0f;
		private Viewport viewport;

		public float Zoom
		{
			get { return this._zoom; }
			set
			{
				this._zoom = value;
				if (this._zoom < zoomLowerLimit)
				{
					this._zoom = zoomLowerLimit;
				}

				if (this._zoom > zoomUpperLimit)
				{
					this._zoom = zoomUpperLimit;
				}
				updateCamera();
			}
		}

		private PositionComponent EntityPosition
		{
			get
			{
				return this.entityPosition;
			}

			set
			{
				this.entityPosition = value;
				updateCamera();
			}
		}

		public CameraComponent(Viewport viewport)
		{
			this.Viewport = viewport;
		}

		public override void Update(GameTime gameTime)
		{
			if (this.entityPosition != null && this.EntityPosition.Position != this.EntityPosition.LastTickPosition)
			{
				updateCamera();
			}
			base.Update(gameTime);
		}

		private void updateCamera()
		{
			this.World = Matrix.CreateTranslation(Vector3.Negate(this.EntityPosition.Position.ToVector3()));
			this.View = Matrix.CreateTranslation(new Vector3(x: this.Viewport.Width / this.Zoom * 0.5f, y: this.Viewport.Height / this.Zoom * 0.5f, z: 0))
				* Matrix.CreateScale(new Vector3(this.Zoom, this.Zoom, 1));
			this.TransformationMatrix = this.World * this.View;
		}

		protected override void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is PositionComponent p)
			{
				this.EntityPosition = p;
			}
			base.Entity_ComponentAdded(sender, e);
		}
		protected override void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e)
		{
			if (e.Component == this.entityPosition)
			{
				this.EntityPosition = null;
			}
			base.Entity_ComponentRemoved(sender, e);
		}
	}
}
