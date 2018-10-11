using System;
using System.Collections.Generic;
using CHMonoTools.ECS;
using Xunit;

namespace CHMonoToolsTests.ECS
{
	public class EntityTests
	{
		[Fact]
		public void AddComponentEventTriggersTest()
		{
			bool triggered = false;
			var entity = Entity.CreateNew();
			entity.ComponentAdded += (sender, e) => { triggered = true; };
			entity.Add(new TestComponent());
			Assert.True(triggered);
		}

		[Fact]
		public void RemoveComponentEventTriggersTest()
		{
			bool triggered = false;
			var entity = Entity.CreateNew();
			var component = new TestComponent();
			entity.Remove(component);
			Assert.False(triggered);

			entity.ComponentRemoved += (sender, e) => { triggered = true; };
			entity.Add(component);
			entity.Remove(component);
			Assert.True(triggered);
		}

		[Fact]
		public void GetComponentTest()
		{
			var entity = Entity.CreateNew();
			var component = new TestComponent();
			var compNull = entity.GetComponent<TestComponent>();
			Assert.Null(compNull);

			entity.Add(component);
			var comp2 = entity.GetComponent<TestComponent>();
			Assert.Equal(component, comp2);
		}

		[Fact]
		public void GetChildComponentTest()
		{
			var entity = Entity.CreateNew();
			var parentComp = new TestComponent();
			var childComp = new TestChildComponent();

			entity.Add(childComp);
			var returnVal = entity.GetComponent<TestComponent>();

			Assert.NotNull(returnVal);
		}

		[Fact]
		public void AutoEntitySetterTest()
		{
			var entity = Entity.CreateNew();
			var comp = new TestComponent();
			Assert.Null(comp.Entity);

			entity.Add(comp);

			Assert.Equal(comp.Entity, entity);

			entity.Remove(comp);

			Assert.Null(comp.Entity);
		}
	}
}
