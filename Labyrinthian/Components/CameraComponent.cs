using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinthian
{
	class CameraComponent : IComponent
	{
		public Entity Entity { get; set; }

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

		public void Initialize()
		{
		}

		public void Update(GameTime gameTime)
		{
			if (this.EntityPosition == null || this.EntityPosition.Entity != this.Entity)
			{
				this.EntityPosition = this.Entity?.GetComponent<PositionComponent>();
				if (this.EntityPosition == null)
				{
					return;
				}
			}

			if (this.EntityPosition.Position != this.EntityPosition.LastTickPosition)
			{
				updateCamera();
			}
		}

		private void updateCamera()
		{
			//this.transformationMatrix =
			//	Matrix.CreateTranslation(new Vector3(-this.Position.Position.X, -this.Position.Position.Y, 0)) *
			//	Matrix.CreateTranslation(new Vector3(x: this._viewportWidth / this.Zoom * 0.5f, y: this._viewportHeight / this.Zoom * 0.5f, z: 0)) *
			//	Matrix.CreateScale(new Vector3(this.Zoom, this.Zoom, 1));

			this.World = Matrix.CreateTranslation(Vector3.Negate(this.EntityPosition.Position.ToVector3()));
			this.View = Matrix.CreateTranslation(new Vector3(x: this.Viewport.Width / this.Zoom * 0.5f, y: this.Viewport.Height / this.Zoom * 0.5f, z: 0))
				* Matrix.CreateScale(new Vector3(this.Zoom, this.Zoom, 1));
			this.TransformationMatrix = this.World * this.View;
		}
	}
}
