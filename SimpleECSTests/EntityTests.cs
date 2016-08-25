using NUnit.Framework;

namespace SimpleECS.Test
{
	[TestFixture]
	public class EntityTests
	{
		private World world;
		private Entity entity;

		[SetUp]
		public void SetUp()
		{
			world = new World();
			entity = world.CreateEntity();
		}

		public void ShouldAddComponentToEntityAfterAdd()
		{
			entity.AddComponent<EmptyComponent>();
			Assert.AreEqual(entity.componentCount, 1);
		}

		public void ShouldRemoveComponentFromEntityAfterRemove()
		{
			IComponent newComponent = entity.AddComponent<EmptyComponent>();
			entity.RemoveComponent(newComponent);

			Assert.AreEqual(entity.componentCount, 0);
		}

		public void ShouldClearAllComponentsAfterClear()
		{
			entity.AddComponent<EmptyComponent>();
			entity.ClearAllComponents();

			Assert.AreEqual(entity.componentCount, 0);
		}

		public void ShouldReturnTrueIfComponentPresent()
		{
			entity.AddComponent<EmptyComponent>();
			Assert.IsTrue(entity.HasComponent<EmptyComponent>());
		}

		public void ShouldReturnFalseIfComponentIsNotPresent()
		{
			Assert.IsFalse(entity.HasComponent<EmptyComponent>());
		}

		public void ShouldReturnComponentIfPresent()
		{
			entity.AddComponent<EmptyComponent>();
			Assert.IsNotNull(entity.GetComponent<EmptyComponent>());
		}

		public void ShouldReturnNullIfComponentIsNotPresent()
		{
			Assert.IsNull(entity.GetComponent<EmptyComponent>());
		}

		private class EmptyComponent : IComponent {}
	}
}

