using CHMonoTools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Labyrinthian
{
	public class Camera : CHMonoTools.IUpdateable, ITransformer
	{
		private void updateCamera()
		{
			//this.transformationMatrix =
			//	Matrix.CreateTranslation(new Vector3(-this.Position.Position.X, -this.Position.Position.Y, 0)) *
			//	Matrix.CreateTranslation(new Vector3(x: this._viewportWidth / this.Zoom * 0.5f, y: this._viewportHeight / this.Zoom * 0.5f, z: 0)) *
			//	Matrix.CreateScale(new Vector3(this.Zoom, this.Zoom, 1));

			this.world = Matrix.CreateTranslation(this.Position.Position.ToVector3() * -1);
			this.view =
				Matrix.CreateTranslation(new Vector3(x: this._viewportWidth / this.Zoom * 0.5f, y: this._viewportHeight / this.Zoom * 0.5f, z: 0))
				* Matrix.CreateScale(new Vector3(this.Zoom, this.Zoom, 1));
			this.transformationMatrix = this.world * this.view;
			this.PrevPosition = this.Position.Position;
		}

		private Matrix transformationMatrix;
		public Matrix TransformationMatrix
		{
			get
			{
				if (this.Position.Position != this.PrevPosition)
				{
					updateCamera();
				}
				return this.transformationMatrix;
			}
		}

		private Matrix world;
		public Matrix World
		{
			get
			{
				if (this.Position.Position != this.PrevPosition)
				{
					updateCamera();
				}
				return this.world;
			}
		}

		private Matrix view;
		public Matrix View
		{
			get
			{
				if (this.Position.Position != this.PrevPosition)
				{
					updateCamera();
				}
				return this.view;
			}
		}

		public const float SCROLLSPEED = 9.0f;

		private const float zoomUpperLimit = 1.5f;
		private const float zoomLowerLimit = .5f;

		private float _zoom;
		private int _viewportWidth;
		private int _viewportHeight;

		private Vector2 PrevPosition { get; set; } = Vector2.Zero;
		public IPosition Position { get; set; }

		public Camera(Viewport viewport, float initialZoom, IPosition attachedTo)
		{
			this.Position = attachedTo;
			this.Zoom = initialZoom;
			this._viewportWidth = viewport.Width;
			this._viewportHeight = viewport.Height;
			updateCamera();
		}

		public void Update(GameTime gameTime)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.T))
			{
				this.Zoom += 0.001f * (gameTime.ElapsedGameTime.Milliseconds / 16.6666f);
			}
			if (Keyboard.GetState().IsKeyDown(Keys.G))
			{
				this.Zoom -= 0.001f * (gameTime.ElapsedGameTime.Milliseconds / 16.6666f);
			}
		}

		#region Properties

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

		#endregion

		public void SetViewport(Viewport viewport)
		{
			this._viewportHeight = viewport.Height;
			this._viewportWidth = viewport.Width;
			updateCamera();
		}
	}
}
