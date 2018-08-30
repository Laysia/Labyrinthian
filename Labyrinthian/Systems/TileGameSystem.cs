using System.Collections.Generic;
using CHMonoTools.ECS;
using Microsoft.Xna.Framework;

namespace Labyrinthian
{
	class TileGameSystem : GameSystem
	{
		public Dictionary<Point, List<TilePositionComponent>> TilePositionComponents = new Dictionary<Point, List<TilePositionComponent>>();

		public TileGameSystem(EntityContainer entityContainer) : base(entityContainer)
		{
		}

		public override void Update(GameTime gameTime)
		{
		}

		private void onTilePositionChanged(TilePositionComponent sender, PointChangedEventArgs e)
		{
			this.TilePositionComponents[e.Previous].Remove(sender);

			if (!this.TilePositionComponents.ContainsKey(e.New))
			{
				this.TilePositionComponents[e.New] = new List<TilePositionComponent>() { sender };
			}
			else
			{
				this.TilePositionComponents[e.New].Add(sender);
			}
		}

		protected override void Entity_ComponentAdded(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is TilePositionComponent p)
			{
				p.TilePositionChanged += onTilePositionChanged;
				if (!this.TilePositionComponents.ContainsKey(p.TilePosition))
				{
					this.TilePositionComponents[p.TilePosition] = new List<TilePositionComponent>() { p };
				}
				else
				{
					this.TilePositionComponents[p.TilePosition].Add(p);
				}
			}
		}

		protected override void Entity_ComponentRemoved(Entity sender, ComponentEventArgs e)
		{
			if (e.Component is TilePositionComponent p)
			{
				p.TilePositionChanged -= onTilePositionChanged;
				this.TilePositionComponents[p.TilePosition].Remove(p);
			}
		}


	}
}
