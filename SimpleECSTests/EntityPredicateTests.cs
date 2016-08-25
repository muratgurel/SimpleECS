using NUnit.Framework;

namespace SimpleECS.Test
{
	[TestFixture]
	public class EntityPredicateTests
	{
		private World world;
		private EntityPredicate predicate;

		[SetUp]
		public void SetUp()
		{
			world = new World();
			predicate = EntityPredicate.New;
		}

		[Test]
		public void ShouldMatchAllEntitiesWithEmptyPredicate()
		{
			Entity emptyEntity = world.CreateEntity();

			Entity entityWithEmptyComponentOne = world.CreateEntity();
			entityWithEmptyComponentOne.AddComponent<EmptyComponentOne>();

			Entity entityWithAllThreeComponents = world.CreateEntity();
			entityWithAllThreeComponents.AddComponent<EmptyComponentOne>();
			entityWithAllThreeComponents.AddComponent<EmptyComponentTwo>();
			entityWithAllThreeComponents.AddComponent<EmptyComponentThree>();

			Assert.IsTrue(predicate.Matches(emptyEntity));
			Assert.IsTrue(predicate.Matches(entityWithEmptyComponentOne));
			Assert.IsTrue(predicate.Matches(entityWithAllThreeComponents));
		}

		[Test]
		public void ShouldFilterEntitiesWithExcludedTypes()
		{
			predicate.Exclude<EmptyComponentTwo>();

			Entity entityWithEmptyComponentTwo = world.CreateEntity();
			entityWithEmptyComponentTwo.AddComponent<EmptyComponentTwo>();

			Entity entityWithAllThreeComponents = world.CreateEntity();
			entityWithAllThreeComponents.AddComponent<EmptyComponentOne>();
			entityWithAllThreeComponents.AddComponent<EmptyComponentTwo>();
			entityWithAllThreeComponents.AddComponent<EmptyComponentThree>();

			Assert.IsFalse(predicate.Matches(entityWithEmptyComponentTwo));
			Assert.IsFalse(predicate.Matches(entityWithAllThreeComponents));
		}

		[Test]
		public void ShouldReturnEntitiesWithIncludedTypes()
		{
			predicate.Include<EmptyComponentTwo>();

			Entity entityWithEmptyComponentTwo = world.CreateEntity();
			entityWithEmptyComponentTwo.AddComponent<EmptyComponentTwo>();

			Entity entityWithAllThreeComponents = world.CreateEntity();
			entityWithAllThreeComponents.AddComponent<EmptyComponentOne>();
			entityWithAllThreeComponents.AddComponent<EmptyComponentTwo>();
			entityWithAllThreeComponents.AddComponent<EmptyComponentThree>();

			Assert.IsTrue(predicate.Matches(entityWithEmptyComponentTwo));
			Assert.IsTrue(predicate.Matches(entityWithAllThreeComponents));
		}

		[Test]
		public void ShouldBeChainable()
		{
			predicate.Include<EmptyComponentOne>().Include<EmptyComponentTwo>().Exclude<EmptyComponentThree>();

			Entity entityWithOneAndTwoComponents = world.CreateEntity();
			entityWithOneAndTwoComponents.AddComponent<EmptyComponentOne>();
			entityWithOneAndTwoComponents.AddComponent<EmptyComponentTwo>();

			Assert.IsTrue(predicate.Matches(entityWithOneAndTwoComponents));
		}

		private class EmptyComponentOne : IComponent { }
		private class EmptyComponentTwo : IComponent { }
		private class EmptyComponentThree : IComponent { }
	}
}
