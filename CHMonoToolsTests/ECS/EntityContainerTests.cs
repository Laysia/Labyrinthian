using CHMonoTools.ECS;
using Xunit;

namespace CHMonoToolsTests.ECS
{
	public class EntityContainerTests
	{
		[Fact]
		public void AddRemoveEntityEventTriggerTests()
		{
			int entitiyCount = 0;
			var EntityContainer = new EntityContainer();
			EntityContainer.EntityAdded += (sender, e) => { entitiyCount++; };
			EntityContainer.EntityRemoved += (sender, e) => { entitiyCount--; };

			Entity entity = Entity.CreateNew();
			EntityContainer.Add(entity);
			Assert.True(entitiyCount == 1);
			EntityContainer.Remove(entity);
			Assert.True(entitiyCount == 0);
			
		}
	}
}
