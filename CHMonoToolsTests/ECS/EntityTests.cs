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
			var Entity = new Entity();
			Entity.ComponentAdded += (sender, e) => { triggered = true; };
			Entity.Add(new TestComponent());
			Assert.True(triggered);
		}

		[Fact]
		public void RemoveComponentEventTriggersTest()
		{
			bool triggered = false;
			var Entity = new Entity();
			var component = new TestComponent();
			Entity.Remove(component);
			Assert.False(triggered);

			Entity.ComponentRemoved += (sender, e) => { triggered = true; };
			Entity.Add(component);
			Entity.Remove(component);
			Assert.True(triggered);
		}

		[Fact]
		public void GetComponentTest()
		{
			var Entity = new Entity();
			var component = new TestComponent();
			var compNull = Entity.GetComponent<TestComponent>();
			Assert.Null(compNull);

			Entity.Add(component);
			var comp2 = Entity.GetComponent<TestComponent>();
			Assert.Equal(component, comp2);
		}

		[Fact]
		public void GetChildComponentTest()
		{
			var Entity = new Entity();
			var parentComp = new TestComponent();
			var childComp = new TestChildComponent();

			Entity.Add(childComp);
			var returnVal = Entity.GetComponent<TestComponent>();

			Assert.NotNull(returnVal);
		}

		[Fact]
		public void AutoEntitySetterTest()
		{
			var Entity = new Entity();
			var comp = new TestComponent();
			Assert.Null(comp.Entity);

			Entity.Add(comp);

			Assert.Equal(comp.Entity, Entity);

			Entity.Remove(comp);

			Assert.Null(comp.Entity);
		}
	}
}
