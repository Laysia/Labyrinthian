using System.Collections.Generic;
using CHMonoTools.ECS;
using Microsoft.Xna.Framework;
using System.Linq;

namespace Labyrinthian
{
	class TileGameSystem : GameSystem
	{
		private Dictionary<Point, List<ITilePositionComponent>> tilePositionComponents = new Dictionary<Point, List<ITilePositionComponent>>();

		public TileGameSystem(EntityContainer entityContainer) : base(entityContainer)
		{
		}

		public override void Update(GameTime gameTime)
		{
		}

		public List<Entity> GetEntitiesAround(Point CenterTilePosition, int Range = 1)
		{
			var returnList = new List<Entity>();
			for (int x = CenterTilePosition.X - Range; x <= CenterTilePosition.X + Range; x++)
			{
				for (int y = CenterTilePosition.Y - Range; y <= CenterTilePosition.Y + Range; y++)
				{
					var point = new Point(x, y);
					if (this.tilePositionComponents.ContainsKey(point))
					{
						returnList.AddRange(this.tilePositionComponents[point].Select(c => c.Entity));
					}
				}
			}
			return returnList;
		}

		private void onTilePositionChanged(ITilePositionComponent sender, TilePositionEventArgs e)
		{
			this.tilePositionComponents[e.PreviousPosition].Remove(sender);

			if (!this.tilePositionComponents.ContainsKey(e.NewPosition))
			{
				this.tilePositionComponents[e.NewPosition] = new List<ITilePositionComponent>() { sender };
			}
			else
			{
				this.tilePositionComponents[e.NewPosition].Add(sender);
			}
		}

		protected override void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is ITilePositionComponent p)
			{
				p.TilePositionChanged += onTilePositionChanged;
				if (!this.tilePositionComponents.ContainsKey(p.TilePosition))
				{
					this.tilePositionComponents[p.TilePosition] = new List<ITilePositionComponent>() { p };
				}
				else
				{
					this.tilePositionComponents[p.TilePosition].Add(p);
				}
			}
		}

		protected override void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is ITilePositionComponent p)
			{
				p.TilePositionChanged -= onTilePositionChanged;
				this.tilePositionComponents[p.TilePosition].Remove(p);
			}
		}


	}
}
