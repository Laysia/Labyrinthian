using CHMonoTools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Labyrinthian
{
	public class PhysicsSystem : CHMonoTools.IUpdateable
	{
		public Player Player { get; set; }
		public IHitbox[,] Tiles { get; set; }

		public void Update(GameTime gameTime)
		{
			int indexX = (int)(this.Player.Position.X / Tile.TILE_WIDTH);
			int indexY = (int)(this.Player.Position.Y / Tile.TILE_HEIGHT);

			if (this.Player.orientation == 1)
			{
				collisionTop(indexX, indexY);
			}
			if (this.Player.orientation == 2)
			{
				collisionLeft(indexX, indexY);
			}
			if (this.Player.orientation == 3)
			{
				collisionBottom(indexX, indexY);
			}
			if (this.Player.orientation == 4)
			{
				collisionRight(indexX, indexY);
			}

		}

		private void collisionBottom(int indexX, int indexY)
		{
			for (int i = -1; i <= 1; ++i)
			{
				IHitbox hitbox = null;
				try
				{
					hitbox = this.Tiles[indexX - i, indexY + 1];
				}
				catch { }

				if (hitbox != null && hitbox.Hitbox.Intersects(this.Player.Hitbox))
				{
					this.Player.Position = new Vector2(this.Player.Position.X, hitbox.Hitbox.Top - this.Player.Hitbox.Height / 2);
					return;
				}
			}
		}

		private void collisionRight(int indexX, int indexY)
		{
			for (int i = -1; i <= 1; ++i)
			{
				IHitbox hitbox = null;
				try
				{
					hitbox = this.Tiles[indexX + 1, indexY + i];
				}
				catch { }

				if (hitbox != null && hitbox.Hitbox.Intersects(this.Player.Hitbox))
				{
					this.Player.Position = new Vector2(hitbox.Hitbox.Left - this.Player.Hitbox.Width / 2, this.Player.Position.Y);
					return;
				}
			}
		}

		private void collisionLeft(int indexX, int indexY)
		{
			for (int i = -1; i <= 1; ++i)
			{
				IHitbox hitbox = null;
				try
				{
					hitbox = this.Tiles[indexX - 1, indexY + i];
				}
				catch { }

				if (hitbox != null && hitbox.Hitbox.Intersects(this.Player.Hitbox))
				{
					this.Player.Position = new Vector2(hitbox.Hitbox.Right + this.Player.Hitbox.Width / 2, this.Player.Position.Y);
					return;
				}
			}
		}

		private void collisionTop(int indexX, int indexY)
		{
			for (int i = -1; i <= 1; ++i)
			{
				IHitbox hitbox = null;
				try
				{
					hitbox = this.Tiles[indexX - i, indexY - 1];
				}
				catch { }

				if (hitbox != null && hitbox.Hitbox.Intersects(this.Player.Hitbox))
				{
					this.Player.Position = new Vector2(this.Player.Position.X, hitbox.Hitbox.Bottom + this.Player.Hitbox.Height / 2);
					return;
				}
			}
		}
	}
}
