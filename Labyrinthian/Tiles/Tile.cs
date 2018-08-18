using CHMonoTools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Labyrinthian
{
	public class Tile : IGameObject
	{
		public static readonly int TILE_WIDTH = 32;
		public static readonly int TILE_HEIGHT = 32;
		private int _X;
		private int _Y;

		public int X
		{
			get
			{
				return this._X;
			}
			set
			{
				if (this._X != value)
				{
					this._X = value;
					this.TileRectangle = new Rectangle(this._X * TILE_WIDTH, this._Y * TILE_HEIGHT, TILE_WIDTH, TILE_HEIGHT);
				}
			}
		}
		public int Y
		{
			get
			{
				return this._Y;
			}
			set
			{
				if (this._Y != value)
				{
					this._Y = value;
					this.TileRectangle = new Rectangle(this._X * TILE_WIDTH, this._Y * TILE_HEIGHT, TILE_WIDTH, TILE_HEIGHT);
				}
			}
		}
		public Rectangle TileRectangle { get; set; }

		public Texture2D Texture { get; set; }

		public Tile(int X, int Y)
		{
			this._X = X;
			this._Y = Y;
			this.TileRectangle = new Rectangle(this.X * TILE_WIDTH, this.Y * TILE_HEIGHT, TILE_WIDTH, TILE_HEIGHT);
			this.Texture = LabyrinthianGame.Game.Content.Load<Texture2D>(@"Textures/GroundSheet");
		}

		public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
		}

		public virtual void Initialize()
		{
		}

		public virtual void Update(GameTime gameTime)
		{
		}
	}
}
