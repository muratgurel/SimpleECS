using NUnit.Framework;

namespace SimpleECS.Test
{
	[TestFixture]
	public class WorldTests
	{
		private World world;

		[SetUp]
		public void SetUp()
		{
			world = new World();
		}

		[Test]
		public void ShouldCreateNewEntityWithZeroComponents()
		{
			Entity newEntity = world.CreateEntity();
			Assert.AreEqual(newEntity.componentCount, 0);
		}

		[Test]
		public void ShouldAddNewEntityToWorldAfterCreate()
		{
			world.CreateEntity();
			Assert.AreEqual(world.entityCount, 1);
		}

		[Test]
		public void ShouldClearAllComponentsOnDestroy()
		{
			Entity newEntity = world.CreateEntity();
			newEntity.AddComponent<EmptyComponent>();
			world.DestroyEntity(newEntity);

			Assert.AreEqual(newEntity.componentCount, 0);
		}

		[Test]
		public void ShouldRemoveEntityFromWorldAfterDestroy()
		{
			Entity newEntity = world.CreateEntity();
			world.DestroyEntity(newEntity);

			Assert.AreEqual(world.entityCount, 0);
		}

		[Test]
		public void ShouldReturnAllEntitiesWithRequestedComponents()
		{
			Assert.Fail();
		}

		[Test]
		public void ShouldFilterAllEntitiesWithExcludedComponents()
		{
			Assert.Fail();
		}

		private class EmptyComponent : IComponent {}
	}
}

