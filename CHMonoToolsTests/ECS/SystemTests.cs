using CHMonoTools.ECS;
using Xunit;

namespace CHMonoToolsTests.ECS
{
	public class SystemTests
	{
		[Fact]
		public void ComponentEventTests()
		{
			var entityContainer = new EntityContainer();
			var System = new TestGameSystem(entityContainer);

			var Entity1 = new Entity();
			Entity1.Add(new TestComponent());
			Entity1.Add(new TestChildComponent());

			entityContainer.Add(Entity1);

			Assert.True(System.componentsAdded == 2 && System.componentsRemoved == 0);

			var Entity2 = new Entity();
			entityContainer.Add(Entity2);
			Entity2.Add(new TestComponent());

			Assert.True(System.componentsAdded == 3 && System.componentsRemoved == 0);

			Entity2.Remove<TestComponent>();

			Assert.True(System.componentsAdded == 3 && System.componentsRemoved == 1);

			entityContainer.Remove(Entity1);

			Assert.True(System.componentsAdded == 3 && System.componentsRemoved == 3);

		}
	}
}
