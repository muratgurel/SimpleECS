using System.Collections.Generic;
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
			EntityPredicate predicate = EntityPredicate.New.Include<EmptyComponent>();

			Entity entityWithEmptyComponent = world.CreateEntity();
			entityWithEmptyComponent.AddComponent<EmptyComponent>();

			Entity entityWithComponentTwo = world.CreateEntity();
			entityWithComponentTwo.AddComponent<EmptyComponentTwo>();

			Entity entityWithAllThreeComponents = world.CreateEntity();
			entityWithAllThreeComponents.AddComponent<EmptyComponent>();
			entityWithAllThreeComponents.AddComponent<EmptyComponentTwo>();
			entityWithAllThreeComponents.AddComponent<EmptyComponentThree>();

			List<Entity> filteredEntities = world.GetEntities(predicate);

			Assert.AreEqual(filteredEntities.Count, 2);
			Assert.Contains(entityWithEmptyComponent, filteredEntities);
			Assert.Contains(entityWithAllThreeComponents, filteredEntities);
		}

		[Test]
		public void ShouldFilterAllEntitiesWithExcludedComponents()
		{
			EntityPredicate predicate = EntityPredicate.New.Exclude<EmptyComponent>();

			Entity entityWithEmptyComponent = world.CreateEntity();
			entityWithEmptyComponent.AddComponent<EmptyComponent>();

			Entity entityWithComponentTwo = world.CreateEntity();
			entityWithComponentTwo.AddComponent<EmptyComponentTwo>();

			Entity entityWithAllThreeComponents = world.CreateEntity();
			entityWithAllThreeComponents.AddComponent<EmptyComponent>();
			entityWithAllThreeComponents.AddComponent<EmptyComponentTwo>();
			entityWithAllThreeComponents.AddComponent<EmptyComponentThree>();

			List<Entity> filteredEntities = world.GetEntities(predicate);

			Assert.AreEqual(filteredEntities.Count, 1);
			Assert.Contains(entityWithComponentTwo, filteredEntities);
		}

		private class EmptyComponent : IComponent { }
		private class EmptyComponentTwo : IComponent { }
		private class EmptyComponentThree : IComponent { }
	}
}

